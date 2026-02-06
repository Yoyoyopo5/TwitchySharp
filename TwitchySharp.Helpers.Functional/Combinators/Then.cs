using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Functional;

public static partial class FunctionalExtensions
{
    // Step
    public static Step<TIn, TOut> Then<TIn, TMid, TOut>(this Step<TIn, TMid> a, Step<TMid, TOut> b)
        => async input => await b(await a(input));
    public static Step<TIn, TOut> Then<TIn, TOut>(this Step<TIn> a, Step<TIn, TOut> b)
        => async input => await b(await a(input));
    public static Step<TIn, TOut> Then<TIn, TOut>(this Step<TIn, TOut> a, Step<TOut> b)
        => async input => await b(await a(input));
    public static Step<T> Then<T>(this Step<T> a, Step<T> b)
        => async input => await b(await a(input));

    // Effect
    public static Step<TIn, TOut> Then<TIn, TOut>(this Step<TIn, TOut> step, Effect<TIn> effect)
        => async input =>
        {
            await effect(input);
            return await step(input);
        };
    public static Step<T> Then<T>(this Step<T> step, Effect<T> effect)
        => async input =>
        {
            await effect(input);
            return await step(input);
        };

    // Layer
    public static Step<TIn, TOut> Then<TIn, TOut>(this Step<TIn, TOut> core, Layer<TIn, TOut> layer)
        => layer(core);
    public static Step<T> Then<T>(this Step<T> core, Layer<T> layer)
        => layer(core);
}
