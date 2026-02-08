using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Functional;

/// <summary>An async transform that takes <typeparamref name="TIn"/> and produces <typeparamref name="TOut"/>.</summary>
/// <remarks>The fundamental building block of a pipeline. Chain steps together with <c>.Then()</c> and execute with <c>.With()</c>.</remarks>
/// <param name="in">The input value to transform.</param>
public delegate ValueTask<TOut> Step<TIn, TOut>(TIn @in);

/// <summary>An async same-type transform that takes and returns <typeparamref name="T"/>.</summary>
/// <remarks>A specialization of <see cref="Step{TIn, TOut}"/> where input and output share the same type, useful for refinement and validation chains.</remarks>
/// <param name="in">The input value to transform.</param>
public delegate ValueTask<T> Step<T>(T @in);