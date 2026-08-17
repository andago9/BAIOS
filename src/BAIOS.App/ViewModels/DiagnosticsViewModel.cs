using System.Collections.ObjectModel;
using System.Windows.Input;
using BAIOS.Config;
using BAIOS.Diagnostics;
using BAIOS.Tools;

namespace BAIOS.App.ViewModels;

public sealed class DiagnosticsViewModel : ViewModelBase
{
    private DiagnosticsSnapshot _snapshot = new();
    private bool _showDetail;

    public DiagnosticsViewModel(ShellViewModel shell)
    {
        Shell = shell;
        RefreshCommand = new AsyncRelayCommand(RefreshAsync);
        ToggleDetailCommand = new RelayCommand(() => ShowDetail = !ShowDetail);
        OpenAutorunsCommand = new AsyncRelayCommand(OpenAutorunsAsync);
    }

    public ShellViewModel Shell { get; }
    public bool IsTechnician => Shell.IsTechnician;
    public ICommand RefreshCommand { get; }
    public ICommand ToggleDetailCommand { get; }
    public ICommand OpenAutorunsCommand { get; }
    public ObservableCollection<string> SummaryLines { get; } = [];

    public DiagnosticsSnapshot Snapshot
    {
        get => _snapshot;
        private set => SetProperty(ref _snapshot, value);
    }

    public bool ShowDetail
    {
        get => _showDetail;
        set
        {
            if (SetProperty(ref _showDetail, value))
            {
                RaisePropertyChanged(nameof(ShowTables));
            }
        }
    }

    public bool ShowTables => IsTechnician || ShowDetail;

    private async Task RefreshAsync()
    {
        await Shell.RunBusyAsync("Inventariando hardware y sistema…", async _ =>
        {
            DiagnosticsSnapshot snap = null!;
            await Task.Run(() => snap = DiagnosticsModule.GetSnapshot());
            Snapshot = snap;
            SummaryLines.Clear();
            SummaryLines.Add($"{snap.Windows.Summary}");
            SummaryLines.Add($"CPU: {snap.Cpu.Name} ({snap.Cpu.Cores} núcleos)");
            SummaryLines.Add($"RAM: {snap.Memory.UsedPercent}% en uso ({DiagnosticsModule.FormatBytes(snap.Memory.TotalBytes)})");
            foreach (var v in snap.Volumes)
            {
                SummaryLines.Add($"Disco {v.Name}: {v.FreePercent}% libre");
            }

            SummaryLines.Add(snap.SmartAvailable
                ? $"SMART: {snap.Smart.Count} disco(s); fallos predichos: {snap.Smart.Count(s => s.PredictFailure)}"
                : "SMART no accesible");
            SummaryLines.Add($"Servicios: {snap.RunningServices}/{snap.TotalServices} en ejecución; {snap.AutoStoppedServices.Count} automáticos detenidos");
            SummaryLines.Add($"Procesos: {snap.ProcessCount}. Inicio: {snap.StartupItems.Count}. Drivers con error: {snap.ProblemDrivers.Count}");
            RaisePropertyChanged(nameof(ShowTables));
        });
    }

    private async Task OpenAutorunsAsync()
    {
        await Shell.RunBusyAsync("Lanzando Autoruns…", async _ =>
        {
            var dir = ConfigModule.ToolsDirectory(Shell.Config);
            ToolLaunch launch = null!;
            await Task.Run(() =>
            {
                var catalog = ToolsModule.LoadCatalog(dir);
                var card = ToolsModule.Require(catalog, "autoruns");
                launch = ToolsModule.Launch(dir, card);
            });
            Shell.AddFinding(ToolsModule.ToFinding(launch));
        });
    }
}
