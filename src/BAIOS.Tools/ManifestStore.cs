using System.Text.Json;
using System.Text.RegularExpressions;

namespace BAIOS.Tools;

public sealed class ManifestLoadResult
{
    public ManifestDocument Document { get; init; } = new();
    public string Path { get; init; } = "";
    public bool FromFile { get; init; }
    public IReadOnlyList<string> Warnings { get; init; } = [];
}

public static class ManifestStore
{
    public const int SchemaVersion = 1;
    public const string FileName = "manifest.json";

    private static readonly Regex SafeId = new("^[a-z0-9][a-z0-9_-]{0,63}$", RegexOptions.CultureInvariant);
    private static readonly HashSet<string> Architectures = new(StringComparer.OrdinalIgnoreCase) { "x64", "x86", "arm64" };
    private static readonly HashSet<string> Categories = new(StringComparer.OrdinalIgnoreCase) { "adware", "scanner", "startup", "other" };

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true,
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static string PathBesideExe(string? baseDirectory = null) =>
        System.IO.Path.Combine(baseDirectory ?? AppContext.BaseDirectory, FileName);

    public static ManifestLoadResult Load(string? baseDirectory = null)
    {
        var path = PathBesideExe(baseDirectory);
        var warnings = new List<string>();
        ManifestDocument? document = null;
        var fromFile = false;

        if (File.Exists(path))
        {
            try
            {
                document = JsonSerializer.Deserialize<ManifestDocument>(File.ReadAllText(path), JsonOptions);
                fromFile = document is not null;
            }
            catch (Exception ex)
            {
                warnings.Add("No se pudo leer manifest.json: " + ex.Message);
            }
        }
        else
        {
            warnings.Add("No hay manifest.json junto al exe; se usa el núcleo embebido.");
        }

        document ??= Default();
        warnings.AddRange(Validate(document));

        if (document.SchemaVersion != SchemaVersion)
        {
            warnings.Add($"schemaVersion {document.SchemaVersion} no es {SchemaVersion}; se ignora el archivo y se usa el núcleo embebido.");
            document = Default();
            fromFile = false;
        }

        return new ManifestLoadResult
        {
            Document = document,
            Path = path,
            FromFile = fromFile,
            Warnings = warnings
        };
    }

    public static void EnsureRepository(string toolsDirectory, ManifestDocument document)
    {
        Directory.CreateDirectory(toolsDirectory);
        foreach (var tool in document.Tools)
        {
            if (!SafeId.IsMatch(tool.Id))
            {
                continue;
            }

            Directory.CreateDirectory(System.IO.Path.Combine(toolsDirectory, tool.Id));
        }
    }

    public static string ToolFolder(string toolsDirectory, string id) =>
        System.IO.Path.Combine(toolsDirectory, id);

    public static IReadOnlyList<string> Validate(ManifestDocument document)
    {
        var warnings = new List<string>();
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        if (document.SchemaVersion != SchemaVersion)
        {
            warnings.Add($"schemaVersion debe ser {SchemaVersion}.");
        }

        foreach (var tool in document.Tools)
        {
            if (!SafeId.IsMatch(tool.Id))
            {
                warnings.Add($"id no válido: '{tool.Id}'.");
                continue;
            }

            if (!seen.Add(tool.Id))
            {
                warnings.Add($"id duplicado: {tool.Id}.");
            }

            if (string.IsNullOrWhiteSpace(tool.Name))
            {
                warnings.Add($"{tool.Id}: falta name.");
            }

            if (string.IsNullOrWhiteSpace(tool.Version))
            {
                warnings.Add($"{tool.Id}: falta version.");
            }

            if (!Architectures.Contains(tool.Architecture))
            {
                warnings.Add($"{tool.Id}: architecture debe ser x64, x86 o arm64.");
            }

            if (!IsHttpsUrl(tool.Download) || !IsHttpsUrl(tool.HomeUrl) || !IsHttpsUrl(tool.LicenseUrl))
            {
                warnings.Add($"{tool.Id}: download, homeUrl y licenseUrl deben ser https.");
            }

            if (!Categories.Contains(tool.Category))
            {
                warnings.Add($"{tool.Id}: category desconocida.");
            }

            if (string.IsNullOrWhiteSpace(tool.Publisher))
            {
                warnings.Add($"{tool.Id}: falta publisher.");
            }

            if (string.IsNullOrWhiteSpace(tool.Sha256))
            {
                warnings.Add($"{tool.Id}: sha256 vacío; no se descargará ni se verificará.");
            }
            else if (!Integrity.IsHexSha256(tool.Sha256))
            {
                warnings.Add($"{tool.Id}: sha256 debe ser 64 hex minúsculas.");
            }
        }

        return warnings;
    }

    public static ManifestDocument Default() => new()
    {
        SchemaVersion = SchemaVersion,
        Tools =
        [
            new ManifestTool
            {
                Id = "adwcleaner",
                Name = "AdwCleaner",
                Version = "8.3.2.0",
                Architecture = "x64",
                Download = "https://www.malwarebytes.com/adwcleaner",
                Sha256 = "",
                Category = "adware",
                Publisher = "Malwarebytes",
                LicenseUrl = "https://www.malwarebytes.com/adwcleaner/eula",
                HomeUrl = "https://www.malwarebytes.com/adwcleaner",
                RequiresAdmin = true,
                Arguments = [],
                ResultHints = "Cuarentena PUP/adware; no borrar a ciegas.",
                Executable = "adwcleaner.exe",
                Portable = true
            },
            new ManifestTool
            {
                Id = "msert",
                Name = "Microsoft Safety Scanner",
                Version = "current",
                Architecture = "x64",
                Download = "https://go.microsoft.com/fwlink/?LinkId=212732",
                Sha256 = "",
                Category = "scanner",
                Publisher = "Microsoft",
                LicenseUrl = "https://privacy.microsoft.com/privacystatement",
                HomeUrl = "https://learn.microsoft.com/defender-endpoint/safety-scanner-download",
                RequiresAdmin = true,
                Arguments = [],
                ResultHints = "Segunda opinión; no sustituye a Defender. El binario caduca ~10 días.",
                Executable = "msert.exe",
                ExpiresDays = 10,
                Portable = true
            },
            new ManifestTool
            {
                Id = "autoruns",
                Name = "Autoruns",
                Version = "14.3",
                Architecture = "x64",
                Download = "https://download.sysinternals.com/files/Autoruns.zip",
                Sha256 = "",
                Category = "startup",
                Publisher = "Microsoft",
                LicenseUrl = "https://learn.microsoft.com/sysinternals/license",
                HomeUrl = "https://learn.microsoft.com/sysinternals/downloads/autoruns",
                RequiresAdmin = true,
                Arguments = [],
                ResultHints = "Inventario de inicio. No deshabilitar entradas de Microsoft sin identificarlas.",
                Executable = "Autoruns64.exe",
                Portable = true
            },
            new ManifestTool
            {
                Id = "hijackthis",
                Name = "HijackThis",
                Version = "2.10.0.14",
                Architecture = "x64",
                Download = "https://github.com/dragokas/hijackthis",
                Sha256 = "",
                Category = "startup",
                Publisher = "Dragokas",
                LicenseUrl = "https://www.gnu.org/licenses/old-licenses/gpl-2.0.html",
                HomeUrl = "https://github.com/dragokas/hijackthis",
                RequiresAdmin = true,
                Arguments = [],
                ResultHints = "Falsos positivos frecuentes. No deshabilitar ni borrar a ciegas.",
                ManualUrl = "https://dragokas.com/tools/help/hjt_tutorial.html",
                Executable = "HiJackThis.exe",
                Portable = true,
                FalsePositiveWarning = true
            },
            new ManifestTool
            {
                Id = "kvrt",
                Name = "Kaspersky Virus Removal Tool",
                Version = "current",
                Architecture = "x64",
                Download = "https://www.kaspersky.com/downloads/free-virus-removal-tool",
                Sha256 = "",
                Category = "scanner",
                Publisher = "Kaspersky",
                LicenseUrl = "https://www.kaspersky.com/downloads/free-virus-removal-tool",
                HomeUrl = "https://www.kaspersky.com/downloads/free-virus-removal-tool",
                RequiresAdmin = true,
                Arguments = [],
                ResultHints = "Portable. BAIOS no hospeda el binario. En EE. UU. la descarga puede no estar disponible.",
                ManualUrl = "https://support.kaspersky.com/help/kvrt/2024/en-us/",
                Executable = "KVRT.exe",
                Portable = true
            },
            new ManifestTool
            {
                Id = "hitmanpro",
                Name = "HitmanPro",
                Version = "3.8.23",
                Architecture = "x64",
                Download = "https://download.sophos.com/endpoint/clients/HitmanPro_x64.exe",
                Sha256 = "",
                Category = "scanner",
                Publisher = "Sophos",
                LicenseUrl = "https://www.sophos.com/en-us/legal/sophos-end-user-terms-of-use",
                HomeUrl = "https://www.hitmanpro.com/",
                RequiresAdmin = true,
                Arguments = [],
                ResultHints = "Prueba ~30 días. No es freeware ilimitado. Revisa la EULA de Sophos.",
                Executable = "HitmanPro_x64.exe",
                Portable = true,
                TrialDays = 30
            },
            new ManifestTool
            {
                Id = "zhpcleaner",
                Name = "ZHPCleaner",
                Version = "2021.8.24.323",
                Architecture = "x64",
                Download = "https://nicolascoolman.eu/en/zhpcleaner-officiel/",
                Sha256 = "",
                Category = "other",
                Publisher = "Nicolas Coolman",
                LicenseUrl = "https://nicolascoolman.eu/en/zhpcleaner-officiel/",
                HomeUrl = "https://nicolascoolman.eu/en/zhpcleaner-officiel/",
                RequiresAdmin = true,
                Arguments = [],
                ResultHints = "Descarga solo desde el sitio del autor. Defender/SmartScreen a menudo lo marca.",
                Executable = "ZHPCleaner.exe",
                Portable = true
            },
            new ManifestTool
            {
                Id = "eek",
                Name = "Emsisoft Emergency Kit",
                Version = "2020.3.2.10048",
                Architecture = "x64",
                Download = "https://www.emsisoft.com/en/emergency-kit/",
                Sha256 = "",
                Category = "scanner",
                Publisher = "Emsisoft",
                LicenseUrl = "https://www.emsisoft.com/en/emergency-kit/",
                HomeUrl = "https://www.emsisoft.com/en/emergency-kit/",
                RequiresAdmin = true,
                Arguments = [],
                ResultHints = "Kit portable. No ejecutar junto a Emsisoft Anti-Malware instalado. Gratis para uso privado.",
                Executable = "EmsisoftEmergencyKit.exe",
                Portable = true
            },
            new ManifestTool
            {
                Id = "spybot",
                Name = "Spybot Search & Destroy",
                Version = "2.7.64.0",
                Architecture = "x64",
                Download = "https://www.safer-networking.org/download/",
                Sha256 = "",
                Category = "scanner",
                Publisher = "Safer-Networking",
                LicenseUrl = "https://www.safer-networking.org/download/",
                HomeUrl = "https://www.safer-networking.org/download/",
                RequiresAdmin = true,
                Arguments = [],
                ResultHints = "Instalador, no portable. Requiere consentimiento antes de instalar.",
                Executable = "SpybotSetup.exe",
                Portable = false
            },
            new ManifestTool
            {
                Id = "malwarebytes",
                Name = "Malwarebytes",
                Version = "4.1.0.56",
                Architecture = "x64",
                Download = "https://www.malwarebytes.com/",
                Sha256 = "",
                Category = "scanner",
                Publisher = "Malwarebytes",
                LicenseUrl = "https://www.malwarebytes.com/eula",
                HomeUrl = "https://www.malwarebytes.com/",
                RequiresAdmin = true,
                Arguments = [],
                ResultHints = "Producto completo (instalador). Distinto de AdwCleaner. No embeber.",
                Executable = "MBSetup.exe",
                Portable = false
            }
        ]
    };

    public static bool IsHttpsUrl(string? url) =>
        !string.IsNullOrWhiteSpace(url)
        && Uri.TryCreate(url, UriKind.Absolute, out var uri)
        && uri.Scheme == Uri.UriSchemeHttps;

    public static ManifestLoadResult Parse(string json, string path)
    {
        var warnings = new List<string>();
        var document = JsonSerializer.Deserialize<ManifestDocument>(json, JsonOptions);
        if (document is null)
        {
            throw new InvalidOperationException("El JSON del manifiesto está vacío.");
        }

        warnings.AddRange(Validate(document));
        return new ManifestLoadResult
        {
            Document = document,
            Path = path,
            FromFile = true,
            Warnings = warnings
        };
    }
}
