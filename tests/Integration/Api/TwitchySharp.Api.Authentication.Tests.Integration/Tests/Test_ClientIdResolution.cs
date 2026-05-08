
namespace TwitchySharp.Api.AuthorizationResolution.Tests.Integration.Tests;

public class Test_ClientIdResolution(TokenResolutionTestFixture fixture)
    : IClassFixture<TokenResolutionTestFixture>
{
    private readonly TokenResolutionTestFixture _fixture = fixture;

    [Fact]
    public async Task SendRequest_WithConfiguredClientId_ConfiguredIdUsed()
    {
        // Arrange
        const string CLIENT_ID_VALUE = "12345";
        ClientId clientId = new(CLIENT_ID_VALUE);
        TwitchRequestAuthorizationContext context = new()
        {
            Identity = new TwitchIdentity.Client(clientId)
        };
        TestAuthorizedTwitchRequest request = new() { AuthorizationContext = context };
        TwitchAuthorizationResolutionOptions options = new()
        {
            FallbackClientIdResolver = (_, _) => ValueTask.FromResult<ClientId?>(new ClientId("0000"))
        };
        ITwitchClient client = _fixture.CreateTestClient(options);

        var response = await client.SendAsync(request);

        Assert.Equal(CLIENT_ID_VALUE, response.Content.RequestAuthorizationHeaders.ClientId?.Value);
    }

    [Fact]
    public async Task SendRequest_WithNoneIdentity_NoClientIdHeaderValue()
    {
        // Arrange
        TwitchRequestAuthorizationContext context = new()
        {
            Identity = TwitchIdentity.None.Instance
        };
        TestAuthorizedTwitchRequest request = new() { AuthorizationContext = context };
        TwitchAuthorizationResolutionOptions options = new()
        {
            FallbackClientIdResolver = (_, _) => ValueTask.FromResult<ClientId?>(new ClientId("0000"))
        };
        ITwitchClient client = _fixture.CreateTestClient(options);

        var response = await client.SendAsync(request);

        Assert.Null(response.Content.RequestAuthorizationHeaders.ClientId);
    }

    [Fact]
    public async Task SendRequest_WithDefaultIdentity_FallbackClientIdUsed()
    {
        // Arrange
        const string FALLBACK_CLIENT_ID = "12345";
        TwitchRequestAuthorizationContext context = new()
        {
            Identity = TwitchIdentity.Client.Default
        };
        TestAuthorizedTwitchRequest request = new() { AuthorizationContext = context };
        TwitchAuthorizationResolutionOptions options = new()
        {
            FallbackClientIdResolver = (_, _) => ValueTask.FromResult<ClientId?>(new ClientId(FALLBACK_CLIENT_ID))
        };
        ITwitchClient client = _fixture.CreateTestClient(options);

        var response = await client.SendAsync(request);

        Assert.Equal(FALLBACK_CLIENT_ID, response.Content.RequestAuthorizationHeaders.ClientId?.Value);
    }
}
