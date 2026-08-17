using System.Windows.Input;
using BAIOS.Config;
using BAIOS.Core;

namespace BAIOS.App.ViewModels;

public sealed class ModeSelectViewModel : ViewModelBase
{
    public ModeSelectViewModel(MainViewModel main)
    {
        SuggestedMode = ConfigStore.ResolveDefaultMode(main.Config);
        UsbDetected = ConfigStore.IsRunningFromRemovable();
        ChooseHomeCommand = new RelayCommand(() => main.EnterShell(AppMode.Home));
        ChooseTechnicianCommand = new RelayCommand(() => main.EnterShell(AppMode.Technician));
    }

    public AppMode SuggestedMode { get; }
    public bool UsbDetected { get; }
    public bool SuggestHome => SuggestedMode == AppMode.Home;
    public bool SuggestTechnician => SuggestedMode == AppMode.Technician;
    public string Hint => UsbDetected
        ? "Se detectó ejecución desde unidad extraíble: se preselecciona Técnico."
        : SuggestedMode == AppMode.Technician
            ? "config.json indica ModeDefault=Technician."
            : "Elige el modo. Puedes cambiar de densidad, no de motor.";

    public ICommand ChooseHomeCommand { get; }
    public ICommand ChooseTechnicianCommand { get; }
}
