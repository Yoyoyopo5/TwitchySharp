namespace TwitchySharp.Helpers.Functional;

public static partial class FunctionalExtensions
{
    /// <summary>Converts a <see cref="Step{TIn, TOut}"/> (where both type parameters are the same) into a <see cref="Step{T}"/>.</summary>
    /// <param name="step">The heteromorphic step to narrow.</param>
    internal static Step<T> Contract<T>(this Step<T, T> step)
        => (input, ct) => step(input, ct);
}
