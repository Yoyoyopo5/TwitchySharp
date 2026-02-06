using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Functional;

public delegate Step<TIn, TOut> Layer<TIn, TOut>(Step<TIn, TOut> next);
public delegate Step<T> Layer<T>(Step<T> next);