using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Unit.Client.DependencyResolution.DependencyCollectionExtensions;

public class Test_SetFixed
{
    [Fact]
    public void GetResolver_ReturnsNotNull()
    {
        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed(1);

        Assert.NotNull(dc.GetResolver<int>());
    }

    [Fact]
    public async Task ResolveOrDefault_ReturnsFixedValue()
    {
        const int EXPECTED = 1;

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed(EXPECTED);

        StubDependencyScope scope = new(dc);

        await scope.ResolveOrDefault<int>(TestContext.Current.CancellationToken).MatchAsync(
            e => throw new Exception(e.Message),
            v =>
            {
                Assert.Equal(EXPECTED, v);
                return v;
            });
    }

}
