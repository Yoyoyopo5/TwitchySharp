using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Functional;

public static partial class FunctionalExtensions
{
    /// <summary>Converts a step into an effect by discarding its output.</summary>
    /// <remarks>The step still executes fully, but the result is thrown away. Useful for fire-and-forget scenarios at the end of a pipeline.</remarks>
    /// <param name="step">The step whose output will be discarded.</param>
    public static Effect<TIn> Ignore<TIn, TOut>(this Step<TIn, TOut> step)
        => async (input, ct) => { await step(input, ct); };
}
