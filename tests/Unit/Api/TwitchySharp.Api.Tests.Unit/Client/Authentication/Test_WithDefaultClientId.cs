using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Unit.Client.Authentication;

public class Test_WithDefaultClientId
{
    private static TwitchClient CreateMockClient(TwitchIdentity? identity, ClientId defaultClientId, Action<TwitchIdentity?> capture)
        => new TwitchClient() { Resolvers = new ImmutableRequestDependencyCollection() }
            .SetFixed(identity)
            .WithDefaultClientId(defaultClientId)
            .Configure<TwitchClient, TwitchIdentity?>(next => (scope, ct) =>
                next(scope, ct).MapAsync(identity =>
                {
                    capture(identity);
                    return identity;
                }
            ))
            .UseStubResponse<TwitchIdentity?>();

    [Fact]
    public async Task ResolveOrDefault_TwitchIdentity_WhenTwitchIdentityClientIdIsNull_TwitchIdentityHasDefaultClientId()
    {
        TwitchIdentity identity = TwitchIdentity.Client.Default;
        ClientId defaultClientId = new("12345");

        TwitchIdentity? resolvedIdentity = null;

        await CreateMockClient(identity, defaultClientId, identity => resolvedIdentity = identity).SendStubRequest(TestContext.Current.CancellationToken);

        Assert.Equal(defaultClientId, resolvedIdentity?.ClientId);
    }

    [Fact]
    public async Task ResolveOrDefault_TwitchIdentity_WhenTwitchIdentityClientIdIsNotNull_TwitchIdentityHasOriginalClientId()
    {
        ClientId configuredClientId = new("678910");
        TwitchIdentity identity = new TwitchIdentity.Client(new(configuredClientId));
        ClientId defaultClientId = new("12345");

        TwitchIdentity? resolvedIdentity = null;

        await CreateMockClient(identity, defaultClientId, identity => resolvedIdentity = identity).SendStubRequest(TestContext.Current.CancellationToken);

        Assert.Equal(configuredClientId, resolvedIdentity?.ClientId);
    }
}
