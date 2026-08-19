using System.Windows.Input;
using BAIOS.App.Services;

namespace BAIOS.App.ViewModels;

public sealed class AgreementViewModel : ViewModelBase
{
    public AgreementViewModel(MainViewModel main)
    {
        AcceptCommand = new RelayCommand(main.ShowModeSelect);
        DeclineCommand = new RelayCommand(main.ShowWelcome);
        OpenWebsiteCommand = new RelayCommand(() => AppLinks.OpenUrl(AppLinks.Website));
        OpenForumCommand = new RelayCommand(() => AppLinks.OpenUrl(AppLinks.Forum));
    }

    public ICommand AcceptCommand { get; }
    public ICommand DeclineCommand { get; }
    public ICommand OpenWebsiteCommand { get; }
    public ICommand OpenForumCommand { get; }
}
