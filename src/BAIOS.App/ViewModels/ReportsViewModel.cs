using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using BAIOS.Config;
using BAIOS.Core;
using BAIOS.Reports;
using BAIOS.App.Services;

namespace BAIOS.App.ViewModels;

public sealed class ReportRow
{
    public required string Name { get; init; }
    public required string HtmlPath { get; init; }
    public DateTime Written { get; init; }
}

public sealed class ReportsViewModel : ViewModelBase
{
    public ReportsViewModel(ShellViewModel shell)
    {
        Shell = shell;
        SaveCommand = new RelayCommand(Save);
        OpenHtmlCommand = new RelayCommand(p => Open((p as ReportRow)?.HtmlPath));
        OpenFolderCommand = new RelayCommand(OpenFolder);
        ReloadCommand = new RelayCommand(Reload);
        Reload();
    }

    public ShellViewModel Shell { get; }
    public bool IsHome => Shell.IsHome;
    public ObservableCollection<ReportRow> Reports { get; } = [];
    public ICommand SaveCommand { get; }
    public ICommand OpenHtmlCommand { get; }
    public ICommand OpenFolderCommand { get; }
    public ICommand ReloadCommand { get; }
    public string ReportsPath => ConfigModule.ReportsDirectory(Shell.Config);

    public void Reload()
    {
        Reports.Clear();
        foreach (var file in ReportsModule.ListReports(ReportsPath))
        {
            Reports.Add(new ReportRow
            {
                Name = Path.GetFileName(file.HtmlPath),
                HtmlPath = file.HtmlPath,
                Written = File.GetLastWriteTime(file.HtmlPath)
            });
        }
    }

    private void Save()
    {
        if (IsHome)
        {
            var ok = MessageBox.Show(
                "El informe incluye el nombre del equipo (hostname) y puede listar adaptadores de red. Sin telemetría: se guarda solo en este PC. ¿Continuar?",
                "Privacidad",
                MessageBoxButton.YesNo,
                MessageBoxImage.Information);
            if (ok != MessageBoxResult.Yes)
            {
                return;
            }
        }

        RecommendationBuilder.Apply(Shell.Session);
        if (Shell.Session.Findings.Count == 0)
        {
            MessageBox.Show("Aún no hay hallazgos. Usa «Revisar equipo» o «Diagnóstico completo» primero.", "Reportes", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var files = ReportsModule.Write(Shell.Session, ReportsPath);
        Reload();
        Shell.Status = "Informe: " + Path.GetFileName(files.HtmlPath);
    }

    private void OpenFolder()
    {
        Directory.CreateDirectory(ReportsPath);
        Process.Start(new ProcessStartInfo { FileName = ReportsPath, UseShellExecute = true });
    }

    private static void Open(string? path)
    {
        if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
        {
            return;
        }

        Process.Start(new ProcessStartInfo { FileName = path, UseShellExecute = true });
    }
}
