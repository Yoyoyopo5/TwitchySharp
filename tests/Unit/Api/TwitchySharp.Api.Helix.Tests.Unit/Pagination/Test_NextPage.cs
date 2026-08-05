namespace TwitchySharp.Api.Helix.Tests.Unit.Pagination;

public class Test_NextPage
{
    [Fact]
    public void NextPage_WithPaginationCursor_ReturnsRequestWithAfterEqualCursor()
    {
        PaginationCursor mockCursor = new("test-cursor");
        StubPageableTwitchRequest request = new();

        StubPageableTwitchRequest pagedRequest = request.NextPage(mockCursor);

        Assert.Equal(mockCursor, pagedRequest.After);
    }

    [Fact]
    public void NextPage_WithNullPaginationCursor_ReturnsNull()
    {
        PaginationCursor? nullCursor = null;
        StubPageableTwitchRequest request = new();

        StubPageableTwitchRequest? pagedRequest = request.NextPage(nullCursor);

        Assert.Null(pagedRequest);
    }
}
