using TwitchySharp.Api.Helix.Streams;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Helix.Streams;

public class Test_GetStreamMarkersRequest
{
    private static readonly ClientId TestClientId = new("test_client_id");
    private static readonly UserAccessToken TestAccessToken = new("test_access_token");

    [Fact]
    public void BroadcasterStreamMarkersQuery_QueryString_ContainsUserId()
    {
        var userId = new UserId("user123");
        var query = new BroadcasterStreamMarkersQuery(userId);
        var request = new GetStreamMarkersRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("user_id=user123", queryString);
    }

    [Fact]
    public void VideoStreamMarkersQuery_QueryString_ContainsVideoId()
    {
        var videoId = new VideoId("video456");
        var query = new VideoStreamMarkersQuery(videoId);
        var request = new GetStreamMarkersRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("video_id=video456", queryString);
    }

    [Fact]
    public void BroadcasterStreamMarkersQuery_WithPagination_IncludesPaginationParams()
    {
        var userId = new UserId("user123");
        var query = new BroadcasterStreamMarkersQuery(userId)
        {
            First = new PaginationAmount(25),
            After = new PaginationCursor("cursor_after")
        };
        var request = new GetStreamMarkersRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("user_id=user123", queryString);
        Assert.Contains("first=25", queryString);
        Assert.Contains("after=cursor_after", queryString);
    }

    [Fact]
    public void VideoStreamMarkersQuery_WithPagination_IncludesPaginationParams()
    {
        var videoId = new VideoId("video456");
        var query = new VideoStreamMarkersQuery(videoId)
        {
            First = new PaginationAmount(50),
            Before = new PaginationCursor("cursor_before")
        };
        var request = new GetStreamMarkersRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("video_id=video456", queryString);
        Assert.Contains("first=50", queryString);
        Assert.Contains("before=cursor_before", queryString);
    }

    [Fact]
    public void GetStreamMarkersRequest_RequestUri_HasCorrectPath()
    {
        var query = new BroadcasterStreamMarkersQuery(new UserId("user123"));
        var request = new GetStreamMarkersRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;

        Assert.Equal("/helix/streams/markers", uri.AbsolutePath);
    }

    [Fact]
    public void GetStreamMarkersRequest_Method_IsGet()
    {
        var query = new BroadcasterStreamMarkersQuery(new UserId("user123"));
        var request = new GetStreamMarkersRequest(TestClientId, TestAccessToken, query);

        Assert.Equal(System.Net.Http.HttpMethod.Get, request.Method);
    }

    [Fact]
    public void GetStreamMarkersRequest_RequestUri_HasCorrectHost()
    {
        var query = new BroadcasterStreamMarkersQuery(new UserId("user123"));
        var request = new GetStreamMarkersRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;

        Assert.Equal("api.twitch.tv", uri.Host);
    }
}
