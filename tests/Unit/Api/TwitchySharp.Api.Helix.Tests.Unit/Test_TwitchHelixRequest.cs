using TwitchySharp.Api.Helix;
using TwitchySharp.Infrastructure.Http;

namespace TwitchySharp.Api.Tests.Unit.Helix;

public class Test_TwitchHelixRequest
{
    [Fact]
    public void RequestUri_WithDefaultHostAndBasePath_BuildsCorrectUri()
    {
        StubHelixRequest fakeRequest = new()
        {
            FakePath = "/test"
        };

        Uri uri = fakeRequest.RequestUri;

        Assert.Equal("https", uri.Scheme);
        Assert.Equal("api.twitch.tv", uri.Host);
        Assert.Equal("/helix/test", uri.AbsolutePath);
        Assert.Equal("https://api.twitch.tv/helix/test", uri.AbsoluteUri);
    }

    [Fact]
    public void RequestUri_WithCustomHost_UsesCustomHost()
    {
        StubHelixRequest request = new()
        {
            Host = "custom.twitch.tv",
            FakePath = "/test"
        };

        Uri uri = request.RequestUri;

        Assert.Equal("custom.twitch.tv", uri.Host);
        Assert.Equal("/helix/test", uri.AbsolutePath);
        Assert.Equal("https://custom.twitch.tv/helix/test", uri.AbsoluteUri);
    }

    [Fact]
    public void RequestUri_WithCustomBasePath_UsesCustomBasePath()
    {
        StubHelixRequest request = new()
        {
            BasePath = "/custom",
            FakePath = "/test"
        };

        Uri uri = request.RequestUri;

        Assert.Equal("/custom/test", uri.AbsolutePath);
    }

    private record StubHelixRequest : TwitchHelixRequest<object>
    {
        public string FakePath { get; init; } = "/stub";
        public TwitchIdentity? StubDefaultIdentity { get; init; }
        public HttpQueryParameters? StubQueryParameters { get; init; }

        protected override string Path => FakePath;
        public override HttpMethod Method => HttpMethod.Get;
        protected override HttpQueryParameters? QueryParameters => StubQueryParameters;
    }
}
