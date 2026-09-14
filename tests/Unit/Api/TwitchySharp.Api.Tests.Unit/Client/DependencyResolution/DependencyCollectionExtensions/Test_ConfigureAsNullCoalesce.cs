using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Unit.Client.DependencyResolution.DependencyCollectionExtensions;

public class Test_ConfigureAsNullCoalesce
{
    [Fact]
    public async Task ResolveOrDefault_OnCollectionWithResolverReturningNull_CallsConfiguredResolver()
    {
        bool calledConfiguredResolver = false;

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed<ImmutableRequestDependencyCollection, object?>(null)
            .ConfigureAsNullCoalesce((scope, ct) =>
            {
                calledConfiguredResolver = true;
                return ValueTask.FromResult<Validation<object?>>(new object());
            });

        StubDependencyScope scope = new(dc);

        await scope.ResolveOrDefault<object?>(TestContext.Current.CancellationToken);
        Assert.True(calledConfiguredResolver);
    }

    [Fact]
    public async Task ResolveOrDefault_OnCollectionWithResolverReturningNotNull_ReturnsWithoutCallingConfiguredResolver()
    {
        bool calledConfiguredResolver = false;

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed<ImmutableRequestDependencyCollection, object?>(new())
            .ConfigureAsNullCoalesce((scope, ct) =>
            {
                calledConfiguredResolver = true;
                return ValueTask.FromResult<Validation<object?>>(new object());
            });

        StubDependencyScope scope = new(dc);

        await scope.ResolveOrDefault<object?>(TestContext.Current.CancellationToken);
        Assert.False(calledConfiguredResolver);
    }
}
