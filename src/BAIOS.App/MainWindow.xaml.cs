using System.ComponentModel;
using System.Windows;
using BAIOS.App.ViewModels;
using BAIOS.App.Views;

namespace BAIOS.App;

public partial class MainWindow : Window
{
    private bool _creditsShown;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnClosing(object? sender, CancelEventArgs e)
    {
        if (_creditsShown)
        {
            return;
        }

        e.Cancel = true;
        var dialog = new CreditsWindow
        {
            Owner = this,
            DataContext = CreditsViewModel.ForApp()
        };
        dialog.ShowDialog();
        _creditsShown = true;
        Close();
    }
}
