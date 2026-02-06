using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Functional;

public static partial class FunctionalExtensions
{
    public static Step<T> Contract<T>(this Step<T, T> step)
        => input => step(input);
}
