using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Unit.Client.DependencyResolution.DependencyCollectionExtensions;

public class Test_As
{
    [Fact]
    public void GetResolver_ReturnsNotNull()
    {
        const string EXPECTED = "abc";

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed<ImmutableRequestDependencyCollection, object>(EXPECTED)
            .As<ImmutableRequestDependencyCollection, string, object>();

        Assert.NotNull(dc.GetResolver<string>());
    }

    [Fact]
    public async Task ResolveOrDefault_WhenBaseIsT_ReturnsBaseAsT()
    {
        const string EXPECTED = "abc";

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed<ImmutableRequestDependencyCollection, object>(EXPECTED)
            .As<ImmutableRequestDependencyCollection, string, object>();

        StubDependencyScope scope = new(dc);

        await scope.ResolveOrDefault<string>(TestContext.Current.CancellationToken).MatchAsync(
            e => throw new Exception(e.Message),
            v =>
            {
                Assert.Equal(EXPECTED, v);
                return v;
            }
            );
    }
}
