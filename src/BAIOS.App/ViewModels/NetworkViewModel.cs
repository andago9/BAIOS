using System.Collections.ObjectModel;
using System.Windows.Input;
using BAIOS.Networking;

namespace BAIOS.App.ViewModels;

public sealed class NetworkViewModel : ViewModelBase
{
    private NetworkSnapshot _snapshot = new();
    private string _trace = "";
    private string _last = "";

    public NetworkViewModel(ShellViewModel shell)
    {
        Shell = shell;
        RefreshCommand = new AsyncRelayCommand(RefreshAsync);
        TraceCommand = new AsyncRelayCommand(TraceAsync);
        FlushCommand = new AsyncRelayCommand(FlushAsync);
        RenewCommand = new AsyncRelayCommand(RenewAsync);
    }

    public ShellViewModel Shell { get; }
    public bool IsTechnician => Shell.IsTechnician;
    public ICommand RefreshCommand { get; }
    public ICommand TraceCommand { get; }
    public ICommand FlushCommand { get; }
    public ICommand RenewCommand { get; }
    public ObservableCollection<string> HomeLines { get; } = [];

    public NetworkSnapshot Snapshot
    {
        get => _snapshot;
        private set => SetProperty(ref _snapshot, value);
    }

    public string TraceOutput
    {
        get => _trace;
        private set => SetProperty(ref _trace, value);
    }

    public string Last
    {
        get => _last;
        private set => SetProperty(ref _last, value);
    }

    private async Task RefreshAsync()
    {
        await Shell.RunBusyAsync("Leyendo red…", async _ =>
        {
            NetworkSnapshot snap = null!;
            await Task.Run(() => snap = NetworkingModule.GetSnapshot());
            Snapshot = snap;
            HomeLines.Clear();
            HomeLines.Add(snap.InternetLikely ? "Hay Internet (respuesta de 1.1.1.1)." : "No hay respuesta de Internet.");
            var adapter = snap.Adapters.FirstOrDefault();
            if (adapter is not null)
            {
                HomeLines.Add($"Dirección: {string.Join(", ", adapter.Addresses)}");
                HomeLines.Add($"DNS: {string.Join(", ", adapter.Dns)}");
                HomeLines.Add($"Puerta de enlace: {string.Join(", ", adapter.Gateways)}");
            }
        });
    }

    private async Task TraceAsync()
    {
        await Shell.RunBusyAsync("Trazando ruta…", async ct =>
        {
            TraceOutput = await NetworkingModule.TraceRouteAsync(NetworkingModule.PublicDns, ct);
        });
    }

    private async Task FlushAsync()
    {
        await Shell.RunBusyAsync("Flush DNS…", async ct =>
        {
            var result = await NetworkingModule.FlushDnsAsync(ct);
            var finding = NetworkingModule.FromCommand("Flush DNS", result);
            Shell.AddFinding(finding);
            Last = finding.Detail ?? finding.Title;
        });
    }

    private async Task RenewAsync()
    {
        await Shell.RunBusyAsync("Renovando DHCP…", async ct =>
        {
            var result = await NetworkingModule.RenewDhcpAsync(ct);
            var finding = NetworkingModule.FromCommand("Renovar DHCP", result);
            Shell.AddFinding(finding);
            Last = finding.Detail ?? finding.Title;
        });
    }
}
