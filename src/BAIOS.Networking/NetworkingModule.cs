using System.Net.NetworkInformation;
using BAIOS.Core;

namespace BAIOS.Networking;

public sealed class AdapterInfo
{
    public string Name { get; init; } = "";
    public string Type { get; init; } = "";
    public string Status { get; init; } = "";
    public IReadOnlyList<string> Addresses { get; init; } = [];
    public IReadOnlyList<string> Dns { get; init; } = [];
    public IReadOnlyList<string> Gateways { get; init; } = [];
    public string AddressText => string.Join(", ", Addresses);
    public string DnsText => string.Join(", ", Dns);
    public string GatewayText => string.Join(", ", Gateways);
}

public sealed class PingResult
{
    public string Host { get; init; } = "";
    public bool Success { get; init; }
    public long? RoundtripMs { get; init; }
    public string Status { get; init; } = "";
}

public sealed class NetworkSnapshot
{
    public IReadOnlyList<AdapterInfo> Adapters { get; init; } = [];
    public IReadOnlyList<PingResult> Pings { get; init; } = [];
    public bool InternetLikely { get; init; }
    public IReadOnlyList<Finding> Findings { get; init; } = [];
}

public static class NetworkingModule
{
    private const string Source = "native:network";
    public const string PublicDns = "1.1.1.1";

    public static NetworkSnapshot GetSnapshot()
    {
        var adapters = ReadAdapters();
        var gateway = adapters.SelectMany(a => a.Gateways).FirstOrDefault();
        var pings = new List<PingResult>
        {
            PingHost(PublicDns)
        };
        if (!string.IsNullOrWhiteSpace(gateway))
        {
            pings.Add(PingHost(gateway));
        }

        var internet = pings.Any(p => p.Host == PublicDns && p.Success);
        var findings = new List<Finding>();

        var primary = adapters.FirstOrDefault();
        if (primary is null)
        {
            findings.Add(Finding.Create(Sections.Network, Severity.Fail, "Sin adaptadores de red activos", Source));
        }
        else
        {
            findings.Add(Finding.Create(
                Sections.Network,
                primary.Addresses.Count > 0 ? Severity.Ok : Severity.Warning,
                $"IP: {(primary.Addresses.Count == 0 ? "ninguna" : string.Join(", ", primary.Addresses))}",
                Source,
                $"Adaptador {primary.Name}. DNS: {string.Join(", ", primary.Dns)}. Puerta de enlace: {string.Join(", ", primary.Gateways)}."));
        }

        foreach (var ping in pings)
        {
            findings.Add(Finding.Create(
                Sections.Network,
                ping.Success ? Severity.Ok : Severity.Fail,
                ping.Success ? $"Ping {ping.Host}: {ping.RoundtripMs} ms" : $"Ping {ping.Host} falló",
                Source,
                ping.Status));
        }

        findings.Add(Finding.Create(
            Sections.Network,
            internet ? Severity.Ok : Severity.Fail,
            internet ? "Hay conectividad a Internet (1.1.1.1)." : "No hay respuesta de 1.1.1.1.",
            Source));

        return new NetworkSnapshot
        {
            Adapters = adapters,
            Pings = pings,
            InternetLikely = internet,
            Findings = findings
        };
    }

    public static async Task<string> TraceRouteAsync(string host, CancellationToken cancellationToken = default)
    {
        var result = await CommandRunner.RunAsync(
            "tracert.exe",
            $"-d -h 15 {host}",
            requireAdmin: false,
            cancellationToken: cancellationToken).ConfigureAwait(false);
        if (!string.IsNullOrWhiteSpace(result.CombinedOutput))
        {
            return result.CombinedOutput;
        }

        return result.Message ?? $"Código {result.ExitCode}";
    }

    public static Task<CommandResult> FlushDnsAsync(CancellationToken cancellationToken = default) =>
        CommandRunner.RunAsync("ipconfig.exe", "/flushdns", requireAdmin: true, cancellationToken: cancellationToken);

    public static Task<CommandResult> RenewDhcpAsync(CancellationToken cancellationToken = default) =>
        CommandRunner.RunAsync("ipconfig.exe", "/renew", requireAdmin: true, cancellationToken: cancellationToken);

    public static Finding FromCommand(string title, CommandResult result)
    {
        if (result.Cancelled)
        {
            return Finding.Create(Sections.Network, Severity.Warning, title, Source, result.Message);
        }

        if (!result.Success)
        {
            var hint = Elevation.IsAdministrator
                ? result.Message ?? $"Código {result.ExitCode}."
                : (result.Message ?? $"Código {result.ExitCode}.") + " Esta acción suele requerir administrador.";
            return Finding.Create(Sections.Network, Severity.Fail, title, Source, hint + " " + Trim(result.CombinedOutput));
        }

        return Finding.Create(Sections.Network, Severity.Ok, title, Source, $"Código {result.ExitCode}. {Trim(result.CombinedOutput)}");
    }

    private static List<AdapterInfo> ReadAdapters()
    {
        var list = new List<AdapterInfo>();
        foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (nic.OperationalStatus != OperationalStatus.Up || nic.NetworkInterfaceType == NetworkInterfaceType.Loopback)
            {
                continue;
            }

            var props = nic.GetIPProperties();
            list.Add(new AdapterInfo
            {
                Name = nic.Name,
                Type = nic.NetworkInterfaceType.ToString(),
                Status = nic.OperationalStatus.ToString(),
                Addresses = props.UnicastAddresses
                    .Select(a => a.Address.ToString())
                    .Where(a => a.Contains('.'))
                    .ToList(),
                Dns = props.DnsAddresses.Select(a => a.ToString()).ToList(),
                Gateways = props.GatewayAddresses.Select(a => a.Address.ToString()).ToList()
            });
        }

        return list;
    }

    private static PingResult PingHost(string host)
    {
        try
        {
            using var ping = new Ping();
            var reply = ping.Send(host, 3000);
            var ok = reply.Status == IPStatus.Success;
            return new PingResult
            {
                Host = host,
                Success = ok,
                RoundtripMs = ok ? reply.RoundtripTime : null,
                Status = reply.Status.ToString()
            };
        }
        catch (Exception ex)
        {
            return new PingResult { Host = host, Success = false, Status = ex.Message };
        }
    }

    private static string Trim(string text)
    {
        text = text.Trim();
        return text.Length <= 800 ? text : text[..800] + "…";
    }
}
