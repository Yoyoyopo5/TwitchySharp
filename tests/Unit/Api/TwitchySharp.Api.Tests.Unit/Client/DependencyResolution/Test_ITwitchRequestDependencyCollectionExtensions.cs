using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Unit.Client.DependencyResolution;

public record StubTwitchRequest : TwitchRequest
{
    public override HttpMethod Method => throw new NotImplementedException();
    public override Uri RequestUri => throw new NotImplementedException();
}

public class Test_ITwitchRequestDependencyCollectionExtensions
{
    private record StubDependencyScope(ITwitchRequestDependencyCollection Resolvers)
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

    [Fact]
    public async Task ConfigureAsNullCoalesce_DependencyCollectionWithPreviousNonNullResolver_ResolvesPreviouslyConfiguredResolverValue()
    {
        const string PREVIOUS_VALUE = "previous";
        const string AS_NULL_COALESCE_VALUE = "coalesced";

        ITwitchRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed(PREVIOUS_VALUE)
            .ConfigureAsNullCoalesce((scope, ct) => ValueTask.FromResult<Validation<string>>(AS_NULL_COALESCE_VALUE));

        ITwitchRequestDependencyScope scope = new StubDependencyScope(dc);
        await scope.ResolveOrDefault<string>(TestContext.Current.CancellationToken)
            .MapAsync(value => Assert.Equal(PREVIOUS_VALUE, value));
    }

    [Fact]
    public async Task SetFixed_EmptyDependencyCollection_ResolvesFixedValue()
    {
        const string FIXED_VALUE = "hey smash";

        ITwitchRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed(FIXED_VALUE);

        ITwitchRequestDependencyScope scope = new StubDependencyScope(dc);
        await scope.ResolveOrDefault<string>(TestContext.Current.CancellationToken)
            .MapAsync(value => Assert.Equal(FIXED_VALUE, value));
    }

    [Fact]
    public async Task ConfigureDefault_DependencyCollectionWithPreviouslyConfiguredResolverReturningNonNull_ResolvesPreviouslyConfiguredResolverValue()
    {
        bool? previouslyConfiguredValue = true;
        bool? configureDefaultValue = false;

        ITwitchRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed(previouslyConfiguredValue)
            .ConfigureDefault(configureDefaultValue);

        ITwitchRequestDependencyScope scope = new StubDependencyScope(dc);
        await scope.ResolveOrDefault<bool?>(TestContext.Current.CancellationToken)
            .MapAsync(value => Assert.Equal(previouslyConfiguredValue, value));
    }
}
