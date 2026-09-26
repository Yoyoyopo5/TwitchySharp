using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Unit.Client.DependencyResolution;

public record StubDependencyScope(ITwitchRequestDependencyCollection Resolvers)
        : ITwitchRequestDependencyScope
{
    public TwitchRequest Request { get; } = new StubTwitchRequest();

    public ResolveRequestDependency<T>? GetResolver<T>() => Resolvers.GetResolver<T>();
    public StubDependencyScope SetResolver<T>(ResolveRequestDependency<T> resolve)
        => this with { Resolvers = Resolvers.SetResolver(resolve) };

    public ValueTask<Validation<T?>> ResolveOrDefault<T>(CancellationToken ct)
        => GetResolver<T>() is ResolveRequestDependency<T> resolver
            ? resolver(this, ct).MapAsync(result => (T?)result)
            : ValueTask.FromResult<Validation<T?>>((T?)default);
    ITwitchRequestDependencyScope ITwitchRequestDependencyCollection<ITwitchRequestDependencyScope>.SetResolver<T>(ResolveRequestDependency<T> resolve)
        => SetResolver(resolve);
    ITwitchRequestDependencyCollection ITwitchRequestDependencyCollection<ITwitchRequestDependencyCollection>.SetResolver<T>(ResolveRequestDependency<T> resolve)
        => SetResolver(resolve);
}
