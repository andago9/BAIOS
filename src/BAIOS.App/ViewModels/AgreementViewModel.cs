using System.Windows.Input;
using BAIOS.App.Services;

namespace BAIOS.App.ViewModels;

public sealed class AgreementViewModel : ViewModelBase
{
    public AgreementViewModel(MainViewModel main)
    {
        AcceptCommand = new RelayCommand(main.ShowModeSelect);
        DeclineCommand = new RelayCommand(main.ShowWelcome);
        OpenForumCommand = new RelayCommand(() => AppLinks.OpenUrl(AppLinks.Forum));
    }

    public ICommand AcceptCommand { get; }
    public ICommand DeclineCommand { get; }
    public ICommand OpenForumCommand { get; }
}
