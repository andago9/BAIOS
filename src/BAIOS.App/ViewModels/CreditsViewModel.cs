using System.Windows.Input;
using BAIOS.App.Services;
using BAIOS.Tools;

namespace BAIOS.App.ViewModels;

public sealed class CreditsViewModel : ViewModelBase
{
    private CreditsViewModel(
        string title,
        string body,
        string? websiteUrl,
        string? forumUrl,
        string? licenseUrl,
        bool showGpl)
    {
        Title = title;
        Body = body;
        WebsiteUrl = websiteUrl;
        ForumUrl = forumUrl;
        LicenseUrl = licenseUrl;
        ShowGpl = showGpl;
        OpenWebsiteCommand = new RelayCommand(() => AppLinks.OpenUrl(WebsiteUrl), () => HasWebsite);
        OpenForumCommand = new RelayCommand(() => AppLinks.OpenUrl(ForumUrl), () => HasForum);
        OpenLicenseUrlCommand = new RelayCommand(() => AppLinks.OpenUrl(LicenseUrl), () => HasLicenseUrl);
        OpenGplCommand = new RelayCommand(AppLinks.OpenLicense, () => ShowGpl);
    }

    public string Title { get; }
    public string Body { get; }
    public string? WebsiteUrl { get; }
    public string? ForumUrl { get; }
    public string? LicenseUrl { get; }
    public bool ShowGpl { get; }
    public bool HasWebsite => !string.IsNullOrWhiteSpace(WebsiteUrl);
    public bool HasForum => !string.IsNullOrWhiteSpace(ForumUrl);
    public bool HasLicenseUrl => !string.IsNullOrWhiteSpace(LicenseUrl);
    public ICommand OpenWebsiteCommand { get; }
    public ICommand OpenForumCommand { get; }
    public ICommand OpenLicenseUrlCommand { get; }
    public ICommand OpenGplCommand { get; }

    public static CreditsViewModel ForApp() => new(
        "Créditos",
        "BAIOS (Blinter All In One Security) es un proyecto de Andago.\n\n" +
        "Las herramientas de terceros son propiedad de sus autores. BAIOS solo las lanza; cada fabricante aplica su EULA. " +
        "El uso de esas herramientas es bajo tu responsabilidad.",
        AppLinks.Website,
        AppLinks.Forum,
        licenseUrl: null,
        showGpl: true);

    public static CreditsViewModel ForTool(ToolCard card) => new(
        "Créditos — " + card.Name,
        card.Name + " es de " + card.Vendor + ".\n\n" +
        "El software es propiedad de su autor. BAIOS solo lo lanza; cada fabricante aplica su propia EULA.",
        card.Url,
        forumUrl: null,
        card.LicenseUrl,
        showGpl: false);
}
