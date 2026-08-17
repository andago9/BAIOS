using System.Collections.ObjectModel;
using System.Windows.Input;
using BAIOS.Core;
using BAIOS.Diagnostics;
using BAIOS.Networking;
using BAIOS.Security;

namespace BAIOS.App.ViewModels;

public sealed class SignalItem
{
    public string Name { get; init; } = "";
    public Severity Severity { get; init; }
    public string Summary { get; init; } = "";
    public string? Detail { get; init; }
}

public sealed class HomeViewModel : ViewModelBase
{
    private string _detail = "Pulsa «Actualizar» o la acción principal.";

    public HomeViewModel(ShellViewModel shell)
    {
        Shell = shell;
        RefreshCommand = new AsyncRelayCommand(RefreshAsync);
        PrimaryCommand = new AsyncRelayCommand(PrimaryAsync);
        _ = RefreshAsync();
    }

    public ShellViewModel Shell { get; }
    public bool IsTechnician => Shell.IsTechnician;
    public string PrimaryLabel => IsTechnician ? "Diagnóstico completo" : "Revisar equipo";
    public ObservableCollection<SignalItem> Signals { get; } = [];
    public ObservableCollection<SignalItem> Alerts { get; } = [];
    public ICommand RefreshCommand { get; }
    public ICommand PrimaryCommand { get; }

    public string Detail
    {
        get => _detail;
        set => SetProperty(ref _detail, value);
    }

    public void RefreshFromSession()
    {
        Alerts.Clear();
        foreach (var finding in Shell.Session.Findings.Where(f => f.Severity != Severity.Ok).Take(IsTechnician ? 8 : 3))
        {
            Alerts.Add(new SignalItem { Name = finding.Title, Severity = finding.Severity, Summary = finding.Detail ?? finding.Title });
        }
    }

    private Task PrimaryAsync() => IsTechnician ? Shell.RunFullDiagnosticAsync() : Shell.RunSafeReviewAsync();

    private async Task RefreshAsync()
    {
        await Shell.RunBusyAsync("Leyendo estado del equipo…", async _ =>
        {
            SecuritySnapshot security = null!;
            DiagnosticsSnapshot diag = null!;
            NetworkSnapshot net = null!;
            await Task.Run(() =>
            {
                security = SecurityModule.GetSnapshot();
                diag = DiagnosticsModule.GetSnapshot();
                net = NetworkingModule.GetSnapshot();
            });

            Signals.Clear();
            Signals.Add(new SignalItem
            {
                Name = "Windows Update",
                Severity = diag.WindowsUpdate.Severity,
                Summary = diag.WindowsUpdate.Summary,
                Detail = diag.WindowsUpdate.LastHotfix
            });
            Signals.Add(new SignalItem
            {
                Name = "Defender",
                Severity = security.Defender.Presence switch
                {
                    DefenderPresence.Active => Severity.Ok,
                    DefenderPresence.Passive => Severity.Warning,
                    _ => Severity.Fail
                },
                Summary = security.Defender.Summary,
                Detail = IsTechnician ? $"Último rápido: {security.Defender.LastQuickScan ?? "n/d"}; build {diag.Windows.Build}" : null
            });
            Signals.Add(new SignalItem
            {
                Name = "Firewall",
                Severity = security.Firewall.AnyEnabled ? Severity.Ok : Severity.Fail,
                Summary = security.Firewall.Summary,
                Detail = security.Firewall.Detail
            });

            var worstDisk = diag.Volumes.OrderBy(v => v.FreePercent).FirstOrDefault();
            Signals.Add(new SignalItem
            {
                Name = "Disco",
                Severity = worstDisk is null ? Severity.Warning : worstDisk.FreePercent <= 10 ? Severity.Fail : worstDisk.FreePercent <= 18 ? Severity.Warning : Severity.Ok,
                Summary = worstDisk is null ? "Sin volúmenes." : $"{worstDisk.Name} libre {worstDisk.FreePercent}%",
                Detail = IsTechnician && worstDisk is not null
                    ? string.Join(" · ", diag.Volumes.Select(v => $"{v.Name} {v.FreePercent}% libre"))
                    : null
            });
            Signals.Add(new SignalItem
            {
                Name = "RAM",
                Severity = diag.Memory.UsedPercent >= 90 ? Severity.Fail : diag.Memory.UsedPercent >= 80 ? Severity.Warning : Severity.Ok,
                Summary = $"{diag.Memory.UsedPercent}% en uso",
                Detail = IsTechnician ? $"{DiagnosticsModule.FormatBytes(diag.Memory.FreeBytes)} libres · {diag.Windows.Summary}" : null
            });

            Detail = IsTechnician
                ? $"{diag.Windows.Summary}. CPU {diag.Cpu.Name}. Internet: {(net.InternetLikely ? "sí" : "no")}."
                : net.InternetLikely ? "El equipo responde en red." : "No hay respuesta de Internet (1.1.1.1).";

            Alerts.Clear();
            foreach (var finding in security.Findings.Concat(diag.Findings).Concat(net.Findings)
                         .Where(f => f.Severity != Severity.Ok)
                         .Take(IsTechnician ? 8 : 3))
            {
                Alerts.Add(new SignalItem { Name = finding.Title, Severity = finding.Severity, Summary = finding.Detail ?? finding.Title });
            }
        });
    }
}
