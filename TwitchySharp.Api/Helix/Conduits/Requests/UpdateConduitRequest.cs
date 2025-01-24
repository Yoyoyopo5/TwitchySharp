using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Updates a conduit’s shard count. To delete shards, update the count to a lower number, and the shards above the count will be deleted. 
/// For example, if the existing shard count is 100, by resetting shard count to 50, shards 50-99 are disabled.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-conduits">update conduits</see> for more information.
/// </summary>
/// <remarks>
/// Requires an app access token.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app access token.</param>
/// <param name="conduitToUpdate">The data to update the conduit with.</param>
public class UpdateConduitRequest(string clientId, string accessToken, UpdateConduitRequestData conduitToUpdate)
    : HelixApiRequest<UpdateConduitResponse, UpdateConduitRequestData>(
        "/eventsub/conduits",
        clientId,
        accessToken,
        conduitToUpdate
        )
{
    public override HttpMethod Method => HttpMethod.Patch;
}

/// <summary>
/// Contains data used to update an existing conduit.
/// </summary>
public record UpdateConduitRequestData
{
    /// <summary>
    /// The conduit id of the conduit you want to update.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The new number of shards to assign to this conduit.
    /// </summary>
    public required int ShardCount { get; init; }
}
