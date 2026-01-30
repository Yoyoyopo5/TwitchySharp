using TwitchySharp.Api.Helix.Streams;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Helix.Streams;

public class Test_GetStreamMarkersRequest
{
    private static readonly UserId TestUserId = new("test_user_id");

    [Fact]
    public void BroadcasterStreamMarkersQuery_QueryString_ContainsUserId()
    {
        var userId = new UserId("user123");
        var request = new GetStreamMarkersRequest
        {
            User = new UserIdentity(TestUserId),
            UserId = userId
        };

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("user_id=user123", queryString);
    }

    [Fact]
    public void VideoStreamMarkersQuery_QueryString_ContainsVideoId()
    {
        var videoId = new VideoId("video456");
        var request = new GetStreamMarkersRequest
        {
            User = new UserIdentity(TestUserId),
            VideoId = videoId
        };

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("video_id=video456", queryString);
    }

    [Fact]
    public void BroadcasterStreamMarkersQuery_WithPagination_IncludesPaginationParams()
    {
        var userId = new UserId("user123");
        var request = new GetStreamMarkersRequest
        {
            User = new UserIdentity(TestUserId),
            UserId = userId,
            First = new PaginationAmount(25),
            After = new PaginationCursor("cursor_after")
        };

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
        var request = new GetStreamMarkersRequest
        {
            User = new UserIdentity(TestUserId),
            VideoId = videoId,
            First = new PaginationAmount(50),
            Before = new PaginationCursor("cursor_before")
        };

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("video_id=video456", queryString);
        Assert.Contains("first=50", queryString);
        Assert.Contains("before=cursor_before", queryString);
    }

    [Fact]
    public void GetStreamMarkersRequest_RequestUri_HasCorrectPath()
    {
        var request = new GetStreamMarkersRequest
        {
            User = new UserIdentity(TestUserId),
            UserId = new UserId("user123")
        };

        var uri = request.RequestUri;

        Assert.Equal("/helix/streams/markers", uri.AbsolutePath);
    }

    [Fact]
    public void GetStreamMarkersRequest_Method_IsGet()
    {
        var request = new GetStreamMarkersRequest
        {
            User = new UserIdentity(TestUserId),
            UserId = new UserId("user123")
        };

        Assert.Equal(System.Net.Http.HttpMethod.Get, request.Method);
    }
}
