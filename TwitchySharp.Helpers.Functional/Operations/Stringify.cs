using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Functional;

public static partial class FunctionalOperations
{
    public static Step<T, string?> Stringify<T>()
        => input => (input?.ToString()).AsValueTask();

    public static Step<T, string?> Stringify<T>(this Step<T> step)
        => async input => await Stringify<T>()(await step(input));
}
