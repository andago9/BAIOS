using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using BAIOS.Config;
using BAIOS.Core;
using BAIOS.Diagnostics;
using BAIOS.Networking;
using BAIOS.Reports;
using BAIOS.Security;
using BAIOS.App.Services;
using BAIOS.App.Views;

namespace BAIOS.App.ViewModels;

public sealed class ShellViewModel : ViewModelBase
{
    private object? _section;
    private string _status = "Listo.";
    private bool _busy;
    private CancellationTokenSource? _flowCts;

    public ShellViewModel(MainViewModel main)
    {
        Main = main;
        Home = new HomeViewModel(this);
        Security = new SecurityViewModel(this);
        Diagnostics = new DiagnosticsViewModel(this);
        Maintenance = new MaintenanceViewModel(this);
        Network = new NetworkViewModel(this);
        Tools = new ToolsViewModel(this);
        Reports = new ReportsViewModel(this);
        Section = Home;

        NavigateCommand = new RelayCommand(p => Navigate(p as string));
        ChangeModeCommand = new RelayCommand(main.ShowModeSelect, () => !IsBusy);
        CancelFlowCommand = new RelayCommand(CancelFlow, () => _flowCts is not null);
        AboutCommand = new RelayCommand(ShowAbout);
    }

    public MainViewModel Main { get; }
    public Session Session => Main.Session;
    public AppConfig Config => Main.Config;
    public bool IsTechnician => Session.Mode == AppMode.Technician;
    public bool IsHome => Session.Mode == AppMode.Home;
    public string ModeLabel => IsTechnician ? "Técnico" : "Hogar";
    public string ElevationLabel => Elevation.IsAdministrator ? "Administrador" : "Sin elevación";
    public HomeViewModel Home { get; }
    public SecurityViewModel Security { get; }
    public DiagnosticsViewModel Diagnostics { get; }
    public MaintenanceViewModel Maintenance { get; }
    public NetworkViewModel Network { get; }
    public ToolsViewModel Tools { get; }
    public ReportsViewModel Reports { get; }
    public ICommand NavigateCommand { get; }
    public ICommand ChangeModeCommand { get; }
    public ICommand CancelFlowCommand { get; }
    public ICommand AboutCommand { get; }

    public object? Section
    {
        get => _section;
        private set => SetProperty(ref _section, value);
    }

    public string Status
    {
        get => _status;
        set => SetProperty(ref _status, value);
    }

    public bool IsBusy
    {
        get => _busy;
        set
        {
            if (SetProperty(ref _busy, value))
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }
    }

    public void Navigate(string? key)
    {
        Section = key switch
        {
            "security" => Security,
            "diagnostics" => Diagnostics,
            "maintenance" => Maintenance,
            "network" => Network,
            "tools" => Tools,
            "reports" => Reports,
            _ => Home
        };

        switch (key)
        {
            case "security":
                Security.RefreshCommand.Execute(null);
                break;
            case "diagnostics":
                Diagnostics.RefreshCommand.Execute(null);
                break;
            case "network":
                Network.RefreshCommand.Execute(null);
                break;
            case "tools":
                Tools.Reload();
                break;
            case "reports":
                Reports.Reload();
                break;
        }
    }

    public async Task RunSafeReviewAsync()
    {
        await RunBusyAsync("Revisando el equipo…", async ct =>
        {
            await Task.Run(() => CollectReadOnly(ct), ct);
            RecommendationBuilder.Apply(Session);
            Status = "Revisión de lectura completada.";
            Home.RefreshFromSession();
        });
    }

    public async Task RunFullDiagnosticAsync()
    {
        if (!IsTechnician)
        {
            await RunSafeReviewAsync();
            return;
        }

        _flowCts = new CancellationTokenSource();
        var ct = _flowCts.Token;
        IsBusy = true;
        try
        {
            Status = "Diagnóstico completo: seguridad…";
            await Task.Run(() =>
            {
                var security = SecurityModule.GetSnapshot();
                ReplaceNative(Sections.Security, security.Findings);
            }, ct);
            ct.ThrowIfCancellationRequested();

            Status = "Diagnóstico completo: hardware y sistema…";
            await Task.Run(() =>
            {
                var diag = DiagnosticsModule.GetSnapshot();
                ReplaceNative(Sections.System, diag.Findings.Where(f => f.Section == Sections.System));
                ReplaceNative(Sections.Storage, diag.Findings.Where(f => f.Section == Sections.Storage));
            }, ct);
            ct.ThrowIfCancellationRequested();

            Status = "Diagnóstico completo: red…";
            await Task.Run(() =>
            {
                var net = NetworkingModule.GetSnapshot();
                ReplaceNative(Sections.Network, net.Findings);
            }, ct);
            ct.ThrowIfCancellationRequested();

            var maintenance = MessageBox.Show(
                "¿Ejecutar limpieza de temporales y papelera ahora? DISM, SFC y CHKDSK no se lanzan solos: ve a Mantenimiento si los necesitas.",
                "Mantenimiento opcional",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (maintenance)
            {
                Status = "Diagnóstico completo: mantenimiento (limpieza)…";
                await Task.Run(() =>
                {
                    var plan = BAIOS.Maintenance.MaintenanceModule.BuildCleanupPlan();
                    Session.Add(BAIOS.Maintenance.MaintenanceModule.RunCleanup(plan));
                }, ct);
            }

            RecommendationBuilder.Apply(Session);
            var reportsDir = ConfigModule.ReportsDirectory(Config);
            if (IsHome)
            {
                var privacy = MessageBox.Show(
                    "El informe incluye el nombre del equipo (hostname) y datos de adaptadores. ¿Guardar?",
                    "Privacidad",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Information);
                if (privacy != MessageBoxResult.Yes)
                {
                    Status = "Diagnóstico completo sin guardar informe.";
                    Navigate("home");
                    Home.RefreshFromSession();
                    return;
                }
            }

            var files = ReportsModule.Write(Session, reportsDir);
            Reports.Reload();
            Navigate("reports");
            Status = $"Informe guardado: {Path.GetFileName(files.HtmlPath)}";
            Home.RefreshFromSession();
        }
        catch (OperationCanceledException)
        {
            Status = "Diagnóstico completo cancelado.";
        }
        catch (Exception ex)
        {
            Status = "Error: " + ex.Message;
        }
        finally
        {
            _flowCts?.Dispose();
            _flowCts = null;
            IsBusy = false;
            CommandManager.InvalidateRequerySuggested();
        }
    }

    public async Task RunBusyAsync(string status, Func<CancellationToken, Task> work)
    {
        IsBusy = true;
        Status = status;
        try
        {
            await work(CancellationToken.None);
        }
        catch (Exception ex)
        {
            Status = "Error: " + ex.Message;
            MessageBox.Show(ex.Message, "BAIOS", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        finally
        {
            IsBusy = false;
        }
    }

    public void AddFinding(Finding finding)
    {
        Session.Add(finding);
        RecommendationBuilder.Apply(Session);
    }

    private void CollectReadOnly(CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        var security = SecurityModule.GetSnapshot();
        ReplaceNative(Sections.Security, security.Findings);
        ct.ThrowIfCancellationRequested();
        var diag = DiagnosticsModule.GetSnapshot();
        ReplaceNative(Sections.System, diag.Findings.Where(f => f.Section == Sections.System));
        ReplaceNative(Sections.Storage, diag.Findings.Where(f => f.Section == Sections.Storage));
        ct.ThrowIfCancellationRequested();
        var net = NetworkingModule.GetSnapshot();
        ReplaceNative(Sections.Network, net.Findings);
    }

    private void ReplaceNative(string section, IEnumerable<Finding> incoming)
    {
        Session.Findings.RemoveAll(f => f.Section == section && f.Source.StartsWith("native:", StringComparison.OrdinalIgnoreCase));
        Session.AddRange(incoming);
    }

    private void CancelFlow() => _flowCts?.Cancel();

    private void ShowAbout()
    {
        var dialog = new AboutWindow
        {
            Owner = Application.Current.MainWindow,
            DataContext = new AboutViewModel(this)
        };
        dialog.ShowDialog();
    }
}
