using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Unit.Client.DependencyResolution.DependencyCollectionExtensions;

public class Test_FromRequest
{
    [Fact]
    public void GetResolver_HttpMethod_ReturnsNonNull()
    {
        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .FromRequest(request => request.Method);

        Assert.NotNull(dc.GetResolver<HttpMethod>());
    }

    [Fact]
    public async Task ResolveOrDefault_HttpMethod_ReturnsHttpMethodFromRequest()
    {
        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .FromRequest(request => request.Method);

        StubDependencyScope scope = new(dc);

        await scope.ResolveOrDefault<HttpMethod>(TestContext.Current.CancellationToken).MatchAsync(
            e => throw new Exception(e.Message),
            v =>
            {
                Assert.Equal(scope.Request.Method, v);
                return v;
            }
            );
    }
}
