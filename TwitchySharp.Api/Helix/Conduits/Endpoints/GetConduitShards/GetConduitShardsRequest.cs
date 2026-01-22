using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Enums;
using TwitchySharp.Shared.Models;

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
    : TwitchHelixRequest<GetConduitShardsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app access token.</param>
    /// <param name="conduitId">The conduit id of the conduit you want to get shards for.</param>
    /// <param name="status">Status to filter returned shards by.</param>
    /// <param name="after">
    /// The cursor used to get the next page of results. 
    /// The <see cref="Pagination"/> in the response contains the cursor’s value.
    /// </param>
    public GetConduitShardsRequest(
        ClientId clientId,
        AppAccessToken accessToken,
        GetConduitShardsRequestParameters parameters
        )
        : base(
            "/eventsub/conduits/shards",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("conduit_id", parameters.ConduitId)
                .Add("status", parameters.Status?.Value)
                .Add("after", parameters.After?.Value)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetConduitShardsRequest"/>.
/// </summary>
public record GetConduitShardsRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// The conduit id of the conduit you want to get shards for.
    /// </summary>
    public required ConduitId ConduitId { get; set; }
    /// <summary>
    /// Status to filter returned shards by.
    /// </summary>
    public ConduitShardStatus? Status { get; set; }
    public PaginationCursor? After { get; set; }
    /// <summary>
    /// Unused for this request type.
    /// </summary>
    public PaginationAmount? First { get; set; }
}
