using BAIOS.Core;

namespace BAIOS.Security;

public static class SecurityModule
{
    private const string Source = "native:security";

    public static SecuritySnapshot GetSnapshot()
    {
        var defender = ReadDefender();
        var firewall = ReadFirewall();
        var findings = new List<Finding>
        {
            DefenderFinding(defender),
            FirewallFinding(firewall)
        };
        return new SecuritySnapshot
        {
            Defender = defender,
            Firewall = firewall,
            Findings = findings
        };
    }

    public static string? FindMpCmdRun()
    {
        var candidates = new[]
        {
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Windows Defender", "MpCmdRun.exe"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Windows Defender", "MpCmdRun.exe"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "MpCmdRun.exe")
        };

        return candidates.FirstOrDefault(File.Exists);
    }

    public static bool IsOfflineScanAvailable()
    {
        try
        {
            var result = CommandRunner.RunAsync(
                "powershell.exe",
                "-NoProfile -NonInteractive -Command \"if (Get-Command Start-MpWDOScan -ErrorAction SilentlyContinue) { 'yes' } else { 'no' }\"",
                requireAdmin: false,
                captureOutput: true).GetAwaiter().GetResult();
            return result.Success && result.StandardOutput.Contains("yes", StringComparison.OrdinalIgnoreCase);
        }
        catch
        {
            return false;
        }
    }

    public static Task<CommandResult> StartQuickScanAsync(CancellationToken cancellationToken = default) =>
        StartScanAsync(1, cancellationToken);

    public static Task<CommandResult> StartFullScanAsync(CancellationToken cancellationToken = default) =>
        StartScanAsync(2, cancellationToken);

    public static Task<CommandResult> StartOfflineScanAsync(CancellationToken cancellationToken = default) =>
        CommandRunner.RunAsync(
            "powershell.exe",
            "-NoProfile -Command \"Start-MpWDOScan\"",
            requireAdmin: true,
            captureOutput: true,
            cancellationToken: cancellationToken);

    private static Task<CommandResult> StartScanAsync(int scanType, CancellationToken cancellationToken)
    {
        var path = FindMpCmdRun();
        if (path is null)
        {
            return Task.FromResult(new CommandResult
            {
                Success = false,
                Message = "No se encontró MpCmdRun.exe. Microsoft Defender no está disponible en este equipo."
            });
        }

        return CommandRunner.RunAsync(path, $"-Scan -ScanType {scanType}", requireAdmin: true, cancellationToken: cancellationToken);
    }

    private static DefenderStatus ReadDefender()
    {
        var rows = WmiQuery.Query(@"root\Microsoft\Windows\Defender", "SELECT * FROM MSFT_MpComputerStatus");
        if (rows.Count == 0)
        {
            return new DefenderStatus
            {
                Presence = DefenderPresence.Absent,
                Summary = "Microsoft Defender ausente o no accesible (WMI)."
            };
        }

        var row = rows[0];
        var antivirus = WmiQuery.Flag(row, "AntivirusEnabled");
        var rtp = WmiQuery.Flag(row, "RealTimeProtectionEnabled");
        var service = WmiQuery.Flag(row, "AMServiceEnabled");
        var presence = !antivirus
            ? DefenderPresence.Absent
            : rtp ? DefenderPresence.Active : DefenderPresence.Passive;

        var quickAge = ToInt(row, "QuickScanAge");
        var fullAge = ToInt(row, "FullScanAge");
        var summary = presence switch
        {
            DefenderPresence.Active => "Microsoft Defender activo (protección en tiempo real).",
            DefenderPresence.Passive => "Microsoft Defender en modo pasivo (otro antivirus o RTP desactivada).",
            DefenderPresence.Absent => "Microsoft Defender no está activo.",
            _ => "Estado de Defender desconocido."
        };

        return new DefenderStatus
        {
            Presence = presence,
            RealTimeProtection = rtp,
            ServiceEnabled = service,
            QuickScanAgeDays = quickAge,
            FullScanAgeDays = fullAge,
            LastQuickScan = FormatAge(quickAge),
            LastFullScan = FormatAge(fullAge),
            Summary = summary
        };
    }

    private static FirewallStatus ReadFirewall()
    {
        var rows = WmiQuery.Query(@"root\StandardCimv2", "SELECT Name, Enabled FROM MSFT_NetFirewallProfile");
        if (rows.Count > 0)
        {
            bool On(string name) => rows.Any(r =>
                string.Equals(WmiQuery.Text(r, "Name"), name, StringComparison.OrdinalIgnoreCase)
                && WmiQuery.Flag(r, "Enabled"));

            var domain = On("Domain");
            var priv = On("Private");
            var pub = On("Public");
            var any = domain || priv || pub;
            return new FirewallStatus
            {
                DomainEnabled = domain,
                PrivateEnabled = priv,
                PublicEnabled = pub,
                Summary = any ? "Firewall de Windows activo en al menos un perfil." : "Firewall de Windows desactivado en todos los perfiles.",
                Detail = $"Dominio: {YesNo(domain)}; Privado: {YesNo(priv)}; Público: {YesNo(pub)}"
            };
        }

        return ReadFirewallViaNetsh();
    }

    private static FirewallStatus ReadFirewallViaNetsh()
    {
        try
        {
            var result = CommandRunner.RunAsync(
                "netsh.exe",
                "advfirewall show allprofiles state",
                requireAdmin: false).GetAwaiter().GetResult();
            var text = result.CombinedOutput;
            if (string.IsNullOrWhiteSpace(text))
            {
                return new FirewallStatus { Summary = "No se pudo leer el firewall.", Detail = result.Message };
            }

            var on = text.Contains("ON", StringComparison.OrdinalIgnoreCase)
                     || text.Contains("Activado", StringComparison.OrdinalIgnoreCase);
            return new FirewallStatus
            {
                DomainEnabled = on,
                PrivateEnabled = on,
                PublicEnabled = on,
                Summary = on ? "Firewall de Windows parece activo." : "Firewall de Windows parece desactivado.",
                Detail = text.Trim()
            };
        }
        catch (Exception ex)
        {
            return new FirewallStatus { Summary = "No se pudo leer el firewall.", Detail = ex.Message };
        }
    }

    private static Finding DefenderFinding(DefenderStatus status)
    {
        var severity = status.Presence switch
        {
            DefenderPresence.Active => Severity.Ok,
            DefenderPresence.Passive => Severity.Warning,
            _ => Severity.Fail
        };
        var detail = status.LastQuickScan is null
            ? status.Summary
            : $"{status.Summary} Último análisis rápido: {status.LastQuickScan}.";
        return Finding.Create(Sections.Security, severity, status.Summary, Source, detail);
    }

    private static Finding FirewallFinding(FirewallStatus status)
    {
        var severity = status.AnyEnabled ? Severity.Ok : Severity.Fail;
        return Finding.Create(Sections.Security, severity, status.Summary, Source, status.Detail);
    }

    private static int? ToInt(Dictionary<string, object?> row, string name)
    {
        if (!row.TryGetValue(name, out var value) || value is null)
        {
            return null;
        }

        try
        {
            var n = Convert.ToInt32(value);
            return n < 0 || n > 36500 ? null : n;
        }
        catch
        {
            return null;
        }
    }

    private static string? FormatAge(int? days)
    {
        if (days is null)
        {
            return null;
        }

        return days.Value == 0 ? "hoy" : $"hace {days.Value} día(s)";
    }

    private static string YesNo(bool value) => value ? "sí" : "no";
}
