using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Unit.Client.DependencyResolution;

public class Test_ITwitchRequestDependencyCollectionExtensions
{

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
