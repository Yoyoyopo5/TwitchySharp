using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Authentication.Tests.Unit.Client;

public class Test_WithExtension
{
    private record ExtensionSecretRequest : TwitchRequest<ExtensionSecret?>
    {
        public override HttpMethod Method => throw new NotImplementedException();
        public override Uri RequestUri => throw new NotImplementedException();
    }

    private record ExtensionOwnerIdRequest : TwitchRequest<ExtensionOwnerId?>
    {
        public override HttpMethod Method => throw new NotImplementedException();
        public override Uri RequestUri => throw new NotImplementedException();
    }

    [Fact]
    public async Task SendAsync_ResolveExtensionSecret_WithSameExtensionId_ReturnsSecret()
    {
        ExtensionId fakeId = new("mclovin");
        ExtensionSecret fakeSecret = new("26");
        ExtensionOwnerId fakeOwnerId = new(new("fogle"));

        TwitchClient stubClient = new TwitchClient() { Resolvers = new ImmutableRequestDependencyCollection() }
            .SetResolver<TwitchResponse<ExtensionSecret?>>((scope, ct) =>
                scope.ResolveOrDefault<ExtensionSecret?>(ct).MapAsync(secret => new TwitchResponse<ExtensionSecret?>()
                {
                    Content = secret,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Request = scope.Request
                }))
            .WithExtension(fakeId, fakeOwnerId, fakeSecret)
            .SetFixed<TwitchClient, ExtensionId?>(fakeId);

        ExtensionSecret? result = (await stubClient.SendAsync(new ExtensionSecretRequest(), TestContext.Current.CancellationToken)).Content;

        Assert.Equal(fakeSecret, result);
    }

    [Fact]
    public async Task SendAsync_ResolveExtensionOwnerId_WithSameExtensionId_ReturnsOwnerId()
    {
        ExtensionId fakeId = new("mclovin");
        ExtensionSecret fakeSecret = new("26");
        ExtensionOwnerId fakeOwnerId = new(new("fogle"));

        TwitchClient stubClient = new TwitchClient() { Resolvers = new ImmutableRequestDependencyCollection() }
            .SetResolver<TwitchResponse<ExtensionOwnerId?>>((scope, ct) =>
                scope.ResolveOrDefault<ExtensionOwnerId?>(ct).MapAsync(ownerId => new TwitchResponse<ExtensionOwnerId?>()
                {
                    Content = ownerId,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Request = scope.Request
                }))
            .WithExtension(fakeId, fakeOwnerId, fakeSecret)
            .SetFixed<TwitchClient, ExtensionId?>(fakeId);

        ExtensionOwnerId? result = (await stubClient.SendAsync(new ExtensionOwnerIdRequest(), TestContext.Current.CancellationToken)).Content;

        Assert.Equal(fakeOwnerId, result);
    }

    [Fact]
    public async Task SendAsync_ResolveExtensionSecret_WithDifferentExtensionId_ReturnsNull()
    {
        ExtensionId fakeId = new("mclovin");
        ExtensionSecret fakeSecret = new("26");
        ExtensionOwnerId fakeOwnerId = new(new("fogle"));

        TwitchClient stubClient = new TwitchClient() { Resolvers = new ImmutableRequestDependencyCollection() }
            .SetResolver<TwitchResponse<ExtensionSecret?>>((scope, ct) =>
                scope.ResolveOrDefault<ExtensionSecret?>(ct).MapAsync(secret => new TwitchResponse<ExtensionSecret?>()
                {
                    Content = secret,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Request = scope.Request
                }))
            .WithExtension(fakeId, fakeOwnerId, fakeSecret)
            .SetFixed<TwitchClient, ExtensionId?>(new("other-id"));

        ExtensionSecret? result = (await stubClient.SendAsync(new ExtensionSecretRequest(), TestContext.Current.CancellationToken)).Content;

        Assert.Null(result);
    }

    [Fact]
    public async Task SendAsync_ResolveExtensionOwnerId_WithDifferentExtensionId_ReturnsNull()
    {
        ExtensionId fakeId = new("mclovin");
        ExtensionSecret fakeSecret = new("26");
        ExtensionOwnerId fakeOwnerId = new(new("fogle"));

        TwitchClient stubClient = new TwitchClient() { Resolvers = new ImmutableRequestDependencyCollection() }
            .SetResolver<TwitchResponse<ExtensionOwnerId?>>((scope, ct) =>
                scope.ResolveOrDefault<ExtensionOwnerId?>(ct).MapAsync(ownerId => new TwitchResponse<ExtensionOwnerId?>()
                {
                    Content = ownerId,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Request = scope.Request
                }))
            .WithExtension(fakeId, fakeOwnerId, fakeSecret)
            .SetFixed<TwitchClient, ExtensionId?>(new("other-id"));

        ExtensionOwnerId? result = (await stubClient.SendAsync(new ExtensionOwnerIdRequest(), TestContext.Current.CancellationToken)).Content;

        Assert.Null(result);
    }
}
