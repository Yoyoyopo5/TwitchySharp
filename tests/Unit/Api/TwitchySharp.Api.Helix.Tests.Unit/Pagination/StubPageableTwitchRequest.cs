namespace TwitchySharp.Api.Helix.Tests.Unit.Pagination;

public record StubPageableResponseContent : IPageableResponse
{
    public Api.Pagination Pagination { get; init; }
}

public record StubPageableTwitchRequest
    : TwitchRequest<StubPageableResponseContent>, IPageableRequest
{
    public PaginationCursor? After { get; init; }
    public PaginationAmount? First { get; init; }

    public override HttpMethod Method => throw new NotImplementedException();
    public override Uri RequestUri => throw new NotImplementedException();
}
