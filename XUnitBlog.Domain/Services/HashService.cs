using System.Security.Cryptography;
using System.Text;

namespace XUnitBlog.Domain.Services;

public class HashService : IHashService
{
    public string CreateHash(string input)
    {
        if (string.IsNullOrEmpty(input))
            throw new ArgumentException("The input cannot be empty");

        using (var sha512 = SHA512.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha512.ComputeHash(bytes);
            return BytesToHexString(hash);
        }
    }

    private string BytesToHexString(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (var b in bytes)
            sb.Append(b.ToString("x2"));
        return sb.ToString();
    }

    public bool CompareHash(string text, string compareHash)
    {
        var newHash = CreateHash(text);
        return StringComparer.OrdinalIgnoreCase.Compare(newHash, compareHash) == 0;
    }
}
