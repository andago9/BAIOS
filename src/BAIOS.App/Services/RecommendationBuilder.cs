using BAIOS.Core;

namespace BAIOS.App.Services;

public static class RecommendationBuilder
{
    public static void Apply(Session session)
    {
        session.Recommendations.Clear();
        foreach (var finding in session.Findings.Where(f => f.Severity != Severity.Ok))
        {
            if (finding.Section == Sections.Storage && finding.Title.Contains("libre", StringComparison.OrdinalIgnoreCase))
            {
                session.AddRecommendation("Liberar espacio en disco.");
            }
            else if (finding.Section == Sections.Security && finding.Title.Contains("Defender", StringComparison.OrdinalIgnoreCase))
            {
                session.AddRecommendation("Revisar Microsoft Defender; no desactivarlo de forma permanente.");
            }
            else if (finding.Section == Sections.Security && finding.Title.Contains("Firewall", StringComparison.OrdinalIgnoreCase))
            {
                session.AddRecommendation("Activar el firewall de Windows en los perfiles en uso.");
            }
            else if (finding.Section == Sections.Network)
            {
                session.AddRecommendation("Comprobar cable/Wi‑Fi, DNS y puerta de enlace.");
            }
            else if (finding.Title.Contains("SMART", StringComparison.OrdinalIgnoreCase))
            {
                session.AddRecommendation("Hacer copia de seguridad: el disco puede estar fallando.");
            }
            else if (finding.Title.Contains("driver", StringComparison.OrdinalIgnoreCase))
            {
                session.AddRecommendation("Revisar administradores de dispositivos / drivers con error.");
            }
            else if (finding.Title.Contains("servicio", StringComparison.OrdinalIgnoreCase))
            {
                session.AddRecommendation("Revisar servicios de inicio automático detenidos.");
            }
        }

        if (session.Findings.Any(f => f.Title.Contains("inicio", StringComparison.OrdinalIgnoreCase)))
        {
            session.AddRecommendation("Revisar programas de inicio (Autoruns).");
        }
    }
}
