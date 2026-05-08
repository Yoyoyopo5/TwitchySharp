using System.Net.Http;

namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Gets a lists of all shards for a conduit.
/// </summary>
/// <remarks>
/// Requires an app access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-conduit-shards">Get Conduit Shards</see> for more information.
/// </remarks>
public record GetConduitShardsRequest
    : TwitchHelixRequest<GetConduitShardsResponse>, IPageableRequest
{
    protected override string Path => "/eventsub/conduits/shards";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("conduit_id", ConduitId)
            .Add("status", Status?.Value)
            .Add("after", After?.ToString());

    /// <summary>
    /// The conduit id of the conduit you want to get shards for.
    /// </summary>
    public required ConduitId ConduitId { get; init; }

    /// <summary>
    /// Status to filter returned shards by.
    /// </summary>
    public ConduitShardStatus? Status { get; init; }

    /// <inheritdoc/>
    public PaginationCursor? After { get; init; }

    /// <summary>
    /// Unused for this request type.
    /// </summary>
    public PaginationAmount? First { get; init; }
}
