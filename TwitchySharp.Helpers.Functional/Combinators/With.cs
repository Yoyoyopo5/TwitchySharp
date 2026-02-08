using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Functional;

public static partial class FunctionalExtensions
{
    /// <summary>Executes a step by piping a value into it.</summary>
    /// <remarks>The primary way to invoke a pipeline: <c>input.With(pipeline)</c>.</remarks>
    /// <param name="input">The value to pipe into the step.</param>
    /// <param name="step">The step or composed pipeline to execute.</param>
    public static ValueTask<TOut> With<TIn, TOut>(this TIn input, Step<TIn, TOut> step)
        => step(input);
}
