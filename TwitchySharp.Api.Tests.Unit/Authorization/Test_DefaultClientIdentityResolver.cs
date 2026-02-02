using System.Collections.Immutable;
using System.Net.Http;
using System.Text.Json;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Authorization;

public class Test_DefaultClientIdentityResolver
{
    private const string DefaultClientIdValue = "default_client_id";
    private const string ConfiguredClientIdValue = "configured_client_id";

    private static readonly ClientIdentity DefaultClientIdentity = new(new ClientId(DefaultClientIdValue));
    private static readonly ClientIdentity ConfiguredClientIdentity = new(new ClientId(ConfiguredClientIdValue));

    [Fact]
    public async Task GetClientId_RequestWithConfiguredIdentity_ReturnsConfiguredIdentity()
    {
        var resolver = new DefaultClientIdentityResolver(DefaultClientIdentity);
        var request = new MockAuthorizableRequest(ConfiguredClientIdentity);

        var result = await resolver.GetClientId(request);

        AssertClientIdentity(result, ConfiguredClientIdValue);
    }

    [Fact]
    public async Task GetClientId_RequestWithoutConfiguredIdentity_ReturnsDefaultIdentity()
    {
        var resolver = new DefaultClientIdentityResolver(DefaultClientIdentity);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        var result = await resolver.GetClientId(request);

        AssertClientIdentity(result, DefaultClientIdValue);
    }

    [Fact]
    public async Task GetClientId_NonAuthorizableRequest_ReturnsDefaultIdentity()
    {
        var resolver = new DefaultClientIdentityResolver(DefaultClientIdentity);
        var request = new MockNonAuthorizableRequest();

        var result = await resolver.GetClientId(request);

        AssertClientIdentity(result, DefaultClientIdValue);
    }

    private static void AssertClientIdentity(ClientIdentity? result, string expectedClientId)
    {
        Assert.NotNull(result);
        Assert.Equal(expectedClientId, result.ClientId.Value);
    }

    /// <summary>
    /// Mock request that implements IRequireAuthorization for testing.
    /// </summary>
    private record MockAuthorizableRequest(TwitchApiIdentity Identity) : ITwitchRequest, IRequireAuthorization
    {
        public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet<Scope>.Empty;
        public AccessToken? OverrideAccessToken => null;

        public HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions) => new();
    }

    /// <summary>
    /// Mock request that does NOT implement IRequireAuthorization for testing.
    /// </summary>
    private record MockNonAuthorizableRequest() : ITwitchRequest
    {
        public HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions) => new();
    }
}
