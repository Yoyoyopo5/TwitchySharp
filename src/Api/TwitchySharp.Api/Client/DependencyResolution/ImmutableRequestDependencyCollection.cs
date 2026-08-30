using System.Collections.Immutable;

namespace TwitchySharp.Api;

internal record ImmutableRequestDependencyCollection
    : ITwitchRequestDependencyCollection
{
    private ImmutableDictionary<Type, Delegate> Resolvers { get; init; }
        = ImmutableDictionary<Type, Delegate>.Empty;

    public ImmutableRequestDependencyCollection SetResolver<T>(ResolveRequestDependency<T> resolve)
        => this with { Resolvers = Resolvers.SetItem(typeof(T), resolve) };
    ITwitchRequestDependencyCollection ITwitchRequestDependencyCollection.SetResolver<T>(ResolveRequestDependency<T> resolve)
        => SetResolver(resolve);
    public ResolveRequestDependency<T>? GetResolver<T>()
        => Resolvers.GetValueOrDefault(typeof(T)) as ResolveRequestDependency<T>;
}
