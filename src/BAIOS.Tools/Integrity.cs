using System.Security.Cryptography;
using System.Text;

namespace BAIOS.Tools;

public static class Integrity
{
    public static string Sha256Hex(string filePath)
    {
        using var stream = File.OpenRead(filePath);
        var hash = SHA256.HashData(stream);
        return Convert.ToHexString(hash).ToLowerInvariant();
    }

    public static bool Matches(string filePath, string expectedHex)
    {
        if (!IsHexSha256(expectedHex))
        {
            return false;
        }

        return string.Equals(Sha256Hex(filePath), expectedHex.Trim(), StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsHexSha256(string? value) =>
        !string.IsNullOrWhiteSpace(value)
        && value.Length == 64
        && value.All(char.IsAsciiHexDigit);

    public static void Log(string toolsDirectory, string message)
    {
        try
        {
            Directory.CreateDirectory(toolsDirectory);
            var line = $"{DateTimeOffset.Now:yyyy-MM-dd HH:mm:ss zzz} {message}{Environment.NewLine}";
            File.AppendAllText(Path.Combine(toolsDirectory, "integrity.log"), line, Encoding.UTF8);
        }
        catch
        {
        }
    }
}
