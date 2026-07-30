using System;
using System.Collections.Generic;
using System.Linq;

namespace TwitchySharp.Infrastructure.Functional;

public record MiddlewarePipelineBuilder<TDelegate>
{
    private readonly List<Func<TDelegate, TDelegate>> _components = [];
    /// <summary>
    /// Add a single middleware pipeline step.
    /// </summary>
    /// <param name="func">
    /// The middleware function to use, 
    /// taking a next <typeparamref name="TDelegate"/> as the single parameter and returning the step.
    /// </param>
    /// <returns>This builder with the added step.</returns>
    public MiddlewarePipelineBuilder<TDelegate> Use(Func<TDelegate, TDelegate> func)
    {
        _components.Add(func);
        return this;
    }

    /// <summary>
    /// Sets the terminal function and returns the complete pipeline function.
    /// </summary>
    /// <param name="terminal">The terminal function to use.</param>
    /// <returns>The built pipeline as a <typeparamref name="TDelegate"/>.</returns>
    public TDelegate Finally(TDelegate terminal)
        => _components.AsEnumerable().Reverse().Aggregate(terminal, (current, next) => next(current));
}
