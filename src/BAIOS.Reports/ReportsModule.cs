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
        var sb = new StringBuilder();
        sb.AppendLine("<!DOCTYPE html><html lang=\"es\"><head><meta charset=\"utf-8\"/>");
        sb.AppendLine("<title>Informe BAIOS</title>");
        sb.AppendLine("<style>body{font-family:Segoe UI,sans-serif;margin:24px;color:#222}h1{margin-bottom:0}table{border-collapse:collapse;width:100%;margin:12px 0}td,th{border:1px solid #ccc;padding:6px 8px;text-align:left}.ok{color:#1b7a3d}.warn{color:#9a6b00}.fail{color:#a11}.meta{color:#555}</style>");
        sb.AppendLine("</head><body>");
        sb.AppendLine("<h1>BAIOS — Blinter All In One Security</h1>");
        sb.AppendLine($"<p class=\"meta\">{WebUtility.HtmlEncode(HeaderLine(session))}</p>");
        foreach (var section in SectionOrder)
        {
            sb.AppendLine($"<h2>{SectionTitles[section]}</h2>");
            var items = session.Findings.Where(f => f.Section == section).ToList();
            if (items.Count == 0)
            {
                sb.AppendLine("<p>(sin datos en esta sesión)</p>");
                continue;
            }

            sb.AppendLine("<table><tr><th>Estado</th><th>Hallazgo</th><th>Detalle</th><th>Fuente</th></tr>");
            foreach (var item in items)
            {
                var css = item.Severity switch { Severity.Ok => "ok", Severity.Warning => "warn", _ => "fail" };
                sb.AppendLine($"<tr><td class=\"{css}\">{Label(item.Severity)}</td><td>{WebUtility.HtmlEncode(item.Title)}</td><td>{WebUtility.HtmlEncode(item.Detail ?? "")}</td><td>{WebUtility.HtmlEncode(item.Source)}</td></tr>");
            }

            sb.AppendLine("</table>");
        }

        sb.AppendLine("<h2>Recomendaciones</h2><ul>");
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

        sb.AppendLine("</ul></body></html>");
        return sb.ToString();
    }

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
}
