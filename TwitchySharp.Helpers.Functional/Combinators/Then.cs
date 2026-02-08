using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Functional;

public static partial class FunctionalExtensions
{
    /// <summary>Chains two steps, feeding the output of <paramref name="a"/> into <paramref name="b"/>.</summary>
    /// <param name="a">The first step to execute.</param>
    /// <param name="b">The second step, which receives the output of <paramref name="a"/>.</param>
    public static Step<TIn, TOut> Then<TIn, TMid, TOut>(this Step<TIn, TMid> a, Step<TMid, TOut> b)
        => async input => await b(await a(input));

    /// <summary>Chains a same-type step into a transforming step.</summary>
    /// <param name="a">The same-type step to execute first.</param>
    /// <param name="b">The transforming step, which receives the refined output of <paramref name="a"/>.</param>
    public static Step<TIn, TOut> Then<TIn, TOut>(this Step<TIn> a, Step<TIn, TOut> b)
        => async input => await b(await a(input));

    /// <summary>Chains a transforming step into a same-type step.</summary>
    /// <param name="a">The transforming step to execute first.</param>
    /// <param name="b">The same-type step, which refines the output of <paramref name="a"/>.</param>
    public static Step<TIn, TOut> Then<TIn, TOut>(this Step<TIn, TOut> a, Step<TOut> b)
        => async input => await b(await a(input));

    /// <summary>Chains two same-type steps together.</summary>
    /// <param name="a">The first same-type step.</param>
    /// <param name="b">The second same-type step.</param>
    public static Step<T> Then<T>(this Step<T> a, Step<T> b)
        => async input => await b(await a(input));

    /// <summary>Wraps a step in a layer, applying middleware behavior.</summary>
    /// <param name="core">The step to wrap.</param>
    /// <param name="layer">The middleware layer to apply.</param>
    public static Step<TIn, TOut> Then<TIn, TOut>(this Step<TIn, TOut> core, Layer<TIn, TOut> layer)
        => layer(core);

    /// <summary>Wraps a same-type step in a same-type layer.</summary>
    /// <param name="core">The step to wrap.</param>
    /// <param name="layer">The middleware layer to apply.</param>
    public static Step<T> Then<T>(this Step<T> core, Layer<T> layer)
        => layer(core);

    /// <summary>Wraps a same-type step in a heteromorphic layer where input and output share the same type.</summary>
    /// <remarks>Internally adapts between <see cref="Step{T}"/> and <see cref="Step{TIn, TOut}"/> so the caller does not need to convert manually.</remarks>
    /// <param name="core">The same-type step to wrap.</param>
    /// <param name="layer">The heteromorphic layer to apply.</param>
    public static Step<T> Then<T>(this Step<T> core, Layer<T, T> layer)
        => layer(core.Expand()).Contract();

    /// <summary>Wraps a heteromorphic step (where input and output share the same type) in a same-type layer.</summary>
    /// <remarks>Internally adapts between <see cref="Step{TIn, TOut}"/> and <see cref="Step{T}"/> so the caller does not need to convert manually.</remarks>
    /// <param name="core">The heteromorphic step to wrap.</param>
    /// <param name="layer">The same-type layer to apply.</param>
    public static Step<T, T> Then<T>(this Step<T, T> core, Layer<T> layer)
        => layer(core.Contract()).Expand();

    /// <summary>Composes two layers into a single layer.</summary>
    /// <remarks>In the composed layer, <paramref name="a"/> wraps first (innermost) and <paramref name="b"/> wraps second (outermost).</remarks>
    /// <param name="a">The inner layer.</param>
    /// <param name="b">The outer layer.</param>
    public static Layer<TIn, TOut> Then<TIn, TOut>(this Layer<TIn, TOut> a, Layer<TIn, TOut> b)
        => core => b(a(core));

    /// <summary>Composes two same-type layers into a single layer.</summary>
    /// <remarks>In the composed layer, <paramref name="a"/> wraps first (innermost) and <paramref name="b"/> wraps second (outermost).</remarks>
    /// <param name="a">The inner layer.</param>
    /// <param name="b">The outer layer.</param>
    public static Layer<T> Then<T>(this Layer<T> a, Layer<T> b)
        => core => b(a(core));
}
