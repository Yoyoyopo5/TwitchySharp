using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Tests.Unit.Pagination;

public class Test_PreviousPage
{
    [Fact]
    public void PreviousPage_WithPaginationCursor_ReturnsRequestWithBeforeEqualCursor()
    {
        PaginationCursor mockCursor = new("test-cursor");
        StubPageableTwitchRequest request = new();

        StubPageableTwitchRequest pagedRequest = request.PreviousPage(mockCursor);

        Assert.Equal(mockCursor, pagedRequest.Before);
    }

    [Fact]
    public void PreviousPage_WithNullPaginationCursor_ReturnsNull()
    {
        PaginationCursor? nullCursor = null;
        StubPageableTwitchRequest request = new();

        StubPageableTwitchRequest? pagedRequest = request.PreviousPage(nullCursor);

        Assert.Null(pagedRequest);
    }
}
