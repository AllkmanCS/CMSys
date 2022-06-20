using System.Security.Cryptography;
using System.Text;

namespace CMSys.Common.Cryptography;

public sealed class Sha512Hash : HashAlgorithm
{
    protected override string CalculateHashInternal(string text)
    {
        var bytes = Encoding.Unicode.GetBytes(text);
        var hashed = SHA512.HashData(bytes);
        return Encoding.Unicode.GetString(hashed);
    }
}