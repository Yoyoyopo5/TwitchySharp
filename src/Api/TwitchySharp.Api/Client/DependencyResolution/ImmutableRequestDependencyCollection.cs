using System.Collections.Immutable;

namespace TwitchySharp.Api;

internal record ImmutableRequestDependencyCollection
    : ITwitchRequestDependencyCollection<ImmutableRequestDependencyCollection>,
    ITwitchRequestDependencyCollection
{
    private ImmutableDictionary<Type, Delegate> Resolvers { get; init; }
        = ImmutableDictionary<Type, Delegate>.Empty;

    public ImmutableRequestDependencyCollection SetResolver<T>(ResolveRequestDependency<T> resolve)
        => this with { Resolvers = Resolvers.SetItem(typeof(T), resolve) };
    public ResolveRequestDependency<T>? GetResolver<T>()
        => Resolvers.GetValueOrDefault(typeof(T)) as ResolveRequestDependency<T>;
    ITwitchRequestDependencyCollection ITwitchRequestDependencyCollection<ITwitchRequestDependencyCollection>.SetResolver<T>(ResolveRequestDependency<T> resolve)
        => SetResolver(resolve);
}
