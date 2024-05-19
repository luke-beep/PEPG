using System.Security.Cryptography;
using System.Text;

namespace PEPG.Helpers;

public class HashHelper
{
    public static string CalculateMd5Hash(byte[] data)
    {
        var hash = MD5.HashData(data);
        var sb = new StringBuilder();
        foreach (var b in hash) sb.Append(b.ToString("x2"));
        return sb.ToString();
    }

    public static string CalculateBase64Representation(byte[] data)
    {
        return Convert.ToBase64String(data);
    }

    public static string CalculateSha256Hash(byte[] data)
    {
        var hash = SHA256.HashData(data);
        return BitConverter.ToString(hash).Replace("-", "").ToUpper();
    }
}