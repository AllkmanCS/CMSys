using System.Security.Cryptography;
using System.Text;
using CMSys.Common.Cryptography;

namespace CMSys.Common.Helpers;

public static class PasswordHelper
{
    /// <exception cref="System.ArgumentException"><paramref name="length" /> must be > 0.</exception>
    public static string GenerateSalt(int length)
    {
        Check.ArgumentSatisfies(length, x => x > 0, "Value must be > 0.", nameof(length));

        var randomBytes = RandomNumberGenerator.GetBytes(length);
        return Encoding.Unicode.GetString(randomBytes);
    }

    /// <exception cref="System.ArgumentNullException"></exception>
    public static string ComputeHash(string password, string salt)
    {
        Check.ArgumentNotNull(password, nameof(password));
        Check.ArgumentNotNull(salt, nameof(salt));

        var hashAlgorithm = new Sha512Hash();
        return hashAlgorithm.CalculateHash(password + salt);
    }

    /// <exception cref="System.ArgumentException"><paramref name="length" /> must be > 0.</exception>
    public static string Generate(int length)
    {
        Check.ArgumentSatisfies(length, x => x > 0, "Value must be > 0.", nameof(length));

        var randomString = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Trim();
        return length <= randomString.Length ? randomString[..length] : randomString;
    }
}