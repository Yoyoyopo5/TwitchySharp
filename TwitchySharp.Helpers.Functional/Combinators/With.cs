using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Functional;

public static partial class FunctionalExtensions
{
    public static ValueTask<TOut> With<TIn, TOut>(this TIn input, Step<TIn, TOut> step)
        => step(input);
    public static ValueTask<TOut> With<TIn, TOut>(this Step<TIn, TOut> step, TIn input)
        => step(input);
}
