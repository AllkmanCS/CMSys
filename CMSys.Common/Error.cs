namespace CMSys.Common;

public static class Error
{
    public static ArgumentException Argument() => new();

    public static ArgumentException Argument(string message) => new(message);

    public static ArgumentException Argument(string paramName, string message) => new(message, paramName);

    public static ArgumentNullException ArgumentNull() => new();

    public static ArgumentNullException ArgumentNull(string paramName) => new(paramName);
}