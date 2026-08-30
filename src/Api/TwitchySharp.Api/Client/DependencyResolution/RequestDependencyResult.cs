using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api;

/// <summary>
/// A result for a resolved Twitch API request dependency.
/// </summary>
/// <typeparam name="T">The type of dependency the result is for.</typeparam>
/// <param name="Value">The result's value, if any.</param>
/// <param name="UpdatedScope">The new request dependency scope after resolving <paramref name="Value"/>.</param>
/// <param name="Error">The error that occurred during resolution, if any.</param>
public record RequestDependencyResult<T>(
    T? Value,
    ITwitchRequestDependencyScope UpdatedScope,
    Error? Error
    )
{
    /// <summary>
    /// An error representing a missing request dependency.
    /// </summary>
    /// <param name="DependencyType">The type of dependency that was missing.</param>
    public record MissingRequiredDependencyError(Type DependencyType) : Error(
        $"Failed to resolve required dependency of type {DependencyType.Name}."
        );

    /// <summary>
    /// Create a result with no error.
    /// </summary>
    /// <param name="value"><inheritdoc cref="RequestDependencyResult{T}" path="/param[@name='Value']"/></param>
    /// <param name="updatedScope"><inheritdoc cref="RequestDependencyResult{T}" path="/param[@name='UpdatedScope']"/></param>
    public RequestDependencyResult(T? value, ITwitchRequestDependencyScope updatedScope)
        : this(value, updatedScope, null) { }

    /// <summary>
    /// Create an error result.
    /// </summary>
    /// <param name="error">The error.</param>
    /// <param name="updatedScope">The new request dependency scope after resolving the error.</param>
    public RequestDependencyResult(Error error, ITwitchRequestDependencyScope updatedScope)
        : this(default, updatedScope, error) { }

    /// <summary>
    /// Resolve the request dependency of <typeparamref name="TNext"/>
    /// using the <see cref="UpdatedScope"/> of the <see cref="RequestDependencyResult{T}"/>.
    /// </summary>
    /// <remarks>
    /// If the <see cref="RequestDependencyResult{T}.Error"/> is not <see langword="null"/>,
    /// <typeparamref name="TNext"/> resolution is skipped and the error is passed to the new result.
    /// </remarks>
    /// <typeparam name="TNext">The next type to resolve.</typeparam>
    /// <param name="ct">Cancellation token</param>
    /// <returns>A <see cref="ValueTask"/> containing a <see cref="RequestDependencyResult{T}"/>.</returns>
    public async ValueTask<RequestDependencyResult<TNext>> GetOrDefault<TNext>(CancellationToken ct)
        => Error is not null
            ? new(default, UpdatedScope, Error)
            : await UpdatedScope.GetOrDefault<TNext>(ct);

    public RequestDependencyResult<TNext> Map<TNext>(Func<T?, TNext?> map)
        => Error is not null
            ? UpdatedScope.ToResult<TNext>(Error)
            : UpdatedScope.ToResult(map(Value));

    public async ValueTask<RequestDependencyResult<TNext>> Map<TNext>(Func<T?, ValueTask<TNext?>> map)
        => Error is not null
            ? UpdatedScope.ToResult<TNext>(Error)
            : UpdatedScope.ToResult(await map(Value));

    public async ValueTask<RequestDependencyResult<TNext>> MapRequired<TNext>(Func<T, ValueTask<TNext?>> map)
        => Error is not null
            ? UpdatedScope.ToResult<TNext>(Error)
            : Value is null
            ? UpdatedScope.ToResult<TNext>(new MissingRequiredDependencyError(typeof(T)))
            : UpdatedScope.ToResult(await map(Value));

    public RequestDependencyResult<TNext> MapRequired<TNext>(Func<T, TNext?> map)
        => Error is not null
            ? UpdatedScope.ToResult<TNext>(Error)
            : Value is null
            ? UpdatedScope.ToResult<TNext>(new MissingRequiredDependencyError(typeof(T)))
            : UpdatedScope.ToResult(map(Value));

    public ValueTask<RequestDependencyResult<TNext>> Bind<TNext>(
        Func<ITwitchRequestDependencyScope, T?, ValueTask<RequestDependencyResult<TNext>>> func
        )
        => Error is not null
            ? ValueTask.FromResult(UpdatedScope.ToResult<TNext>(Error))
            : func(UpdatedScope, Value);

    public ValueTask<RequestDependencyResult<TNext>> BindRequired<TNext>(
        Func<ITwitchRequestDependencyScope, T, ValueTask<RequestDependencyResult<TNext>>> func
        )
        => Error is Error error
            ? ValueTask.FromResult(UpdatedScope.ToResult<TNext>(error))
            : Value is null
            ? ValueTask.FromResult(UpdatedScope.ToResult<TNext>(new MissingRequiredDependencyError(typeof(T))))
            : func(UpdatedScope, Value);

    /// <summary>
    /// Create a <see cref="RequestDependencyResult{T}"/> from a <see cref="Validation"/> and <see cref="TwitchRequestDependencyScope"/>,
    /// passing the <see cref="Validation"/> value or error, if any, to the returned result.
    /// </summary>
    /// <param name="validation">The <see cref="Validation"/> to construct the result from.</param>
    /// <param name="scope">The <see cref="TwitchRequestDependencyScope"/> to use in the result.</param>
    /// <returns>A new <see cref="RequestDependencyResult{T}"/> with the <see cref="Validation"/> value or error.</returns>
    internal static RequestDependencyResult<T> From(Validation<T?> validation, ITwitchRequestDependencyScope scope)
        => validation.Match(
            scope.ToResult<T>,
            scope.ToResult
            );
}

public static class DependencyResultExtensions
{
    public static async ValueTask<RequestDependencyResult<TNext>> MapAsync<T, TNext>(
        this ValueTask<RequestDependencyResult<T>> result,
        Func<T?, TNext?> func
        )
        => (await result).Map(func);

    public static async ValueTask<RequestDependencyResult<TNext>> MapAsync<T, TNext>(
        this ValueTask<RequestDependencyResult<T>> result,
        Func<T?, ValueTask<TNext?>> func
        )
        => await (await result).Map(func);

    public static async ValueTask<RequestDependencyResult<TNext>> MapRequiredAsync<T, TNext>(
        this ValueTask<RequestDependencyResult<T>> result,
        Func<T, ValueTask<TNext?>> func
        )
        => await (await result).MapRequired(func);

    public static async ValueTask<RequestDependencyResult<TNext>> MapRequiredAsync<T, TNext>(
        this ValueTask<RequestDependencyResult<T>> result,
        Func<T, TNext?> func
        )
        => (await result).MapRequired(func);

    public static async ValueTask<RequestDependencyResult<TNext>> BindAsync<T, TNext>(
        this ValueTask<RequestDependencyResult<T>> result,
        Func<ITwitchRequestDependencyScope, T?, ValueTask<RequestDependencyResult<TNext>>> func
        )
        => await (await result).Bind(func);

    public static async ValueTask<RequestDependencyResult<TNext>> BindRequiredAsync<T, TNext>(
        this ValueTask<RequestDependencyResult<T>> result,
        Func<ITwitchRequestDependencyScope, T, ValueTask<RequestDependencyResult<TNext>>> func
        )
        => await (await result).BindRequired(func);

    public static ValueTask<RequestDependencyResult<T>> ToDependencyResultAsync<T>(
        this ValueTask<Validation<T>> validation,
        ITwitchRequestDependencyScope scope
        )
        => validation.MatchAsync(
            scope.ToResult<T>,
            scope.ToResult
            );
}
