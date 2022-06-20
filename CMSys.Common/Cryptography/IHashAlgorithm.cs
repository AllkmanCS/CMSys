namespace CMSys.Common.Cryptography;

public interface IHashAlgorithm
{
    /// <exception cref="System.ArgumentNullException"><paramref name="text" /> is null.</exception>
    /// <exception cref="System.ArgumentException">Length of <paramref name="text" /> nameof(text).</exception>
    string CalculateHash(string text);
}