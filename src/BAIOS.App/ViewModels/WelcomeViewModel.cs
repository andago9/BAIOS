using System.Windows.Input;
using BAIOS.App.Services;

namespace BAIOS.App.ViewModels;

public sealed class WelcomeViewModel : ViewModelBase
{
    public WelcomeViewModel(MainViewModel main)
    {
        ContinueCommand = new RelayCommand(main.ShowAgreement);
        OpenLicenseCommand = new RelayCommand(AppLinks.OpenLicense);
        OpenWebsiteCommand = new RelayCommand(() => AppLinks.OpenUrl(AppLinks.Website));
    }

    public string ProductName => "BAIOS";
    public string Tagline => "Blinter All In One Security";
    public string Version => "4.0.0-dev";
    public ICommand ContinueCommand { get; }
    public ICommand OpenLicenseCommand { get; }
    public ICommand OpenWebsiteCommand { get; }
}
