using System.IO;
using System.Windows;
using System.Windows.Input;
using BAIOS.Core;
using BAIOS.Maintenance;

namespace BAIOS.App.ViewModels;

public sealed class MaintenanceViewModel : ViewModelBase
{
    private string _last = "Nada se ejecuta al abrir esta pantalla.";

    public MaintenanceViewModel(ShellViewModel shell)
    {
        Shell = shell;
        CleanupCommand = new AsyncRelayCommand(CleanupAsync);
        DismCommand = new AsyncRelayCommand(DismAsync);
        SfcCommand = new AsyncRelayCommand(SfcAsync);
        ChkdskCommand = new AsyncRelayCommand(ChkdskAsync);
    }

    public ShellViewModel Shell { get; }
    public bool IsHome => Shell.IsHome;
    public ICommand CleanupCommand { get; }
    public ICommand DismCommand { get; }
    public ICommand SfcCommand { get; }
    public ICommand ChkdskCommand { get; }

    public string Last
    {
        get => _last;
        private set => SetProperty(ref _last, value);
    }

    private async Task CleanupAsync()
    {
        var plan = MaintenanceModule.BuildCleanupPlan();
        var text = MaintenanceModule.ConfirmationText(plan, Shell.IsHome);
        if (MessageBox.Show(text, "Confirmar limpieza", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
        {
            return;
        }

        await Shell.RunBusyAsync("Limpiando temporales…", async _ =>
        {
            Finding finding = null!;
            await Task.Run(() => finding = MaintenanceModule.RunCleanup(plan));
            Shell.AddFinding(finding);
            Last = finding.Detail ?? finding.Title;
        });
    }

    private async Task DismAsync()
    {
        if (!Confirm("DISM /RestoreHealth puede tardar mucho y requiere administrador. ¿Continuar?", "DISM"))
        {
            return;
        }

        await Shell.RunBusyAsync("Ejecutando DISM…", async ct =>
        {
            var result = await MaintenanceModule.RunDismAsync(ct);
            var finding = MaintenanceModule.FromCommand("DISM RestoreHealth", result);
            Shell.AddFinding(finding);
            Last = Format(result);
        });
    }

    private async Task SfcAsync()
    {
        if (!Confirm("SFC /scannow puede tardar y requiere administrador. ¿Continuar?", "SFC"))
        {
            return;
        }

        await Shell.RunBusyAsync("Ejecutando SFC…", async ct =>
        {
            var result = await MaintenanceModule.RunSfcAsync(ct);
            var finding = MaintenanceModule.FromCommand("SFC scannow", result);
            Shell.AddFinding(finding);
            Last = Format(result);
        });
    }

    private async Task ChkdskAsync()
    {
        var system = Path.GetPathRoot(Environment.SystemDirectory) ?? "C:\\";
        var warn = "CHKDSK /F en el volumen del sistema suele programarse para el próximo reinicio y puede tardar bastante. " +
                   "No se usa /R (más agresivo). ¿Programar comprobación de " + system + "?";
        if (MessageBox.Show(warn, "CHKDSK", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
        {
            return;
        }

        await Shell.RunBusyAsync("Programando CHKDSK…", async ct =>
        {
            var result = await MaintenanceModule.ScheduleChkdskAsync(system, ct);
            var finding = MaintenanceModule.FromCommand("CHKDSK /F (programado)", result);
            Shell.AddFinding(finding);
            Last = Format(result);
        });
    }

    private bool Confirm(string text, string caption)
    {
        var copy = IsHome
            ? text + Environment.NewLine + Environment.NewLine + "No cierres BAIOS hasta que termine. Si cancelas el UAC, no se ejecutará nada."
            : text;
        return MessageBox.Show(copy, caption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
    }

    private static string Format(CommandResult result)
    {
        if (result.Cancelled)
        {
            return result.Message ?? "UAC cancelado.";
        }

        return $"Código {result.ExitCode}. {result.Message}".Trim();
    }
}
