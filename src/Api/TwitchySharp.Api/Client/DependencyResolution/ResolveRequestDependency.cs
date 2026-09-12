using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api;
/// <summary>
/// A function that resolves a specific Twitch API request dependency of <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of dependency the function resolves.</typeparam>
/// <param name="scope">The dependency scope for the request.</param>
/// <param name="ct">Cancellation token</param>
/// <returns>A <see cref="ValueTask"/> containing the resolved dependency result.</returns>
public delegate ValueTask<Validation<T>> ResolveRequestDependency<T>(
    ITwitchRequestDependencyScope scope,
    CancellationToken ct);

/// <summary>
/// Collection of extensions for <see cref="ResolveRequestDependency{T}"/>.
/// </summary>
public static class ResolveRequestDependencyExtensions
{
    /// <summary>
    /// Create a resolver for <typeparamref name="T"/> from a resolver for <typeparamref name="TFrom"/> to using a map function.
    /// </summary>
    /// <typeparam name="T">The dependency type to create a resolver for.</typeparam>
    /// <typeparam name="TFrom">The resolver type to map.</typeparam>
    /// <param name="resolve">The resolver function to map.</param>
    /// <param name="map">A function mapping the <typeparamref name="TFrom"/> resolver's output to <typeparamref name="T"/>.</param>
    /// <returns>A new resolver for <typeparamref name="T"/>.</returns>
    public static ResolveRequestDependency<T?> Map<T, TFrom>(
        this ResolveRequestDependency<TFrom> resolve,
        Func<TFrom?, T?> map
        )
        => (scope, ct) => resolve(scope, ct).MapAsync(f => map(f));

    /// <inheritdoc cref="Map"/>
    public static ResolveRequestDependency<T?> Map<T, TFrom>(
        this ResolveRequestDependency<TFrom> resolve,
        Func<TFrom?, ValueTask<T?>> map
        )
        => (scope, ct) => resolve(scope, ct).MapAsync(f => map(f));
}
