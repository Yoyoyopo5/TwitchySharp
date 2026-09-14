using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Unit.Client.DependencyResolution.DependencyCollectionExtensions;

public class Test_From
{
    [Fact]
    public void GetResolver_ReturnsNonNull()
    {
        const int EXPECTED = 1;

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed(true)
            .From<ImmutableRequestDependencyCollection, int, bool>(b => EXPECTED);

        Assert.NotNull(dc.GetResolver<int>());
    }

    [Fact]
    public async Task ResolveOrDefault_ReturnsValueFromTFrom()
    {
        const int EXPECTED = 1;
        string expectedString = EXPECTED.ToString();

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed(expectedString)
            .From<ImmutableRequestDependencyCollection, int, string>(s => int.Parse(s!));

        StubDependencyScope scope = new(dc);

        await scope.ResolveOrDefault<int>(TestContext.Current.CancellationToken).MatchAsync<int, int>(
            e => throw new Exception(e.Message),
            v =>
            {
                Assert.Equal(EXPECTED, v);
                return v;
            });
    }
}
