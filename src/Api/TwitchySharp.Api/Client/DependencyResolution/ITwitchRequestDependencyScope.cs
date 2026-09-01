using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api;

/// <summary>
/// Provides dependency resolvers scoped to a specific <see cref="TwitchRequest"/>.
/// </summary>
public interface ITwitchRequestDependencyScope
    : ITwitchRequestDependencyCollection, ITwitchRequestDependencyCollection<ITwitchRequestDependencyScope>
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
    ValueTask<Validation<T?>> ResolveOrDefault<T>(CancellationToken ct);
}

public record MissingRequiredDependencyError(Type DependencyType)
    : Error($"Failed to resolve required dependency {DependencyType.Name}.")
{
    public static MissingRequiredDependencyError Create<T>()
        => new(typeof(T));
}

public static class IRequestDependencyScopeExtensions
{
    public static ValueTask<Validation<T>> ResolveRequired<T>(
        this ITwitchRequestDependencyScope scope,
        CancellationToken ct)
        => scope.ResolveOrDefault<T>(ct).BindAsync<T?, T>(value => value is not null
            ? value
            : MissingRequiredDependencyError.Create<T>());
}
