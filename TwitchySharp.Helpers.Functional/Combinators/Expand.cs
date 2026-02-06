using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Functional;

public static partial class FunctionalExtensions
{
    public static Step<T, T> Expand<T>(this Step<T> step)
        => input => step(input);
}
