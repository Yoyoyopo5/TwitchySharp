using System;
using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Functional;

public static partial class FunctionalExtensions
{
    /// <summary>Converts a synchronous <see cref="Action{T}"/> into an async <see cref="Effect{TIn}"/>.</summary>
    /// <remarks>The resulting effect completes synchronously. Use with <c>.Tap()</c> or <c>.TapInput()</c> to attach to a pipeline.</remarks>
    /// <param name="effect">The synchronous action to adapt.</param>
    public static Effect<T> AsEffect<T>(this Action<T> effect)
        => input =>
        {
            effect(input);
            return ValueTask.CompletedTask;
        };
}
