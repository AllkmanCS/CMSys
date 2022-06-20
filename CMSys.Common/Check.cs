namespace CMSys.Common;

public static class Check
{
    /// <summary>
    ///     Throws <see cref="System.ArgumentNullException" /> if the given argument is null.
    /// </summary>
    /// <param name="argumentValue">Argument value to test.</param>
    /// <param name="argumentName">Name of the argument being tested (optional).</param>
    /// <exception cref="System.ArgumentNullException"><paramref name="argumentValue" /> is null.</exception>
    public static void ArgumentNotNull<T>(T? argumentValue, string? argumentName = "") where T : class
    {
        if (argumentValue is not null)
        {
            return;
        }

        if (string.IsNullOrEmpty(argumentName))
        {
            throw Error.ArgumentNull();
        }

        throw Error.ArgumentNull(argumentName);
    }

    /// <summary>
    ///     Throws <see cref="System.ArgumentException" /> if the given argument does not satisfy the condition.
    /// </summary>
    /// <typeparam name="T">The type of the first argument.</typeparam>
    /// <param name="argumentValue">Argument value to test.</param>
    /// <param name="condition">Predicate function that describes the condition.</param>
    /// <param name="violationMessage">Message in case of violation of the condition (optional).</param>
    /// <param name="argumentName">Name of the argument being tested (optional).</param>
    /// <exception cref="System.ArgumentException">
    ///     <paramref name="argumentValue" /> does not satisfy <paramref name="condition" />.
    /// </exception>
    public static void ArgumentSatisfies<T>(T argumentValue, Func<T, bool>? condition,
        string? violationMessage = "", string? argumentName = "")
    {
        if (condition is null)
        {
            return;
        }

        if (condition(argumentValue))
        {
            return;
        }

        if (string.IsNullOrEmpty(violationMessage))
        {
            throw Error.Argument();
        }

        if (string.IsNullOrEmpty(argumentName))
        {
            throw Error.Argument(violationMessage);
        }

        throw Error.Argument(argumentName, violationMessage);
    }
}