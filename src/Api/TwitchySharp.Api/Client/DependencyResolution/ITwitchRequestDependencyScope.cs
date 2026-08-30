using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api;

/// <summary>
/// Provides dependency resolvers scoped to a specific <see cref="TwitchRequest"/>.
/// </summary>
public interface ITwitchRequestDependencyScope
    : ITwitchRequestDependencyCollection
{
    /// <summary>
    /// The <see cref="TwitchRequest"/> associated with scope.
    /// </summary>
    TwitchRequest Request { get; }
    /// <summary>
    /// Resolve the dependency of <typeparamref name="T"/> for the current request scope.
    /// </summary>
    /// <typeparam name="T">The type of dependency to resolve.</typeparam>
    /// <param name="ct">Cancellation token</param>
    /// <returns>A <see cref="ValueTask"/> containing the resolver result.</returns>
    ValueTask<RequestDependencyResult<T>> GetOrDefault<T>(CancellationToken ct);
}

public static class IRequestDependencyScopeExtensions
{
    /// <summary>
    /// Create a <see cref="RequestDependencyResult{T}"/> from
    /// an existing <see cref="ITwitchRequestDependencyScope"/>.
    /// </summary>
    /// <typeparam name="T">The type of dependency.</typeparam>
    /// <param name="nextScope">The scope to create the result from.</param>
    /// <param name="validResult">The resolved dependency value.</param>
    /// <returns>A valid <see cref="RequestDependencyResult{T}"/>.</returns>
    public static RequestDependencyResult<T> ToResult<T>(
        this ITwitchRequestDependencyScope nextScope,
        T? validResult
        )
        => new(validResult, nextScope);

    /// <summary>
    /// <inheritdoc cref="ToResult{T}(ITwitchRequestDependencyScope, T?)"/>
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="ToResult{T}(ITwitchRequestDependencyScope, T?)" path="/typeparam[@name='T']"/></typeparam>
    /// <param name="nextScope"><inheritdoc cref="ToResult{T}(ITwitchRequestDependencyScope, T?)" path="/param[@name='nextScope']"/></param>
    /// <param name="error">The error to set the result to.</param>
    /// <returns>An errored <see cref="RequestDependencyResult{T}"/>.</returns>
    public static RequestDependencyResult<T> ToResult<T>(
        this ITwitchRequestDependencyScope nextScope,
        Error error
        )
        => new(error, nextScope);
}
