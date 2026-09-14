using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Unit.Client.DependencyResolution.DependencyCollectionExtensions;

public class Test_Configure
{
    [Fact]
    public void GetResolver_ReturnsNotNull()
    {
        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .Configure<ImmutableRequestDependencyCollection, int>(next => (scope, ct) => ValueTask.FromResult<Validation<int>>(1));

        Assert.NotNull(dc.GetResolver<int>());
    }

    [Fact]
    public void Configure_OnCollectionWithExistingResolver_ConfigureGetsExistingResolver()
    {
        ResolveRequestDependency<int> existing = (scope, ct) => throw new NotImplementedException();
        ResolveRequestDependency<int>? received = null;

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetResolver(existing)
            .Configure<ImmutableRequestDependencyCollection, int>(next =>
            {
                received = next;
                return (scope, ct) => throw new NotImplementedException();
            });

        Assert.Equal(existing, received);
    }

    [Fact]
    public async Task Configure_OnCollectionWithoutExistingResolver_ConfigureGetsDefaultResolver()
    {
        ResolveRequestDependency<int>? received = null;

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .Configure<ImmutableRequestDependencyCollection, int>(next =>
            {
                received = next;
                return (scope, ct) => throw new NotImplementedException();
            });

        Assert.NotNull(received);
        await received(new StubDependencyScope(dc), TestContext.Current.CancellationToken).MatchAsync(
            e => throw new Exception(e.Message),
            v =>
            {
                Assert.Equal(default, v);
                return v;
            });
    }
}
