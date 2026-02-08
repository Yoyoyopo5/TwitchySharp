using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Functional;

public static partial class FunctionalExtensions
{
    /// <summary>Wraps a synchronous value in a completed <see cref="ValueTask{T}"/>.</summary>
    /// <remarks>Useful inside step lambdas to return a sync result: <c>input => input.Length.AsValueTask()</c>.</remarks>
    /// <param name="value">The value to wrap.</param>
    public static ValueTask<T> AsValueTask<T>(this T value)
        => ValueTask.FromResult(value);
}
