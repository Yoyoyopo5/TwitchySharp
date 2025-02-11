using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Gets a lists of all shards for a conduit.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-conduit-shards">get conduit shards</see> for more information.
/// </summary>
/// <remarks>
/// Requires an app access token.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app access token.</param>
/// <param name="conduitId">The conduit id of the conduit you want to get shards for.</param>
/// <param name="status">Status to filter returned shards by.</param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="Pagination"/> in the response contains the cursor’s value.
/// </param>
public class GetConduitShardsRequest(
    string clientId, 
    string accessToken, 
    string conduitId, 
    ConduitShardStatus? status = null, 
    string? after = null
    )
    : HelixApiRequest<GetConduitShardsResponse>(
        "/eventsub/conduits/shards" +
        new HttpQueryParameters()
            .Add("conduit_id", conduitId)
            .Add("status", status?.Value)
            .Add("after", after),
        clientId,
        accessToken
        );
