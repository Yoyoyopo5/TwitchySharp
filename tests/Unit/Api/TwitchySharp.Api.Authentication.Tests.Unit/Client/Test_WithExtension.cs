using TwitchySharp.Api.Tests.Unit.Client.DependencyResolution;
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
    public async Task ResolveExtensionSecret_WithSameExtensionId_ReturnsSecret()
    {
        ExtensionId fakeId = new("mclovin");
        ExtensionSecret fakeSecret = new("c3VwZXJfc2VjdXJlX3NlY3JldF9rZXk=");
        ExtensionOwnerId fakeOwnerId = new(new("fogle"));

        TwitchClient stubClient = new TwitchClient() { Resolvers = new ImmutableRequestDependencyCollection() }
            .WithExtension(fakeId, fakeOwnerId, fakeSecret)
            .SetFixed<TwitchClient, ExtensionId?>(fakeId);

        ResolveRequestDependency<ExtensionSecret?>? resolve = stubClient.GetResolver<ExtensionSecret?>();
        Assert.NotNull(resolve);

        StubDependencyScope scope = new(stubClient.Resolvers);
        Result<ExtensionSecret?> result = await resolve(scope, TestContext.Current.CancellationToken);

        Assert.Equal(fakeSecret, result);
    }

    [Fact]
    public async Task ResolveExtensionOwnerId_WithSameExtensionId_ReturnsOwnerId()
    {
        ExtensionId fakeId = new("mclovin");
        ExtensionSecret fakeSecret = new("c3VwZXJfc2VjdXJlX3NlY3JldF9rZXk=");
        ExtensionOwnerId fakeOwnerId = new(new("fogle"));

        TwitchClient stubClient = new TwitchClient() { Resolvers = new ImmutableRequestDependencyCollection() }
            .WithExtension(fakeId, fakeOwnerId, fakeSecret)
            .SetFixed<TwitchClient, ExtensionId?>(fakeId);

        ResolveRequestDependency<ExtensionOwnerId?>? resolve = stubClient.GetResolver<ExtensionOwnerId?>();
        Assert.NotNull(resolve);

        StubDependencyScope scope = new(stubClient.Resolvers);
        Result<ExtensionOwnerId?> result = await resolve(scope, TestContext.Current.CancellationToken);

        result.Match(
            e => throw new Exception(e.Message),
            id =>
            {
                Assert.Equal(id, result);
                return id;
            });
    }

    [Fact]
    public async Task ResolveExtensionSecret_WithDifferentExtensionId_ReturnsNull()
    {
        ExtensionId fakeId = new("mclovin");
        ExtensionSecret fakeSecret = new("c3VwZXJfc2VjdXJlX3NlY3JldF9rZXk=");
        ExtensionOwnerId fakeOwnerId = new(new("fogle"));

        TwitchClient stubClient = new TwitchClient() { Resolvers = new ImmutableRequestDependencyCollection() }
            .WithExtension(fakeId, fakeOwnerId, fakeSecret)
            .SetFixed<TwitchClient, ExtensionId?>(new("other-id"));

        ResolveRequestDependency<ExtensionSecret?>? resolve = stubClient.GetResolver<ExtensionSecret?>();
        Assert.NotNull(resolve);

        StubDependencyScope scope = new(stubClient.Resolvers);
        Result<ExtensionSecret?> result = await resolve(scope, TestContext.Current.CancellationToken);

        result.Match(
            e => throw new Exception(e.Message),
            secret =>
            {
                Assert.Null(secret);
                return secret;
            });
    }

    [Fact]
    public async Task ResolveExtensionOwnerId_WithDifferentExtensionId_ReturnsNull()
    {
        ExtensionId fakeId = new("mclovin");
        ExtensionSecret fakeSecret = new("c3VwZXJfc2VjdXJlX3NlY3JldF9rZXk=");
        ExtensionOwnerId fakeOwnerId = new(new("fogle"));

        TwitchClient stubClient = new TwitchClient() { Resolvers = new ImmutableRequestDependencyCollection() }
            .WithExtension(fakeId, fakeOwnerId, fakeSecret)
            .SetFixed<TwitchClient, ExtensionId?>(new("other-id"));

        ResolveRequestDependency<ExtensionOwnerId?>? resolve = stubClient.GetResolver<ExtensionOwnerId?>();
        Assert.NotNull(resolve);

        StubDependencyScope scope = new(stubClient.Resolvers);
        Result<ExtensionOwnerId?> result = await resolve(scope, TestContext.Current.CancellationToken);

        result.Match(
            e => throw new Exception(e.Message),
            id =>
            {
                Assert.Null(id);
                return id;
            });
    }
}
