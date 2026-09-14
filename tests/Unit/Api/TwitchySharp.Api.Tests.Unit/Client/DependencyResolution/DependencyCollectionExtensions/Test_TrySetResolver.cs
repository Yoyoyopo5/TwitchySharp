using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Unit.Client.DependencyResolution.DependencyCollectionExtensions;

public class Test_TrySetResolver
{
    [Fact]
    public void GetResolver_OnCollectionWithExistingTResolver_ReturnsExistingResolver()
    {
        ResolveRequestDependency<int> expected = (scope, ct) => throw new NotImplementedException();

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetResolver(expected)
            .TrySetResolver((scope, ct) => ValueTask.FromResult<Validation<int>>(1));

        Assert.Equal(expected, dc.GetResolver<int>());
    }

    [Fact]
    public void GetResolver_OnCollectionWithoutTResolver_ReturnsNewResolver()
    {
        ResolveRequestDependency<int> expected = (scope, ct) => throw new NotImplementedException();

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .TrySetResolver(expected);

        Assert.Equal(expected, dc.GetResolver<int>());
    }
}
