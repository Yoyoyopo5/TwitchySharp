namespace TwitchySharp.Helpers.Functional;

public static partial class FunctionalExtensions
{
    /// <summary>Converts a <see cref="Step{T}"/> into a <see cref="Step{TIn, TOut}"/> where both type parameters are the same.</summary>
    /// <param name="step">The same-type step to widen.</param>
    internal static Step<T, T> Expand<T>(this Step<T> step)
        => (input, ct) => step(input, ct);
}
