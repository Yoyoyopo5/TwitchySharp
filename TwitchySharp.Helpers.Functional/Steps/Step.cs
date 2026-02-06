using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Functional;

public delegate ValueTask<TOut> Step<TIn, TOut>(TIn @in);

public delegate ValueTask<T> Step<T>(T @in);