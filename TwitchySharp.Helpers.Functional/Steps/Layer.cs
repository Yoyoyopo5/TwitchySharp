namespace TwitchySharp.Helpers.Functional;

/// <summary>Middleware that wraps a <see cref="Step{TIn, TOut}"/>, adding behavior before and/or after it executes.</summary>
/// <remarks>A layer receives the next step in the pipeline and returns a decorated step. Apply to a step with <c>step.Then(layer)</c>, or compose layers with <c>layerA.Then(layerB)</c>.</remarks>
/// <param name="next">The inner step to wrap.</param>
public delegate Step<TIn, TOut> Layer<TIn, TOut>(Step<TIn, TOut> next);

/// <summary>Middleware that wraps a <see cref="Step{T}"/>, adding behavior before and/or after it executes.</summary>
/// <remarks>A same-type specialization of <see cref="Layer{TIn, TOut}"/> for refinement pipelines.</remarks>
/// <param name="next">The inner step to wrap.</param>
public delegate Step<T> Layer<T>(Step<T> next);