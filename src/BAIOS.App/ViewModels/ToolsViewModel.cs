using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BAIOS.App.Views;
using BAIOS.Config;
using BAIOS.Tools;

namespace BAIOS.App.ViewModels;

public sealed class ToolItemViewModel : ViewModelBase
{
    private string _status = "";
    private bool _canDownload;
    private string _updateLabel = "Actualizar";

    public ToolItemViewModel(ToolCard card, string? path, ToolInstallState state, string toolsDirectory)
    {
        Card = card;
        Path = path;
        Apply(state);
        Meta = string.IsNullOrWhiteSpace(card.Version)
            ? $"{card.Vendor} · {card.Architecture} · Tools/{card.Id}/"
            : $"v{card.Version} · {card.Vendor} · {card.Architecture} · Tools/{card.Id}/";
        Icon = LoadIcon(toolsDirectory, card);
    }

    public ToolCard Card { get; }
    public string? Path { get; private set; }
    public bool Found => Path is not null;
    public string Meta { get; }
    public ImageSource? Icon { get; }
    public bool HasIcon => Icon is not null;

    public string Status
    {
        get => _status;
        set => SetProperty(ref _status, value);
    }

    public bool CanDownload
    {
        get => _canDownload;
        private set => SetProperty(ref _canDownload, value);
    }

    public string UpdateLabel
    {
        get => _updateLabel;
        private set => SetProperty(ref _updateLabel, value);
    }

    public void Apply(ToolInstallState state, string? path = null)
    {
        if (path is not null)
        {
            Path = path;
            RaisePropertyChanged(nameof(Path));
            RaisePropertyChanged(nameof(Found));
        }

        Status = state.Status;
        CanDownload = state.CanDownload;
        UpdateLabel = state.HasBinary ? "Actualizar" : "Instalar";
    }

    private static ImageSource? LoadIcon(string toolsDirectory, ToolCard card)
    {
        var name = string.IsNullOrWhiteSpace(card.Icon) ? "icon.png" : System.IO.Path.GetFileName(card.Icon);
        if (string.IsNullOrWhiteSpace(name))
        {
            return null;
        }

        var path = System.IO.Path.Combine(ManifestStore.ToolFolder(toolsDirectory, card.Id), name);
        if (!File.Exists(path))
        {
            return null;
        }

        try
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(path);
            bitmap.EndInit();
            bitmap.Freeze();
            return bitmap;
        }
        catch
        {
            return null;
        }
    }
}

public sealed class ToolsViewModel : ViewModelBase
{
    public ToolsViewModel(ShellViewModel shell)
    {
        Shell = shell;
        LaunchCommand = new AsyncRelayCommand(p => LaunchAsync(p as ToolItemViewModel));
        UpdateCommand = new AsyncRelayCommand(p => UpdateAsync(p as ToolItemViewModel));
        OpenUrlCommand = new RelayCommand(p => OpenUrl(p as string));
        ShowCreditsCommand = new RelayCommand(p => ShowCredits(p as ToolItemViewModel));
        OpenLogCommand = new RelayCommand(p => OpenLog(p as ToolItemViewModel));
        OpenFolderCommand = new RelayCommand(p => OpenFolder(p as ToolItemViewModel));
        RefreshCommand = new RelayCommand(Reload);
        RefreshManifestCommand = new AsyncRelayCommand(RefreshManifestAsync, () => HasManifestUrl);
        Reload();
    }

    public ShellViewModel Shell { get; }
    public bool IsTechnician => Shell.IsTechnician;
    public ObservableCollection<ToolItemViewModel> Tools { get; } = [];
    public ObservableCollection<RescueItem> Rescue { get; } = [];
    public ICommand LaunchCommand { get; }
    public ICommand UpdateCommand { get; }
    public ICommand OpenUrlCommand { get; }
    public ICommand ShowCreditsCommand { get; }
    public ICommand OpenLogCommand { get; }
    public ICommand OpenFolderCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand RefreshManifestCommand { get; }
    public string ToolsPath => ConfigModule.ToolsDirectory(Shell.Config);
    public string ManifestSummary { get; private set; } = "";
    public bool HasManifestUrl => ManifestStore.IsHttpsUrl(Shell.Config.ManifestUrl);

    public void Reload()
    {
        Tools.Clear();
        var dir = ToolsPath;
        var catalog = ToolsModule.LoadCatalog(dir);
        var origin = catalog.FromFile ? catalog.ManifestPath : "núcleo embebido";
        ManifestSummary = $"Manifiesto schema 1 ({catalog.Tools.Count} ficha(s)): {origin}.";
        RaisePropertyChanged(nameof(ManifestSummary));
        RaisePropertyChanged(nameof(HasManifestUrl));

        foreach (var card in ToolsModule.VisibleTools(catalog, IsTechnician))
        {
            var launch = ToolsModule.Inspect(dir, card);
            var state = ToolUpdater.Inspect(dir, card);
            Tools.Add(new ToolItemViewModel(card, launch.Path, state, dir));
        }

        Rescue.Clear();
        foreach (var item in RescueCatalog.Load())
        {
            Rescue.Add(item);
        }
    }

    private async Task LaunchAsync(ToolItemViewModel? item)
    {
        if (item is null)
        {
            return;
        }

        if (item.Card.FalsePositiveWarning)
        {
            var go = MessageBox.Show(
                item.Card.Name + " marca a menudo entradas legítimas (falsos positivos). " +
                "No deshabilites ni borres nada sin saber qué es. ¿Continuar?",
                "Falsos positivos",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);
            if (go != MessageBoxResult.Yes)
            {
                ToolsModule.LogSkipped(ToolsPath, item.Card, "cancelado: alerta de falsos positivos");
                item.Status = "No se lanzó: el usuario rechazó la alerta de falsos positivos.";
                return;
            }
        }

        if (!item.Card.Portable)
        {
            var go = MessageBox.Show(
                "Esto es un instalador, no un portable. Se instalará software de terceros con su propia EULA. ¿Continuar?",
                item.Card.Name,
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (go != MessageBoxResult.Yes)
            {
                ToolsModule.LogSkipped(ToolsPath, item.Card, "cancelado: confirmación de instalador");
                item.Status = "No se lanzó: instalador no confirmado.";
                return;
            }
        }

        if (item.Card.ExpiresDays is > 0)
        {
            MessageBox.Show(
                $"{item.Card.Name} caduca ~{item.Card.ExpiresDays} días después de descargarlo. Usa una copia reciente.",
                item.Card.Name,
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        if (item.Card.TrialDays is > 0)
        {
            MessageBox.Show(
                $"{item.Card.Name} es una prueba (~{item.Card.TrialDays} días). Revisa la EULA del fabricante.",
                item.Card.Name,
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        await Shell.RunBusyAsync("Lanzando " + item.Card.Name + "…", async _ =>
        {
            ToolLaunch launch = null!;
            await Task.Run(() => launch = ToolsModule.Launch(ToolsPath, item.Card));
            item.Status = launch.Message ?? "";
            Shell.AddFinding(ToolsModule.ToFinding(launch));
        });
    }

    private async Task UpdateAsync(ToolItemViewModel? item)
    {
        if (item is null)
        {
            return;
        }

        if (!item.CanDownload)
        {
            MessageBox.Show(
                "Para descargar hace falta una URL https y sha256 (64 hex) en el manifiesto. Sin eso, coloca el binario a mano.",
                "Actualizar",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
            return;
        }

        await Shell.RunBusyAsync("Descargando " + item.Card.Name + "…", async ct =>
        {
            var result = await ToolUpdater.UpdateAsync(ToolsPath, item.Card, ct);
            var state = ToolUpdater.Inspect(ToolsPath, item.Card);
            item.Apply(state, result.Path);
            MessageBox.Show(result.Message, item.Card.Name, MessageBoxButton.OK,
                result.Success ? MessageBoxImage.Information : MessageBoxImage.Warning);
        });
    }

    private async Task RefreshManifestAsync()
    {
        var url = Shell.Config.ManifestUrl;
        if (!ManifestStore.IsHttpsUrl(url))
        {
            return;
        }

        await Shell.RunBusyAsync("Actualizando manifiesto…", async ct =>
        {
            var local = ConfigModule.ManifestPath();
            var result = await ToolUpdater.RefreshManifestAsync(url!, local, ct);
            MessageBox.Show(result.Message, "Manifiesto", MessageBoxButton.OK,
                result.Success ? MessageBoxImage.Information : MessageBoxImage.Warning);
            Reload();
        });
    }

    private void ShowCredits(ToolItemViewModel? item)
    {
        if (item is null)
        {
            return;
        }

        var dialog = new CreditsWindow
        {
            Owner = Application.Current.MainWindow,
            DataContext = CreditsViewModel.ForTool(item.Card)
        };
        dialog.ShowDialog();
    }

    private void OpenLog(ToolItemViewModel? item)
    {
        if (item is null)
        {
            return;
        }

        var path = ExecutionLog.PathFor(ToolsPath, item.Card.Id);
        if (!File.Exists(path))
        {
            MessageBox.Show("Aún no hay log de ejecución de esta herramienta.", item.Card.Name, MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        Process.Start(new ProcessStartInfo { FileName = path, UseShellExecute = true });
    }

    private void OpenFolder(ToolItemViewModel? item)
    {
        if (item is null)
        {
            return;
        }

        var folder = ManifestStore.ToolFolder(ToolsPath, item.Card.Id);
        Directory.CreateDirectory(folder);
        Process.Start(new ProcessStartInfo { FileName = folder, UseShellExecute = true });
    }

    private static void OpenUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return;
        }

        Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
    }
}
