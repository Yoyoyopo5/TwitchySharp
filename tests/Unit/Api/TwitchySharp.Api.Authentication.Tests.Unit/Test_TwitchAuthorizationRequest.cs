using TwitchySharp.Api.Authorization;
using TwitchySharp.Infrastructure.Http;

namespace TwitchySharp.Api.Tests.Unit.Authorization;

public class Test_TwitchAuthorizationRequest
{
    [Fact]
    public void RequestUri_WithDefaultHostAndBasePath_BuildsCorrectUri()
    {
        StubAuthorizationRequest request = new()
        {
            StubPath = "/token"
        };

        Uri uri = request.RequestUri;

        Assert.Equal("https", uri.Scheme);
        Assert.Equal("id.twitch.tv", uri.Host);
        Assert.Equal("/oauth2/token", uri.AbsolutePath);
    }

    [Fact]
    public void RequestUri_WithCustomHost_UsesCustomHost()
    {
        StubAuthorizationRequest request = new()
        {
            Host = "custom.twitch.tv",
            StubPath = "/token"
        };

        Uri uri = request.RequestUri;

        Assert.Equal("custom.twitch.tv", uri.Host);
        Assert.Equal("/oauth2/token", uri.AbsolutePath);
    }

    [Fact]
    public void RequestUri_WithCustomBasePath_UsesCustomBasePath()
    {
        StubAuthorizationRequest request = new()
        {
            BasePath = "/custom",
            StubPath = "/token"
        };

        Uri uri = request.RequestUri;

        Assert.Equal("/custom/token", uri.AbsolutePath);
    }

    [Fact]
    public void RequestUri_WithQueryParameters_IncludesQueryString()
    {
        StubAuthorizationRequest request = new()
        {
            StubPath = "/authorize",
            StubQueryParameters = new HttpQueryParameters()
                .Add("client_id", "test_client")
                .Add("response_type", "code")
        };

        Uri uri = request.RequestUri;

        Assert.Contains("client_id=test_client", uri.Query);
        Assert.Contains("response_type=code", uri.Query);
    }

    [Fact]
    public void RequestUri_WithoutQueryParameters_HasNoQueryString()
    {
        StubAuthorizationRequest request = new()
        {
            StubPath = "/keys"
        };

        Uri uri = request.RequestUri;

        Assert.Empty(uri.Query);
    }

    [Fact]
    public void RequestUri_WithMultiValueQueryParameter_IncludesAllValues()
    {
        StubAuthorizationRequest request = new()
        {
            StubPath = "/authorize",
            StubQueryParameters = new HttpQueryParameters()
                .Add("scope", ["channel:read:polls", "channel:manage:polls", "openid"])
        };

        Uri uri = request.RequestUri;

        Assert.Contains("scope=channel%3Aread%3Apolls", uri.Query);
        Assert.Contains("scope=channel%3Amanage%3Apolls", uri.Query);
        Assert.Contains("scope=openid", uri.Query);
    }

    private record StubAuthorizationRequest : TwitchAuthorizationRequest<object>
    {
        public string StubPath { get; init; } = "/stub";
        public HttpQueryParameters? StubQueryParameters { get; init; }

        protected override string Path => StubPath;
        public override HttpMethod Method => HttpMethod.Get;
        protected override HttpQueryParameters? QueryParameters => StubQueryParameters;
    }
}
