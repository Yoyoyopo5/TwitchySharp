using TwitchySharp.Api.Tests.Unit.Client.DependencyResolution;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Authentication.Tests.Unit.Client;

public class Test_WithClient
{
    private record ClientSecretRequest : TwitchRequest<ClientSecret?>
    {
        public override HttpMethod Method => throw new NotImplementedException();
        public override Uri RequestUri => throw new NotImplementedException();
    }

    [Fact]
    public async Task ResolveClientSecret_WithSameClientId_ReturnsClientSecret()
    {
        ClientId fakeId = new("mclovin");
        ClientSecret fakeSecret = new("26");

        TwitchClient stubClient = new TwitchClient() { Resolvers = new ImmutableRequestDependencyCollection() }
            .WithClient(fakeId, fakeSecret);

        StubDependencyScope scope = new(stubClient.Resolvers);

        ResolveRequestDependency<ClientSecret?>? resolver = stubClient.Resolvers.GetResolver<ClientSecret?>();
        Assert.NotNull(resolver);

        Result<ClientSecret?> result = await resolver(scope, TestContext.Current.CancellationToken);

        Assert.Equal(fakeSecret, result);
    }

    [Fact]
    public async Task ResolveClientSecret_WithDifferentClientId_ReturnsNull()
    {
        ClientId fakeId = new("mclovin");
        ClientSecret fakeSecret = new("26");

        ClientId otherFakeId = new("fogle");

        TwitchClient stubClient = new TwitchClient() { Resolvers = new ImmutableRequestDependencyCollection() }
            .WithClient(fakeId, fakeSecret)
            .SetFixed<TwitchClient, ClientId?>(otherFakeId);

        StubDependencyScope scope = new(stubClient.Resolvers);

        ResolveRequestDependency<ClientSecret?>? resolver = stubClient.Resolvers.GetResolver<ClientSecret?>();
        Assert.NotNull(resolver);

        Result<ClientSecret?> result = await resolver(scope, TestContext.Current.CancellationToken);

        result.Match(
            e => throw new Exception(e.Message),
            secret =>
            {
                Assert.Null(secret);
                return secret;
            });
    }
}
