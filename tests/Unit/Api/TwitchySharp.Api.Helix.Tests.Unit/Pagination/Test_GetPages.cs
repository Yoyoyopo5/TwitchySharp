namespace TwitchySharp.Api.Helix.Tests.Unit.Pagination;

public class Test_GetPages
{
    public class StubPaginationTwitchClient(int pages) : ITwitchClient
    {
        private int _pages = pages;

        public Task<TwitchResponse<TResponseContent>> SendAsync<TResponseContent>(
            TwitchRequest<TResponseContent> request,
            CancellationToken ct = default
            )
            => request is StubPageableTwitchRequest pageableRequest
            ? Task.FromResult(new TwitchResponse<TResponseContent>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Request = request,
                Content = new StubPageableResponseContent() { Pagination = new() { Cursor = _pages == 0 ? null : new(_pages--.ToString()) } } is TResponseContent content
                    ? content
                    : throw new NotImplementedException()
            })
            : throw new NotImplementedException();
    }

    [Fact]
    public async Task GetPages_ClientWithFourPages_AllResponsesWithExpectedPagination()
    {
        StubPageableTwitchRequest request = new();
        StubPaginationTwitchClient client = new(3);

        IAsyncEnumerable<TwitchResponse<StubPageableResponseContent>> results
            = request.GetPages<StubPageableTwitchRequest, StubPageableResponseContent>(client, TestContext.Current.CancellationToken);

        Assert.Collection(results,
            result => Assert.Equal(new(3.ToString()), result.Content.Pagination.Cursor),
            result => Assert.Equal(new(2.ToString()), result.Content.Pagination.Cursor),
            result => Assert.Equal(new(1.ToString()), result.Content.Pagination.Cursor),
            result => Assert.Null(result.Content.Pagination.Cursor)
            );
    }
}
