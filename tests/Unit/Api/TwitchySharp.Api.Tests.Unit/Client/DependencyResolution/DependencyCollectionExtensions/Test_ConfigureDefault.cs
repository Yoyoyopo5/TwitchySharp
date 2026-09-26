using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Unit.Client.DependencyResolution.DependencyCollectionExtensions;

public class Test_ConfigureDefault
{
    [Fact]
    public async Task ResolveOrDefault_OnCollectionWithResolverReturningNull_ReturnsDefaultValue()
    {
        const string EXPECTED = "abc";

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed<ImmutableRequestDependencyCollection, string?>(null)
            .ConfigureDefault<ImmutableRequestDependencyCollection, string?>(EXPECTED);

        StubDependencyScope scope = new(dc);

        await scope.ResolveOrDefault<string?>(TestContext.Current.CancellationToken).MatchAsync(
            e => throw new Exception(e.Message),
            v =>
            {
                Assert.Equal(EXPECTED, v);
                return v;
            });
    }

    [Fact]
    public async Task ResolveOrDefault_OnCollectionWithResolverReturningNotNull_ReturnsResolverValue()
    {
        const string EXPECTED = "abc";

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed<ImmutableRequestDependencyCollection, string?>(EXPECTED)
            .ConfigureDefault<ImmutableRequestDependencyCollection, string?>("def");

        StubDependencyScope scope = new(dc);

        await scope.ResolveOrDefault<string?>(TestContext.Current.CancellationToken).MatchAsync(
            e => throw new Exception(e.Message),
            v =>
            {
                Assert.Equal(EXPECTED, v);
                return v;
            });
    }
}
