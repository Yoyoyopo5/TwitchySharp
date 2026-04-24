using TwitchySharp.Api.Helix;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Helix;

public class Test_TwitchHelixRequest
{
    [Fact]
    public void RequestUri_WithDefaultHostAndBasePath_BuildsCorrectUri()
    {
        var request = new StubHelixRequest
        {
            StubPath = "/test"
        };

        var uri = request.RequestUri;

        Assert.Equal("https", uri.Scheme);
        Assert.Equal("api.twitch.tv", uri.Host);
        Assert.Equal("/helix/test", uri.AbsolutePath);
    }

    [Fact]
    public void RequestUri_WithCustomHost_UsesCustomHost()
    {
        var request = new StubHelixRequest
        {
            Host = "custom.twitch.tv",
            StubPath = "/test"
        };

        var uri = request.RequestUri;

        Assert.Equal("custom.twitch.tv", uri.Host);
        Assert.Equal("/helix/test", uri.AbsolutePath);
    }

    [Fact]
    public void RequestUri_WithCustomBasePath_UsesCustomBasePath()
    {
        var request = new StubHelixRequest
        {
            BasePath = "/custom",
            StubPath = "/test"
        };

        var uri = request.RequestUri;

        Assert.Equal("/custom/test", uri.AbsolutePath);
    }

    [Fact]
    public void Identity_WhenNotSet_FallsBackToDefaultIdentity()
    {
        var defaultIdentity = new TwitchIdentity.User(new UserId("default_user"));
        var request = new StubHelixRequest
        {
            StubDefaultIdentity = defaultIdentity
        };

        Assert.Equal(defaultIdentity, request.AuthorizationContext.Identity);
    }

    [Fact]
    public void Identity_WhenSet_OverridesDefaultIdentity()
    {
        var defaultIdentity = new TwitchIdentity.User(new UserId("default_user"));
        var overrideIdentity = new TwitchIdentity.User(new UserId("override_user"));
        var request = new StubHelixRequest
        {
            StubDefaultIdentity = defaultIdentity,
            AuthorizationContext = new() { Identity = overrideIdentity }
        };

        Assert.Equal(overrideIdentity, request.AuthorizationContext.Identity);
        Assert.NotEqual(defaultIdentity, request.AuthorizationContext.Identity);
    }

    [Fact]
    public void Identity_WhenDefaultIdentityNotOverridden_UsesStaticDefault()
    {
        var request = new StubHelixRequest();

        Assert.Equal(TwitchIdentity.Client.Default, request.AuthorizationContext.Identity);
    }

    [Fact]
    public void RequestUri_WithPath_BuildsCorrectUri()
    {
        var request = new StubHelixRequest
        {
            StubPath = "/test/endpoint"
        };

        var uri = request.RequestUri;

        Assert.Equal("https", uri.Scheme);
        Assert.Equal("api.twitch.tv", uri.Host);
        Assert.Equal("/helix/test/endpoint", uri.AbsolutePath);
    }

    [Fact]
    public void RequestUri_WithQueryParameters_IncludesQueryString()
    {
        var request = new StubHelixRequest
        {
            StubPath = "/test",
            StubQueryParameters = new HttpQueryParameters()
                .Add("param1", "value1")
                .Add("param2", "value2")
        };

        var uri = request.RequestUri;

        Assert.Contains("param1=value1", uri.Query);
        Assert.Contains("param2=value2", uri.Query);
    }

    [Fact]
    public void RequestUri_WithoutQueryParameters_HasNoQueryString()
    {
        var request = new StubHelixRequest
        {
            StubPath = "/test"
        };

        var uri = request.RequestUri;

        Assert.Empty(uri.Query);
    }

    [Fact]
    public void RequestUri_WithMultiValueQueryParameter_IncludesAllValues()
    {
        var request = new StubHelixRequest
        {
            StubPath = "/test",
            StubQueryParameters = new HttpQueryParameters()
                .Add("id", ["123", "456", "789"])
        };

        var uri = request.RequestUri;

        Assert.Contains("id=123", uri.Query);
        Assert.Contains("id=456", uri.Query);
        Assert.Contains("id=789", uri.Query);
    }

    private record StubHelixRequest : TwitchHelixRequest<object>
    {
        public string StubPath { get; init; } = "/stub";
        public TwitchIdentity? StubDefaultIdentity { get; init; }
        public HttpQueryParameters? StubQueryParameters { get; init; }

        protected override string Path => StubPath;
        public override HttpMethod Method => HttpMethod.Get;
        protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext
            => StubDefaultIdentity switch
            {
                null => base.DefaultAuthorizationContext,
                TwitchIdentity identity => new()
                {
                    Identity = identity
                }
            };
        protected override HttpQueryParameters? QueryParameters => StubQueryParameters;
    }
}
