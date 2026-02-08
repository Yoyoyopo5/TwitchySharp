using System;
using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Functional;

public static partial class FunctionalExtensions
{
    /// <summary>Lifts a synchronous function into an async <see cref="Step{TIn, TOut}"/>.</summary>
    /// <remarks>Avoids the need to manually wrap return values with <c>.AsValueTask()</c> in every lambda.</remarks>
    /// <param name="function">The synchronous function to adapt.</param>
    public static Step<TIn, TOut> AsStep<TIn, TOut>(this Func<TIn, TOut> function)
        => input => ValueTask.FromResult(function(input));

    /// <summary>Lifts a synchronous same-type function into an async <see cref="Step{T}"/>.</summary>
    /// <remarks>When <typeparamref name="T"/> is the same for input and output, use <c>FunctionalExtensions.AsStep&lt;T&gt;(func)</c> to disambiguate from the heteromorphic overload.</remarks>
    /// <param name="function">The synchronous function to adapt.</param>
    public static Step<T> AsStep<T>(this Func<T, T> function)
        => input => ValueTask.FromResult(function(input));
}
