using System.Management;

namespace BAIOS.Security;

internal static class WmiQuery
{
    public static List<Dictionary<string, object?>> Query(string scope, string wql)
    {
        var list = new List<Dictionary<string, object?>>();
        try
        {
            using var searcher = new ManagementObjectSearcher(new ManagementScope(scope), new ObjectQuery(wql));
            using var collection = searcher.Get();
            foreach (ManagementBaseObject item in collection)
            {
                using (item)
                {
                    var row = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
                    foreach (var property in item.Properties)
                    {
                        row[property.Name] = property.Value;
                    }

                    list.Add(row);
                }
            }
        }
        catch (ManagementException)
        {
        }
        catch (System.Runtime.InteropServices.COMException)
        {
        }

        return list;
    }

    public static string? Text(Dictionary<string, object?> row, string name) =>
        row.TryGetValue(name, out var value) && value is not null ? Convert.ToString(value) : null;

    public static bool Flag(Dictionary<string, object?> row, string name)
    {
        if (!row.TryGetValue(name, out var value) || value is null)
        {
            return false;
        }

        return value switch
        {
            bool b => b,
            int i => i != 0,
            uint u => u != 0,
            short s => s != 0,
            ushort us => us != 0,
            string text => text is "True" or "true" or "1",
            _ => Convert.ToBoolean(value)
        };
    }
}
