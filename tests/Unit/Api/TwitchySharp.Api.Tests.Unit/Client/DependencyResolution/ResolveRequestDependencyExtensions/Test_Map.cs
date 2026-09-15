using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Unit.Client.DependencyResolution.ResolveRequestDependencyExtensions;

public class Test_Map
{
    [Fact]
    public async Task Invoke_MappedResolver_TFromResolverEvaluatedAndMapEvaluatedWithTFrom()
    {
        const string EXPECTED = "test";
        bool mapCalled = false;

        ResolveRequestDependency<string?> rootResolver = (scope, ct)
            => ValueTask.FromResult<Validation<string?>>(EXPECTED);

        ResolveRequestDependency<string?> mapped = rootResolver.Map<string?, string?>(s =>
        {
            mapCalled = true;
            Assert.Equal(EXPECTED, s);
            return s;
        });

        await mapped(new StubDependencyScope(new ImmutableRequestDependencyCollection()), TestContext.Current.CancellationToken);
        Assert.True(mapCalled);
    }

    [Fact]
    public async Task Invoke_MappedResolver_TFromResolverEvaluatedAndAsyncMapEvaluatedWithTFrom()
    {
        const string EXPECTED = "test";
        bool mapCalled = false;

        ResolveRequestDependency<string?> rootResolver = (scope, ct)
            => ValueTask.FromResult<Validation<string?>>(EXPECTED);

        ResolveRequestDependency<string?> mapped = rootResolver.Map<string?, string?>(s =>
        {
            mapCalled = true;
            Assert.Equal(EXPECTED, s);
            return ValueTask.FromResult(s);
        });

        await mapped(new StubDependencyScope(new ImmutableRequestDependencyCollection()), TestContext.Current.CancellationToken);
        Assert.True(mapCalled);
    }
}
