using System.Windows;
using BAIOS.App.ViewModels;
using BAIOS.Config;
using BAIOS.Core;
using BAIOS.Tools;

namespace BAIOS.App;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        DispatcherUnhandledException += (_, args) =>
        {
            AppLog.Error("Excepción no capturada.", args.Exception);
            args.Handled = true;
            MessageBox.Show(args.Exception.Message, "BAIOS", MessageBoxButton.OK, MessageBoxImage.Warning);
        };

        AppLog.Info("Arranque " + typeof(App).Assembly.GetName().Version);
        if (EngineUpdater.TryRelaunchToApplyPending())
        {
            Shutdown();
            return;
        }

        var config = ConfigModule.Load();
        var window = new MainWindow
        {
            DataContext = new MainViewModel(config)
        };
        window.Show();
    }
}
