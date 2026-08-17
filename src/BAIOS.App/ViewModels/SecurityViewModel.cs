using System.Windows;
using System.Windows.Input;
using BAIOS.Core;
using BAIOS.Security;

namespace BAIOS.App.ViewModels;

public sealed class SecurityViewModel : ViewModelBase
{
    private DefenderStatus _defender = new();
    private FirewallStatus _firewall = new();
    private string _lastAction = "";
    private bool _offlineAvailable;
    private string _offlineHint = "Comprobando análisis sin conexión…";

    public SecurityViewModel(ShellViewModel shell)
    {
        Shell = shell;
        RefreshCommand = new AsyncRelayCommand(RefreshAsync);
        QuickScanCommand = new AsyncRelayCommand(() => ScanAsync("Análisis rápido de Defender", SecurityModule.StartQuickScanAsync));
        FullScanCommand = new AsyncRelayCommand(() => ScanAsync("Análisis completo de Defender", SecurityModule.StartFullScanAsync));
        OfflineScanCommand = new AsyncRelayCommand(OfflineAsync, () => OfflineAvailable);
    }

    public ShellViewModel Shell { get; }
    public bool IsTechnician => Shell.IsTechnician;
    public ICommand RefreshCommand { get; }
    public ICommand QuickScanCommand { get; }
    public ICommand FullScanCommand { get; }
    public ICommand OfflineScanCommand { get; }

    public DefenderStatus Defender
    {
        get => _defender;
        private set => SetProperty(ref _defender, value);
    }

    public FirewallStatus Firewall
    {
        get => _firewall;
        private set => SetProperty(ref _firewall, value);
    }

    public string LastAction
    {
        get => _lastAction;
        private set => SetProperty(ref _lastAction, value);
    }

    public bool OfflineAvailable
    {
        get => _offlineAvailable;
        private set
        {
            if (SetProperty(ref _offlineAvailable, value))
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }
    }

    public string OfflineHint
    {
        get => _offlineHint;
        private set => SetProperty(ref _offlineHint, value);
    }

    private async Task RefreshAsync()
    {
        await Shell.RunBusyAsync("Leyendo Defender y firewall…", async _ =>
        {
            SecuritySnapshot snap = null!;
            var offline = false;
            await Task.Run(() =>
            {
                snap = SecurityModule.GetSnapshot();
                offline = SecurityModule.IsOfflineScanAvailable();
            });
            Defender = snap.Defender;
            Firewall = snap.Firewall;
            OfflineAvailable = offline;
            OfflineHint = offline
                ? "El análisis sin conexión reinicia el equipo. Úsalo solo si hace falta."
                : "Análisis sin conexión no disponible en este Windows.";
            LastAction = snap.Defender.Summary;
        });
    }

    private async Task ScanAsync(string title, Func<CancellationToken, Task<CommandResult>> start)
    {
        var extra = IsTechnician ? " Se registrará el código de salida." : "";
        if (MessageBox.Show(title + " requiere administrador (UAC) y puede tardar." + extra + " ¿Continuar?",
                "Seguridad", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
        {
            return;
        }

        await Shell.RunBusyAsync(title + "…", async ct =>
        {
            var result = await start(ct);
            LastAction = FormatResult(title, result);
            Shell.AddFinding(ToFinding(title, result));
        });
    }

    private async Task OfflineAsync()
    {
        if (MessageBox.Show(
                "Windows Defender Offline reinicia el equipo para analizar fuera del sistema habitual. Guarda tu trabajo. ¿Continuar?",
                "Análisis sin conexión",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning) != MessageBoxResult.Yes)
        {
            return;
        }

        await Shell.RunBusyAsync("Iniciando análisis sin conexión…", async ct =>
        {
            var result = await SecurityModule.StartOfflineScanAsync(ct);
            LastAction = FormatResult("Análisis sin conexión", result);
            Shell.AddFinding(ToFinding("Análisis sin conexión de Defender", result));
        });
    }

    private static string FormatResult(string title, CommandResult result)
    {
        if (result.Cancelled)
        {
            return title + ": UAC cancelado.";
        }

        if (!string.IsNullOrWhiteSpace(result.Message) && result.ExitCode is null)
        {
            return title + ": " + result.Message;
        }

        return $"{title}: código {result.ExitCode} ({DateTime.Now:HH:mm:ss}).";
    }

    private static Finding ToFinding(string title, CommandResult result)
    {
        if (result.Cancelled)
        {
            return Finding.Create(Sections.Security, Severity.Warning, title, "native:security", result.Message);
        }

        if (!result.Success)
        {
            return Finding.Create(Sections.Security, Severity.Fail, title, "native:security", result.Message ?? $"Código {result.ExitCode}");
        }

        return Finding.Create(Sections.Security, Severity.Ok, title, "native:security", $"Código {result.ExitCode} a las {DateTime.Now:HH:mm:ss}");
    }
}
