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

/// <summary>
/// An error occuring when a required request dependency resolved to <see langword="null"/>.
/// </summary>
/// <param name="DependencyType">The missing dependency type.</param>
public record MissingRequiredDependencyError(Type DependencyType)
    : Error($"Failed to resolve required dependency {DependencyType.Name}.")
{
    /// <summary>
    /// Create an instance of <see cref="MissingRequiredDependencyError"/>
    /// for dependency type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The missing dependency type.</typeparam>
    /// <returns>A new <see cref="MissingRequiredDependencyError"/> for type <typeparamref name="T"/>.</returns>
    public static MissingRequiredDependencyError Create<T>()
        => new(typeof(T));
}

/// <summary>
/// Collection of extensions for <see cref="ITwitchRequestDependencyScope"/>.
/// </summary>
public static class IRequestDependencyScopeExtensions
{
    /// <summary>
    /// Resolve the dependency of type <typeparamref name="T"/>,
    /// returning a <see cref="MissingRequiredDependencyError"/> if
    /// the resolved <typeparamref name="T"/> is <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T">The type to resolve.</typeparam>
    /// <param name="scope">The scope to resolve the dependency from.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns><inheritdoc cref="ITwitchRequestDependencyScope.ResolveOrDefault{T}(CancellationToken)"/></returns>
    public static ValueTask<Validation<T>> ResolveRequired<T>(
        this ITwitchRequestDependencyScope scope,
        CancellationToken ct)
        => scope.ResolveOrDefault<T>(ct).BindAsync<T?, T>(value => value is not null
            ? value
            : MissingRequiredDependencyError.Create<T>());
}
