using System.Security.Cryptography;
using System.Text;

namespace AzureMcp.Helpers;

internal static class Sha256Helper
{
    private static readonly SHA256 s_sHA256 = SHA256.Create();
    private static readonly Encoding s_encoding = Encoding.UTF8;

    internal static string GetHashedValue(string contents)
    {
        if (string.IsNullOrEmpty(contents))
        {
            return string.Empty;
        }

        var bytes = s_sHA256.ComputeHash(s_encoding.GetBytes(contents));
        return string.Join(string.Empty, bytes.Select(x => x.ToString("x2")));
    }
}
