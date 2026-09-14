using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Unit.Client.DependencyResolution.DependencyCollectionExtensions;

public class Test_RequestAs
{
    [Fact]
    public void GetResolver_ReturnsNotNull()
    {
        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .RequestAs<ImmutableRequestDependencyCollection, TwitchRequest<object>>();

        Assert.NotNull(dc.GetResolver<TwitchRequest<object>>());
    }

    [Fact]
    public async Task ResolveOrDefault_WhenRequestIsT_ReturnsRequestAsT()
    {
        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .RequestAs<ImmutableRequestDependencyCollection, TwitchRequest<object>>();

        StubDependencyScope scope = new(dc);

        await scope.ResolveOrDefault<TwitchRequest<object>>(TestContext.Current.CancellationToken).MatchAsync(
            e => throw new Exception(e.Message),
            v =>
            {
                Assert.Equal(scope.Request, v);
                return v;
            });
    }
}
