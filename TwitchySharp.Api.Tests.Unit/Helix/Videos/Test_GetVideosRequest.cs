using TwitchySharp.Api.Helix.Videos;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Helix.Videos;

public class Test_GetVideosRequest
{
    private static readonly ClientId TestClientId = new("test_client_id");
    private static readonly AppAccessToken TestAccessToken = new("test_access_token");

    [Fact]
    public void VideoIdQuery_QueryString_ContainsIds()
    {
        var videoIds = new[] { new VideoId("123"), new VideoId("456") };
        var query = new VideoIdQuery(videoIds);
        var request = new GetVideosRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("id=123", queryString);
        Assert.Contains("id=456", queryString);
    }

    [Fact]
    public void VideoUserQuery_QueryString_ContainsUserId()
    {
        var userId = new UserId("user123");
        var query = new VideoUserQuery(userId);
        var request = new GetVideosRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("user_id=user123", queryString);
    }

    [Fact]
    public void VideoUserQuery_WithPagination_IncludesPaginationParams()
    {
        var userId = new UserId("user123");
        var query = new VideoUserQuery(userId)
        {
            First = new PaginationAmount(50),
            After = new PaginationCursor("cursor123")
        };
        var request = new GetVideosRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("user_id=user123", queryString);
        Assert.Contains("first=50", queryString);
        Assert.Contains("after=cursor123", queryString);
    }

    [Fact]
    public void VideoGameQuery_QueryString_ContainsGameId()
    {
        var gameId = new GameId("game123");
        var query = new VideoGameQuery(gameId);
        var request = new GetVideosRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("game_id=game123", queryString);
    }

    [Fact]
    public void VideoGameQuery_WithLanguage_IncludesLanguageParam()
    {
        var gameId = new GameId("game123");
        LanguageCode.TryParse("en", out var languageCode);
        var query = new VideoGameQuery(gameId)
        {
            Language = languageCode
        };
        var request = new GetVideosRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("game_id=game123", queryString);
        Assert.Contains("language=en", queryString);
    }

    [Fact]
    public void VideoUserQuery_WithFilterParams_IncludesAllParams()
    {
        var userId = new UserId("user123");
        var query = new VideoUserQuery(userId)
        {
            Period = VideoQueryPeriod.Week,
            Sort = VideoQuerySort.Views,
            Type = VideoQueryType.Highlight
        };
        var request = new GetVideosRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("user_id=user123", queryString);
        Assert.Contains("period=week", queryString);
        Assert.Contains("sort=views", queryString);
        Assert.Contains("type=highlight", queryString);
    }

    [Fact]
    public void VideoIdQuery_WithMultipleIds_IncludesAllIds()
    {
        var videoIds = new[]
        {
            new VideoId("111"),
            new VideoId("222"),
            new VideoId("333")
        };
        var query = new VideoIdQuery(videoIds);
        var request = new GetVideosRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("id=111", queryString);
        Assert.Contains("id=222", queryString);
        Assert.Contains("id=333", queryString);
    }

    [Fact]
    public void GetVideosRequest_RequestUri_HasCorrectPath()
    {
        var query = new VideoIdQuery([new VideoId("123")]);
        var request = new GetVideosRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;

        Assert.Equal("/helix/videos", uri.AbsolutePath);
    }

    [Fact]
    public void GetVideosRequest_RequestUri_HasCorrectHost()
    {
        var query = new VideoIdQuery([new VideoId("123")]);
        var request = new GetVideosRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;

        Assert.Equal("api.twitch.tv", uri.Host);
    }
}
