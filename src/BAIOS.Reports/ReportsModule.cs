using System.Net;
using System.Text;
using BAIOS.Core;

namespace BAIOS.Reports;

public sealed class ReportFiles
{
    public required string HtmlPath { get; init; }
    public required string TxtPath { get; init; }
}

public static class ReportsModule
{
    private const string ProductSite = "https://sites.google.com/view/blinter-baios";
    private static readonly string? LogoDataUri = LoadLogoDataUri();

    private static readonly string[] SectionOrder =
    [
        Sections.Security,
        Sections.Storage,
        Sections.Network,
        Sections.System,
        Sections.Maintenance
    ];

    private static readonly Dictionary<string, string> SectionTitles = new()
    {
        [Sections.Security] = "Seguridad",
        [Sections.Storage] = "Almacenamiento",
        [Sections.Network] = "Red",
        [Sections.System] = "Sistema",
        [Sections.Maintenance] = "Mantenimiento"
    };

    public static IReadOnlyList<ReportFiles> ListReports(string reportsDirectory)
    {
        if (!Directory.Exists(reportsDirectory))
        {
            return [];
        }

        return Directory.EnumerateFiles(reportsDirectory, "BAIOS-*.html")
            .OrderByDescending(File.GetLastWriteTime)
            .Select(html => new ReportFiles
            {
                HtmlPath = html,
                TxtPath = Path.ChangeExtension(html, ".txt")
            })
            .ToList();
    }

    public static ReportFiles Write(Session session, string reportsDirectory)
    {
        Directory.CreateDirectory(reportsDirectory);
        var stamp = session.StartedAt.ToLocalTime().ToString("yyyyMMdd-HHmm");
        var safeHost = string.Join("_", session.Hostname.Split(Path.GetInvalidFileNameChars()));
        var baseName = $"BAIOS-{safeHost}-{stamp}";
        var htmlPath = Path.Combine(reportsDirectory, baseName + ".html");
        var txtPath = Path.Combine(reportsDirectory, baseName + ".txt");

        File.WriteAllText(txtPath, BuildTxt(session), Encoding.UTF8);
        File.WriteAllText(htmlPath, BuildHtml(session), Encoding.UTF8);
        return new ReportFiles { HtmlPath = htmlPath, TxtPath = txtPath };
    }

    public static string BuildTxt(Session session)
    {
        var sb = new StringBuilder();
        AppendHeader(sb, session, html: false);
        foreach (var section in SectionOrder)
        {
            sb.AppendLine(SectionTitles[section].ToUpperInvariant());
            var items = session.Findings.Where(f => f.Section == section).ToList();
            if (items.Count == 0)
            {
                sb.AppendLine("  (sin datos en esta sesión)");
            }
            else
            {
                foreach (var item in items)
                {
                    sb.AppendLine($"  {Label(item.Severity)}  {item.Title}");
                    if (!string.IsNullOrWhiteSpace(item.Detail))
                    {
                        sb.AppendLine($"      {item.Detail}");
                    }
                }
            }

            sb.AppendLine();
        }

        sb.AppendLine("RECOMENDACIONES");
        if (session.Recommendations.Count == 0)
        {
            sb.AppendLine("  Ninguna adicional.");
        }
        else
        {
            foreach (var rec in session.Recommendations)
            {
                sb.AppendLine($"- {rec}");
            }
        }

        return sb.ToString();
    }

    public static string BuildHtml(Session session)
    {
        var ok = session.Findings.Count(f => f.Severity == Severity.Ok);
        var warn = session.Findings.Count(f => f.Severity == Severity.Warning);
        var fail = session.Findings.Count(f => f.Severity == Severity.Fail);
        var mode = session.Mode == AppMode.Home ? "Hogar" : "Técnico";
        var host = WebUtility.HtmlEncode(session.Hostname);
        var when = session.StartedAt.ToLocalTime().ToString("yyyy-MM-dd HH:mm");
        var version = WebUtility.HtmlEncode(session.EngineVersion);
        var logo = string.IsNullOrEmpty(LogoDataUri)
            ? ""
            : $"<img src=\"{LogoDataUri}\" alt=\"BAIOS\"/>";

        var sb = new StringBuilder();
        sb.AppendLine("<!DOCTYPE html>");
        sb.AppendLine("<html lang=\"es\">");
        sb.AppendLine("<head>");
        sb.AppendLine("<meta charset=\"utf-8\"/>");
        sb.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"/>");
        sb.AppendLine($"<title>Informe BAIOS — {host}</title>");
        sb.AppendLine("<style>");
        sb.AppendLine(HtmlCss);
        sb.AppendLine("</style>");
        sb.AppendLine("</head>");
        sb.AppendLine("<body>");
        sb.AppendLine("<div class=\"wrap\">");
        sb.AppendLine("<header class=\"hero\">");
        sb.AppendLine($"<div class=\"brand\">{logo}<div><h1>BAIOS</h1><p class=\"tag\">Blinter All In One Security</p></div></div>");
        sb.AppendLine("<p class=\"hero-mark\">Informe de sesión</p>");
        sb.AppendLine("</header>");
        sb.AppendLine("<section class=\"meta\">");
        sb.AppendLine($"<div class=\"chip\"><span class=\"k\">Equipo</span><span class=\"v\">{host}</span></div>");
        sb.AppendLine($"<div class=\"chip\"><span class=\"k\">Modo</span><span class=\"v\">{mode}</span></div>");
        sb.AppendLine($"<div class=\"chip\"><span class=\"k\">Fecha</span><span class=\"v\">{when}</span></div>");
        sb.AppendLine($"<div class=\"chip\"><span class=\"k\">Motor</span><span class=\"v\">{version}</span></div>");
        sb.AppendLine("</section>");
        sb.AppendLine("<section class=\"summary\">");
        sb.AppendLine($"<div class=\"pill ok\"><span class=\"n\">{ok}</span><span>OK</span></div>");
        sb.AppendLine($"<div class=\"pill warn\"><span class=\"n\">{warn}</span><span>Avisos</span></div>");
        sb.AppendLine($"<div class=\"pill fail\"><span class=\"n\">{fail}</span><span>Fallos</span></div>");
        sb.AppendLine("</section>");

        foreach (var section in SectionOrder)
        {
            var items = session.Findings.Where(f => f.Section == section).ToList();
            sb.AppendLine("<section class=\"card\">");
            sb.AppendLine($"<h2>{SectionTitles[section]}</h2>");
            if (items.Count == 0)
            {
                sb.AppendLine("<p class=\"empty\">Sin datos en esta sesión.</p>");
            }
            else
            {
                sb.AppendLine("<table><thead><tr><th>Estado</th><th>Hallazgo</th><th>Detalle</th><th>Fuente</th></tr></thead><tbody>");
                foreach (var item in items)
                {
                    var css = Css(item.Severity);
                    sb.AppendLine(
                        $"<tr><td><span class=\"badge {css}\">{Label(item.Severity)}</span></td>" +
                        $"<td>{WebUtility.HtmlEncode(item.Title)}</td>" +
                        $"<td>{WebUtility.HtmlEncode(item.Detail ?? "")}</td>" +
                        $"<td class=\"src\">{WebUtility.HtmlEncode(item.Source)}</td></tr>");
                }

                sb.AppendLine("</tbody></table>");
            }

            sb.AppendLine("</section>");
        }

        sb.AppendLine("<section class=\"card recs\">");
        sb.AppendLine("<h2>Recomendaciones</h2>");
        sb.AppendLine("<ul>");
        if (session.Recommendations.Count == 0)
        {
            sb.AppendLine("<li>Ninguna adicional.</li>");
        }
        else
        {
            foreach (var rec in session.Recommendations)
            {
                sb.AppendLine($"<li>{WebUtility.HtmlEncode(rec)}</li>");
            }
        }

        sb.AppendLine("</ul>");
        sb.AppendLine("</section>");
        sb.AppendLine("<footer>");
        sb.AppendLine($"<div class=\"brand small\">{logo}<div>");
        sb.AppendLine("<strong>BAIOS</strong> · Andago · GNU GPL v3");
        sb.AppendLine($"<div><a href=\"{ProductSite}\">{ProductSite}</a></div>");
        sb.AppendLine("</div></div>");
        sb.AppendLine("<p>BAIOS no es un antivirus. No elimines archivos sin saber qué son.</p>");
        sb.AppendLine("</footer>");
        sb.AppendLine("</div></body></html>");
        return sb.ToString();
    }

    private const string HtmlCss = """
        :root{--bg:#eef1f7;--card:#fff;--ink:#1e1e24;--muted:#5c5c6b;--accent:#3d8bff;--ok:#1b7a3d;--warn:#9a6b00;--fail:#b42318;--line:#e3e6ee}
        *{box-sizing:border-box}
        body{margin:0;background:var(--bg);color:var(--ink);font:15px/1.5 "Segoe UI",system-ui,sans-serif}
        .wrap{max-width:960px;margin:0 auto;padding:32px 20px 48px}
        .hero{display:flex;align-items:center;justify-content:space-between;gap:16px;background:#1e1e24;color:#f2f2f2;border-radius:16px;padding:28px 32px}
        .brand{display:flex;align-items:center;gap:16px}
        .brand img{width:72px;height:72px;flex-shrink:0}
        .brand.small img{width:36px;height:36px}
        .hero h1{margin:0;font-size:32px;font-weight:650;letter-spacing:.04em}
        .tag,.hero-mark{margin:4px 0 0;color:#a0a0b0}
        .hero-mark{margin:0;font-size:13px;letter-spacing:.08em;text-transform:uppercase}
        .meta,.summary{display:grid;gap:12px;margin:20px 0 0}
        .meta{grid-template-columns:repeat(auto-fit,minmax(160px,1fr))}
        .summary{grid-template-columns:repeat(3,1fr);margin-bottom:8px}
        .chip,.pill,.card{background:var(--card);border-radius:14px;box-shadow:0 1px 2px rgba(30,30,36,.06)}
        .chip{padding:12px 16px}
        .chip .k{display:block;font-size:11px;color:var(--muted);text-transform:uppercase;letter-spacing:.06em}
        .chip .v{font-weight:600}
        .pill{padding:16px;text-align:center;display:flex;flex-direction:column;gap:2px}
        .pill .n{font-size:28px;font-weight:700;line-height:1.1}
        .pill.ok .n{color:var(--ok)}
        .pill.warn .n{color:var(--warn)}
        .pill.fail .n{color:var(--fail)}
        .card{padding:20px 24px;margin:16px 0}
        .card h2{margin:0 0 12px;font-size:18px}
        table{width:100%;border-collapse:collapse}
        th{text-align:left;font-size:11px;color:var(--muted);text-transform:uppercase;letter-spacing:.04em;padding:8px 10px;border-bottom:1px solid var(--line)}
        td{padding:10px;border-bottom:1px solid var(--line);vertical-align:top}
        tr:last-child td{border-bottom:none}
        .src{color:var(--muted);font-size:13px;word-break:break-all}
        .badge{display:inline-block;font-size:11px;font-weight:700;letter-spacing:.04em;padding:3px 8px;border-radius:999px}
        .badge.ok{background:#e6f6ec;color:var(--ok)}
        .badge.warn{background:#fff6dd;color:var(--warn)}
        .badge.fail{background:#fde8e6;color:var(--fail)}
        .empty{color:var(--muted);margin:0 0 8px}
        .recs{background:#eef5ff}
        .recs ul{margin:0 0 8px;padding-left:18px}
        .recs li{margin:8px 0}
        footer{display:flex;align-items:center;justify-content:space-between;gap:16px;margin-top:24px;color:var(--muted);font-size:13px}
        footer a{color:var(--accent);text-decoration:none}
        footer a:hover{text-decoration:underline}
        footer p{margin:0;max-width:320px;text-align:right}
        @media print{
          body{background:#fff}
          .hero,.chip,.pill,.card{box-shadow:none}
          .hero{border-radius:0}
        }
        @media (max-width:640px){
          .hero,footer{flex-direction:column;align-items:flex-start}
          footer p{text-align:left}
          .summary{grid-template-columns:1fr}
        }
        """;

    private static void AppendHeader(StringBuilder sb, Session session, bool html)
    {
        _ = html;
        sb.AppendLine("BAIOS — Blinter All In One Security");
        sb.AppendLine(HeaderLine(session));
        sb.AppendLine();
    }

    private static string HeaderLine(Session session)
    {
        var mode = session.Mode == AppMode.Home ? "Hogar" : "Técnico";
        return $"Equipo: {session.Hostname} · {mode} · {session.StartedAt.ToLocalTime():yyyy-MM-dd HH:mm} · motor {session.EngineVersion}";
    }

    private static string Label(Severity severity) => severity switch
    {
        Severity.Ok => "OK",
        Severity.Warning => "AVISO",
        _ => "FALLO"
    };

    private static string Css(Severity severity) => severity switch
    {
        Severity.Ok => "ok",
        Severity.Warning => "warn",
        _ => "fail"
    };

    private static string? LoadLogoDataUri()
    {
        using var stream = typeof(ReportsModule).Assembly.GetManifestResourceStream("BAIOS.logo.png");
        if (stream is null)
        {
            return null;
        }

        using var ms = new MemoryStream();
        stream.CopyTo(ms);
        return "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
    }
}
