using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Functional;

public static partial class FunctionalExtensions
{
    /// <summary>Runs a side effect on the step's input before the step executes.</summary>
    /// <remarks>The effect observes the input without altering it. The step still receives the original input unchanged.</remarks>
    /// <param name="step">The step to decorate.</param>
    /// <param name="effect">The side effect to run on the input.</param>
    public static Step<TIn, TOut> TapInput<TIn, TOut>(this Step<TIn, TOut> step, Effect<TIn> effect)
        => async input =>
        {
            await effect(input);
            return await step(input);
        };

    /// <summary>Runs a side effect on the step's input before the step executes.</summary>
    /// <remarks>Same-type specialization of <see cref="TapInput{TIn, TOut}"/>.</remarks>
    /// <param name="step">The step to decorate.</param>
    /// <param name="effect">The side effect to run on the input.</param>
    public static Step<T> TapInput<T>(this Step<T> step, Effect<T> effect)
        => async input =>
        {
            await effect(input);
            return await step(input);
        };

    /// <summary>Runs a side effect on the step's output after the step executes.</summary>
    /// <remarks>The effect observes the output without altering it. The original result is returned unchanged.</remarks>
    /// <param name="step">The step to decorate.</param>
    /// <param name="effect">The side effect to run on the output.</param>
    public static Step<TIn, TOut> Tap<TIn, TOut>(this Step<TIn, TOut> step, Effect<TOut> effect)
        => async input =>
        {
            TOut result = await step(input);
            await effect(result);
            return result;
        };

    /// <summary>Runs a side effect on the step's output after the step executes.</summary>
    /// <remarks>Same-type specialization of <see cref="Tap{TIn, TOut}"/>.</remarks>
    /// <param name="step">The step to decorate.</param>
    /// <param name="effect">The side effect to run on the output.</param>
    public static Step<T> Tap<T>(this Step<T> step, Effect<T> effect)
        => async input =>
        {
            T result = await step(input);
            await effect(result);
            return result;
        };
}
