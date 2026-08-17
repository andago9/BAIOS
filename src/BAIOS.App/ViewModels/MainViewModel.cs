using BAIOS.Config;
using BAIOS.Core;

namespace BAIOS.App.ViewModels;

public sealed class MainViewModel : ViewModelBase
{
    private object? _current;

    public MainViewModel(AppConfig config)
    {
        Config = config;
        Session = new Session { EngineVersion = "4.0.0-dev" };
        ShowWelcome();
    }

    public AppConfig Config { get; }
    public Session Session { get; }

    public object? Current
    {
        get => _current;
        private set => SetProperty(ref _current, value);
    }

    public void ShowWelcome() => Current = new WelcomeViewModel(this);

    public void ShowAgreement() => Current = new AgreementViewModel(this);

    public void ShowModeSelect() => Current = new ModeSelectViewModel(this);

    public void EnterShell(AppMode mode)
    {
        Session.Mode = mode;
        AppLog.Info("Modo " + mode);
        Current = new ShellViewModel(this);
    }
}
