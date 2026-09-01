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

public static class ResolveRequestDependencyExtensions
{
    public static ResolveRequestDependency<T?> Map<T, TFrom>(
        this ResolveRequestDependency<TFrom> resolve,
        Func<TFrom?, T?> map
        )
        => (scope, ct) => resolve(scope, ct).MapAsync(f => map(f));

    public static ResolveRequestDependency<T?> Map<T, TFrom>(
        this ResolveRequestDependency<TFrom> resolve,
        Func<TFrom?, ValueTask<T?>> map
        )
        => (scope, ct) => resolve(scope, ct).MapAsync(f => map(f));
}
