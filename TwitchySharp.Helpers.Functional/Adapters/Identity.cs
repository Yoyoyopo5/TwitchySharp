using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Functional;

public static partial class FunctionalExtensions
{
    /// <summary>Returns a step that passes its input through unchanged.</summary>
    /// <remarks>The neutral element for <c>.Then()</c> composition. Useful as a default or no-op placeholder in pipeline construction.</remarks>
    public static Step<T> Identity<T>()
        => (input, ct) => ValueTask.FromResult(input);
}
