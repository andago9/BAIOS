using System.Windows;

namespace BAIOS.App.Views;

public partial class CreditsWindow : Window
{
    public CreditsWindow() => InitializeComponent();

    private void Close_Click(object sender, RoutedEventArgs e) => Close();
}
