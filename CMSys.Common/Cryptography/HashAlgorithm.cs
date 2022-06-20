namespace CMSys.Common.Cryptography;

public abstract class HashAlgorithm : IHashAlgorithm
{
    public string CalculateHash(string text)
    {
        Check.ArgumentNotNull(text, nameof(text));
        Check.ArgumentSatisfies(text, x => x.Length > 0, "Length must be > 0.", nameof(text));

        return CalculateHashInternal(text);
    }

    protected abstract string CalculateHashInternal(string text);
}