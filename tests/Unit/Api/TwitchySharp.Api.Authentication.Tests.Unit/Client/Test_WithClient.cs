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
    public async Task SendAsync_ResolveClientSecret_WithSameClientId_ReturnsClientSecret()
    {
        ClientId fakeId = new("mclovin");
        ClientSecret fakeSecret = new("26");

        TwitchClient stubClient = new TwitchClient() { Resolvers = new ImmutableRequestDependencyCollection() }
            .SetResolver<TwitchResponse<ClientSecret?>>((scope, ct) =>
                scope.ResolveOrDefault<ClientSecret?>(ct).MapAsync(secret => new TwitchResponse<ClientSecret?>()
                {
                    Content = secret,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Request = scope.Request
                }))
            .WithClient(fakeId, fakeSecret);

        ClientSecret? result = (await stubClient.SendAsync(new ClientSecretRequest(), TestContext.Current.CancellationToken)).Content;

        Assert.Equal(fakeSecret, result);
    }

    [Fact]
    public async Task SendAsync_ResolveClientSecret_WithDifferentClientId_ReturnsNull()
    {
        ClientId fakeId = new("mclovin");
        ClientSecret fakeSecret = new("26");

        ClientId otherFakeId = new("fogle");

        TwitchClient stubClient = new TwitchClient() { Resolvers = new ImmutableRequestDependencyCollection() }
            .SetResolver<TwitchResponse<ClientSecret?>>((scope, ct) =>
                scope.ResolveOrDefault<ClientSecret?>(ct).MapAsync(secret => new TwitchResponse<ClientSecret?>()
                {
                    Content = secret,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Request = scope.Request
                }))
            .WithClient(fakeId, fakeSecret)
            .SetFixed<TwitchClient, ClientId?>(otherFakeId);

        ClientSecret? result = (await stubClient.SendAsync(new ClientSecretRequest(), TestContext.Current.CancellationToken)).Content;

        Assert.Null(result);
    }
}
