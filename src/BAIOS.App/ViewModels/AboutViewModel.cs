using System.IO;
using System.Windows.Input;
using BAIOS.App.Services;
using BAIOS.Config;
using BAIOS.Core;
using BAIOS.Tools;

namespace BAIOS.App.ViewModels;

public sealed class AboutViewModel : ViewModelBase
{
    public AboutViewModel(ShellViewModel shell)
    {
        Shell = shell;
        Engine = EngineUpdater.Load();
        LicenseText = ReadLicense();
        OpenWebsiteCommand = new RelayCommand(() => AppLinks.OpenUrl(AppLinks.Website));
        OpenForumCommand = new RelayCommand(() => AppLinks.OpenUrl(AppLinks.Forum));
        OpenGplCommand = new RelayCommand(AppLinks.OpenLicense);
        OpenLogCommand = new RelayCommand(OpenLog);
        OpenGuideCommand = new RelayCommand(AppLinks.OpenUserGuide);
        UpdateEngineCommand = new AsyncRelayCommand(UpdateEngineAsync, () => IsTechnician);
    }

    public ShellViewModel Shell { get; }
    public EngineDocument Engine { get; private set; }
    public string Title => "Acerca de BAIOS";
    public string Version => Shell.Main.Session.EngineVersion;
    public string EngineSummary =>
        string.IsNullOrWhiteSpace(Engine.Version) ? "engine.json local." : "Motor declarado: v" + Engine.Version + ".";
    public string Body =>
        "BAIOS (Blinter All In One Security) es un proyecto de Andago.\n\n" +
        "Logos de marca: colaboración de velosergio. El escudo actual es una marca geométrica del motor 4.x " +
        "(no se embeben logos de fabricantes).\n\n" +
        "Las herramientas de terceros son propiedad de sus autores. BAIOS solo las lanza; cada fabricante aplica su EULA.";
    public string LicenseText { get; }
    public bool IsTechnician => Shell.IsTechnician;
    public ICommand OpenWebsiteCommand { get; }
    public ICommand OpenForumCommand { get; }
    public ICommand OpenGplCommand { get; }
    public ICommand OpenLogCommand { get; }
    public ICommand OpenGuideCommand { get; }
    public ICommand UpdateEngineCommand { get; }

    private async Task UpdateEngineAsync()
    {
        await Shell.RunBusyAsync("Actualizando motor…", async ct =>
        {
            var result = await EngineUpdater.UpdateAsync(Shell.Config.EngineUrl, cancellationToken: ct);
            Engine = EngineUpdater.Load();
            RaisePropertyChanged(nameof(Engine));
            RaisePropertyChanged(nameof(EngineSummary));
            System.Windows.MessageBox.Show(
                result.Message,
                "Motor",
                System.Windows.MessageBoxButton.OK,
                result.Success ? System.Windows.MessageBoxImage.Information : System.Windows.MessageBoxImage.Warning);
        });
    }

    private static void OpenLog()
    {
        var path = AppLog.PathFor();
        if (!File.Exists(path))
        {
            AppLog.Info("Log creado al abrir Acerca de.");
        }

        AppLinks.OpenUrl(path);
    }

    private static string ReadLicense()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "LICENSE");
        if (!File.Exists(path))
        {
            path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "LICENSE"));
        }

        try
        {
            return File.Exists(path) ? File.ReadAllText(path) : "No se encontró LICENSE junto al exe.";
        }
        catch (Exception ex)
        {
            return "No se pudo leer LICENSE: " + ex.Message;
        }
    }
}
