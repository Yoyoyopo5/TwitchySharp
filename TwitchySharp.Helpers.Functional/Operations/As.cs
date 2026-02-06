using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Functional;

public static partial class FunctionalOperations
{
    public static Step<TIn, TOut?> As<TIn, TOut>()
    => input => ValueTask.FromResult((input as object) switch
    {
        TOut t => t,
        _ => default
    });

    public static Step<TIn, TOut?> As<TOut, TIn>(this Step<TIn> step)
        => input => step.Then(As<TIn, TOut>())(input);
}
