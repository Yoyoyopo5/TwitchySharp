using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Functional;

public static partial class FunctionalOperations
{
    public static Layer<TIn, TOut> Log<TIn, TOut>()
    => next => async input =>
    {
        Console.Write($"{input} => ");
        TOut output = await next(input);
        Console.WriteLine(output);
        return output;
    };

    public static Effect<T> Log<T>()
        => input =>
        {
            Console.WriteLine(input);
            return ValueTask.CompletedTask;
        };

    public static Step<TIn, TOut> Log<TIn, TOut>(this Step<TIn, TOut> step)
        => step.Then(Log);
}
