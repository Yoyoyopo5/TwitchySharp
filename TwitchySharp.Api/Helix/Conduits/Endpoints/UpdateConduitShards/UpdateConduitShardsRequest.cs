using System.Net.Http;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Updates shard(s) for a conduit.
/// </summary>
/// <remarks>
/// Requires an app access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-conduit-shards">Update Conduit Shards</see> for more information.
/// </remarks>
public record UpdateConduitShardsRequest
    : TwitchHelixRequest<UpdateConduitShardsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app access token.</param>
    /// <param name="updates">Data used to update the shards.</param>
    public UpdateConduitShardsRequest(
        ClientId clientId,
        AppAccessToken accessToken,
        UpdateConduitShardsRequestData updates
        )
        : base(
            "/eventsub/conduits/shards",
            clientId,
            accessToken
            )
    {
        Method = HttpMethod.Patch;
        ContentObject = updates;
    }
}

/// <summary>
/// Contains data used to update shards on a specific conduit.
/// </summary>
public record UpdateConduitShardsRequestData
{
    /// <summary>
    /// The id of the conduit to update shards on.
    /// </summary>
    public required ConduitId ConduitId { get; set; }
    /// <summary>
    /// The shards to update.
    /// </summary>
    public required ConduitShardUpdate[] Shards { get; set; }
}
