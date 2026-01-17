using System.Net.Http;
using TwitchySharp.Api.Models.Helix.Conduits.Responses;
using TwitchySharp.Api.Models.Shared;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Enums;

namespace TwitchySharp.Api.Models.Helix.Conduits.Requests;
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
        string clientId,
        string accessToken,
        string conduitId,
        ConduitShardStatus? status = null,
        string? after = null
        )
        : base(
            "/eventsub/conduits/shards",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("conduit_id", conduitId)
                .Add("status", status?.Value)
                .Add("after", after)
            )
    {
        Method = HttpMethod.Get;
    }
}
