using System.Security.Principal;

namespace BAIOS.Core;

public static class Elevation
{
    public static bool IsAdministrator
    {
        get
        {
            using var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
