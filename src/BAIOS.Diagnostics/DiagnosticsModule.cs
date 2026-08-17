using System.Diagnostics;
using System.ServiceProcess;
using BAIOS.Core;
using Microsoft.Win32;

namespace BAIOS.Diagnostics;

public static class DiagnosticsModule
{
    private const string Source = "native:diagnostics";

    public static DiagnosticsSnapshot GetSnapshot()
    {
        var cpu = ReadCpu();
        var memory = ReadMemory();
        var volumes = ReadVolumes();
        var smart = ReadSmart();
        var services = ReadServices();
        var processes = ReadProcesses();
        var startup = ReadStartup();
        var drivers = ReadProblemDrivers();
        var windows = ReadWindows();
        var wu = ReadWindowsUpdate();

        var findings = new List<Finding>();
        findings.Add(Finding.Create(
            Sections.System,
            Severity.Ok,
            $"CPU: {cpu.Name}",
            Source,
            cpu.LoadPercent is null ? $"{cpu.Cores} núcleos" : $"{cpu.Cores} núcleos; carga {cpu.LoadPercent}%"));

        var memSeverity = memory.UsedPercent >= 90 ? Severity.Fail : memory.UsedPercent >= 80 ? Severity.Warning : Severity.Ok;
        findings.Add(Finding.Create(
            Sections.System,
            memSeverity,
            $"RAM usada: {memory.UsedPercent}%",
            Source,
            $"{FormatBytes(memory.TotalBytes - memory.FreeBytes)} de {FormatBytes(memory.TotalBytes)}"));

        foreach (var volume in volumes)
        {
            var sev = volume.FreePercent <= 10 ? Severity.Fail : volume.FreePercent <= 18 ? Severity.Warning : Severity.Ok;
            findings.Add(Finding.Create(
                Sections.Storage,
                sev,
                $"Disco {volume.Name} libre: {volume.FreePercent}%",
                Source,
                $"{FormatBytes(volume.FreeBytes)} libres de {FormatBytes(volume.TotalBytes)} ({volume.Label})"));
        }

        if (!smart.Available)
        {
            findings.Add(Finding.Create(Sections.Storage, Severity.Warning, "SMART no accesible", Source, "WMI no expuso MSStorageDriver_FailurePredictStatus. Hace falta admin o el disco no lo publica."));
        }
        else if (smart.Disks.Any(d => d.PredictFailure))
        {
            findings.Add(Finding.Create(Sections.Storage, Severity.Fail, "SMART predice fallo de disco", Source, string.Join(", ", smart.Disks.Where(d => d.PredictFailure).Select(d => d.Instance))));
        }
        else
        {
            findings.Add(Finding.Create(Sections.Storage, Severity.Ok, "SMART: sin predicción de fallo", Source, $"{smart.Disks.Count} disco(s) consultado(s)."));
        }

        var autoStopped = services.AutoStopped;
        findings.Add(Finding.Create(
            Sections.System,
            autoStopped.Count == 0 ? Severity.Ok : Severity.Warning,
            autoStopped.Count == 0 ? "Servicios de inicio automático en ejecución" : $"{autoStopped.Count} servicio(s) automático(s) detenido(s)",
            Source,
            $"{services.Running} en ejecución de {services.Total}"));

        findings.Add(Finding.Create(
            Sections.System,
            Severity.Ok,
            $"{processes.Count} procesos",
            Source,
            string.Join(", ", processes.Top.Take(5).Select(p => $"{p.Name} ({FormatBytes(p.WorkingSetBytes)})"))));

        findings.Add(Finding.Create(
            Sections.System,
            Severity.Ok,
            $"{startup.Count} programa(s) de inicio",
            Source,
            string.Join("; ", startup.Take(8).Select(s => s.Name))));

        findings.Add(Finding.Create(
            Sections.System,
            drivers.Count == 0 ? Severity.Ok : Severity.Warning,
            drivers.Count == 0 ? "Sin drivers con error de PnP" : $"{drivers.Count} driver(s) con problema",
            Source,
            drivers.Count == 0 ? null : string.Join("; ", drivers.Select(d => $"{d.Name} ({d.ErrorCode})"))));

        findings.Add(Finding.Create(
            Sections.System,
            wu.Severity,
            wu.Summary,
            Source,
            wu.LastHotfix));

        return new DiagnosticsSnapshot
        {
            Cpu = cpu,
            Memory = memory,
            Volumes = volumes,
            Smart = smart.Disks,
            SmartAvailable = smart.Available,
            AutoStoppedServices = autoStopped,
            RunningServices = services.Running,
            TotalServices = services.Total,
            TopProcesses = processes.Top,
            ProcessCount = processes.Count,
            StartupItems = startup,
            ProblemDrivers = drivers,
            Windows = windows,
            WindowsUpdate = wu,
            Findings = findings
        };
    }

    private static CpuInfo ReadCpu()
    {
        var rows = WmiQuery.Query(@"root\cimv2", "SELECT Name, NumberOfCores, LoadPercentage FROM Win32_Processor");
        if (rows.Count == 0)
        {
            return new CpuInfo();
        }

        var row = rows[0];
        return new CpuInfo
        {
            Name = WmiQuery.Text(row, "Name")?.Trim() ?? "CPU",
            Cores = WmiQuery.Int32(row, "NumberOfCores"),
            LoadPercent = WmiQuery.Int32(row, "LoadPercentage")
        };
    }

    private static MemoryInfo ReadMemory()
    {
        var rows = WmiQuery.Query(@"root\cimv2", "SELECT TotalVisibleMemorySize, FreePhysicalMemory FROM Win32_OperatingSystem");
        if (rows.Count == 0)
        {
            return new MemoryInfo();
        }

        var row = rows[0];
        var totalKb = WmiQuery.Int64(row, "TotalVisibleMemorySize") ?? 0;
        var freeKb = WmiQuery.Int64(row, "FreePhysicalMemory") ?? 0;
        return new MemoryInfo { TotalBytes = totalKb * 1024, FreeBytes = freeKb * 1024 };
    }

    private static List<VolumeInfo> ReadVolumes()
    {
        var list = new List<VolumeInfo>();
        foreach (var drive in DriveInfo.GetDrives())
        {
            try
            {
                if (!drive.IsReady || drive.DriveType is not (DriveType.Fixed or DriveType.Removable))
                {
                    continue;
                }

                list.Add(new VolumeInfo
                {
                    Name = drive.Name,
                    Label = string.IsNullOrWhiteSpace(drive.VolumeLabel) ? drive.DriveFormat : drive.VolumeLabel,
                    TotalBytes = drive.TotalSize,
                    FreeBytes = drive.AvailableFreeSpace
                });
            }
            catch
            {
            }
        }

        return list;
    }

    private static (bool Available, List<SmartDisk> Disks) ReadSmart()
    {
        var rows = WmiQuery.Query(@"root\wmi", "SELECT InstanceName, PredictFailure FROM MSStorageDriver_FailurePredictStatus");
        if (rows.Count == 0)
        {
            return (false, []);
        }

        var disks = rows.Select(r => new SmartDisk
        {
            Instance = WmiQuery.Text(r, "InstanceName") ?? "disco",
            PredictFailure = WmiQuery.Flag(r, "PredictFailure")
        }).ToList();
        return (true, disks);
    }

    private static (int Total, int Running, List<ServiceRow> AutoStopped) ReadServices()
    {
        var autoStopped = new List<ServiceRow>();
        var total = 0;
        var running = 0;
        try
        {
            foreach (var service in ServiceController.GetServices())
            {
                using (service)
                {
                    total++;
                    if (service.Status == ServiceControllerStatus.Running)
                    {
                        running++;
                    }

                    if (service.StartType == ServiceStartMode.Automatic && service.Status != ServiceControllerStatus.Running)
                    {
                        autoStopped.Add(new ServiceRow
                        {
                            Name = service.ServiceName,
                            DisplayName = service.DisplayName,
                            Status = service.Status.ToString(),
                            StartType = service.StartType.ToString()
                        });
                    }
                }
            }
        }
        catch
        {
        }

        return (total, running, autoStopped.OrderBy(s => s.DisplayName).Take(40).ToList());
    }

    private static (int Count, List<ProcessRow> Top) ReadProcesses()
    {
        Process[] processes;
        try
        {
            processes = Process.GetProcesses();
        }
        catch
        {
            return (0, []);
        }

        try
        {
            var top = processes
                .Select(p =>
                {
                    try
                    {
                        return new ProcessRow { Name = p.ProcessName, Id = p.Id, WorkingSetBytes = p.WorkingSet64 };
                    }
                    catch
                    {
                        return new ProcessRow { Name = p.ProcessName, Id = p.Id };
                    }
                })
                .OrderByDescending(p => p.WorkingSetBytes)
                .Take(15)
                .ToList();
            return (processes.Length, top);
        }
        finally
        {
            foreach (var p in processes)
            {
                p.Dispose();
            }
        }
    }

    private static List<StartupRow> ReadStartup()
    {
        var items = new List<StartupRow>();
        foreach (var row in WmiQuery.Query(@"root\cimv2", "SELECT Name, Command, Location FROM Win32_StartupCommand"))
        {
            items.Add(new StartupRow
            {
                Name = WmiQuery.Text(row, "Name") ?? "",
                Command = WmiQuery.Text(row, "Command") ?? "",
                Location = WmiQuery.Text(row, "Location") ?? ""
            });
        }

        AddRunKey(items, Registry.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\Run", "HKCU\\Run");
        AddRunKey(items, Registry.LocalMachine, @"Software\Microsoft\Windows\CurrentVersion\Run", "HKLM\\Run");
        return items
            .GroupBy(i => i.Name + "|" + i.Command, StringComparer.OrdinalIgnoreCase)
            .Select(g => g.First())
            .OrderBy(i => i.Name)
            .ToList();
    }

    private static void AddRunKey(List<StartupRow> items, RegistryKey root, string path, string location)
    {
        try
        {
            using var key = root.OpenSubKey(path);
            if (key is null)
            {
                return;
            }

            foreach (var name in key.GetValueNames())
            {
                items.Add(new StartupRow
                {
                    Name = name,
                    Command = Convert.ToString(key.GetValue(name)) ?? "",
                    Location = location
                });
            }
        }
        catch
        {
        }
    }

    private static List<DriverRow> ReadProblemDrivers()
    {
        var rows = WmiQuery.Query(@"root\cimv2", "SELECT Name, ConfigManagerErrorCode FROM Win32_PnPEntity WHERE ConfigManagerErrorCode <> 0");
        return rows
            .Select(r => new DriverRow
            {
                Name = WmiQuery.Text(r, "Name") ?? "dispositivo",
                ErrorCode = WmiQuery.Int32(r, "ConfigManagerErrorCode") ?? 0
            })
            .OrderBy(d => d.Name)
            .Take(50)
            .ToList();
    }

    private static WindowsIdentityInfo ReadWindows()
    {
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
            if (key is null)
            {
                return new WindowsIdentityInfo { Product = Environment.OSVersion.VersionString };
            }

            var product = Convert.ToString(key.GetValue("ProductName")) ?? "";
            var display = Convert.ToString(key.GetValue("DisplayVersion")) ?? Convert.ToString(key.GetValue("ReleaseId")) ?? "";
            var build = Convert.ToString(key.GetValue("CurrentBuild")) ?? "";
            var ubr = key.GetValue("UBR");
            if (ubr is not null)
            {
                build = $"{build}.{ubr}";
            }

            return new WindowsIdentityInfo { Product = product, DisplayVersion = display, Build = build };
        }
        catch
        {
            return new WindowsIdentityInfo { Product = Environment.OSVersion.VersionString };
        }
    }

    private static WindowsUpdateStatus ReadWindowsUpdate()
    {
        string? lastHotfix = null;
        var hotfixRows = WmiQuery.Query(@"root\cimv2", "SELECT HotFixID, InstalledOn FROM Win32_QuickFixEngineering");
        if (hotfixRows.Count > 0)
        {
            lastHotfix = hotfixRows
                .Select(r => $"{WmiQuery.Text(r, "HotFixID")} ({WmiQuery.Text(r, "InstalledOn")})")
                .LastOrDefault();
        }

        int? pendingCount = null;
        try
        {
            var task = Task.Run(QueryPendingUpdates);
            pendingCount = task.Wait(TimeSpan.FromSeconds(8)) ? task.Result : null;
        }
        catch
        {
            pendingCount = null;
        }

        if (pendingCount is null && lastHotfix is null)
        {
            return new WindowsUpdateStatus
            {
                Summary = "No se pudo leer Windows Update.",
                Severity = Severity.Warning
            };
        }

        if (pendingCount is > 0)
        {
            return new WindowsUpdateStatus
            {
                PendingCount = pendingCount,
                LastHotfix = lastHotfix,
                Summary = $"{pendingCount} actualización(es) pendiente(s).",
                Severity = Severity.Warning
            };
        }

        return new WindowsUpdateStatus
        {
            PendingCount = pendingCount ?? 0,
            LastHotfix = lastHotfix,
            Summary = pendingCount is 0 ? "Windows Update: sin pendientes locales." : "Windows Update: se leyó el historial de hotfixes.",
            Severity = Severity.Ok
        };
    }

    private static int QueryPendingUpdates()
    {
        var type = Type.GetTypeFromProgID("Microsoft.Update.Session")
                   ?? throw new InvalidOperationException("Microsoft.Update.Session no está disponible.");
        var session = Activator.CreateInstance(type)
                      ?? throw new InvalidOperationException("No se pudo crear la sesión de Windows Update.");
        var searcher = type.InvokeMember("CreateUpdateSearcher", System.Reflection.BindingFlags.InvokeMethod, null, session, null)
                       ?? throw new InvalidOperationException("No se pudo crear el buscador de actualizaciones.");
        var searcherType = searcher.GetType();
        searcherType.InvokeMember("Online", System.Reflection.BindingFlags.SetProperty, null, searcher, [false]);
        var result = searcherType.InvokeMember("Search", System.Reflection.BindingFlags.InvokeMethod, null, searcher, ["IsInstalled=0 and IsHidden=0"])
                     ?? throw new InvalidOperationException("La búsqueda de actualizaciones no devolvió resultado.");
        var updates = result.GetType().InvokeMember("Updates", System.Reflection.BindingFlags.GetProperty, null, result, null)
                      ?? throw new InvalidOperationException("No hay colección de actualizaciones.");
        return Convert.ToInt32(updates.GetType().InvokeMember("Count", System.Reflection.BindingFlags.GetProperty, null, updates, null));
    }

    public static string FormatBytes(long bytes)
    {
        string[] units = ["B", "KB", "MB", "GB", "TB"];
        double value = bytes;
        var unit = 0;
        while (value >= 1024 && unit < units.Length - 1)
        {
            value /= 1024;
            unit++;
        }

        return $"{value:0.#} {units[unit]}";
    }
}
