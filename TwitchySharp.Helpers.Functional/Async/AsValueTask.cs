using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Functional;

public static partial class FunctionalExtensions
{
    public static ValueTask<T> AsValueTask<T>(this T value)
        => ValueTask.FromResult(value);

    public static Effect<T> AsEffect<T>(this Action<T> effect)
        => input =>
        {
            effect(input);
            return ValueTask.CompletedTask;
        };
}