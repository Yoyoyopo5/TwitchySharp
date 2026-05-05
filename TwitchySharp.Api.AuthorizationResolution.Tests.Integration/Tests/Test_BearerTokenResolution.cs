namespace TwitchySharp.Api.AuthorizationResolution.Tests.Integration.Tests;

public class Test_BearerTokenResolution(TokenResolutionTestFixture fixture)
    : IClassFixture<TokenResolutionTestFixture>
{
    private readonly TokenResolutionTestFixture _fixture = fixture;

    [Fact]
    public async Task SendRequest_WithExplicitAccessToken_ExplicitTokenUsed()
    {
        const string ACCESS_TOKEN_VALUE = "12345";
        AppAccessToken token = new(ACCESS_TOKEN_VALUE);
        TwitchRequestAuthorizationContext context = new()
        {
            Identity = new TwitchIdentity.Client(new()),
            AccessToken = token
        };
        TestAuthorizedTwitchRequest request = new() { AuthorizationContext = context };
        TwitchAuthorizationResolutionOptions options = new()
        {
            FallbackBearerTokenResolver = (_, _) => ValueTask.FromResult<IAccessToken?>(new AppAccessToken("0000"))
        };
        ITwitchClient client = _fixture.CreateTestClient(options);

        var response = await client.SendAsync(request);

        Assert.Equal(ACCESS_TOKEN_VALUE, response.Content.RequestAuthorizationHeaders.BearerToken?.Value);
    }

    [Fact]
    public async Task SendRequest_WithFallbackAccessToken_FallbackTokenUsed()
    {
        const string ACCESS_TOKEN_VALUE = "12345";
        AppAccessToken token = new(ACCESS_TOKEN_VALUE);
        TwitchRequestAuthorizationContext context = new()
        {
            Identity = new TwitchIdentity.Client(new())
        };
        TestAuthorizedTwitchRequest request = new() { AuthorizationContext = context };
        TwitchAuthorizationResolutionOptions options = new()
        {
            FallbackBearerTokenResolver = (_, _) => ValueTask.FromResult<IAccessToken?>(token)
        };
        ITwitchClient client = _fixture.CreateTestClient(options);

        var response = await client.SendAsync(request);

        Assert.Equal(ACCESS_TOKEN_VALUE, response.Content.RequestAuthorizationHeaders.BearerToken?.Value);
    }

    [Fact]
    public async Task SendRequest_WithNoneIdentity_NullAuthorizationHeaderValue()
    {
        TwitchRequestAuthorizationContext context = new()
        {
            Identity = TwitchIdentity.None.Instance
        };
        TestAuthorizedTwitchRequest request = new() { AuthorizationContext = context };
        TwitchAuthorizationResolutionOptions options = new()
        {
            FallbackBearerTokenResolver = (_, _) => ValueTask.FromResult<IAccessToken?>(new AppAccessToken("0000"))
        };
        ITwitchClient client = _fixture.CreateTestClient(options);

        var response = await client.SendAsync(request);

        Assert.Null(response.Content.RequestAuthorizationHeaders.BearerToken);
    }
}
