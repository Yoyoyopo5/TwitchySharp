using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Updates shard(s) for a conduit.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-conduit-shards">update conduit shards</see> for more information.
/// </summary>
/// <remarks>
/// Requires an app access token.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app access token.</param>
/// <param name="updates">Data used to update the shards.</param>
public class UpdateConduitShardsRequest(string clientId, string accessToken, UpdateConduitShardsRequestData updates)
    : HelixApiRequest<UpdateConduitShardsResponse, UpdateConduitShardsRequestData>(
        "/eventsub/conduits/shards",
        clientId,
        accessToken,
        updates
        )
{
    public override HttpMethod Method => HttpMethod.Patch;
}

/// <summary>
/// Contains data used to update shards on a specific conduit.
/// </summary>
public record UpdateConduitShardsRequestData
{
    /// <summary>
    /// The id of the conduit to update shards on.
    /// </summary>
    public required string ConduitId { get; set; }
    /// <summary>
    /// The shards to update.
    /// </summary>
    public required ConduitShardUpdate[] Shards { get; set; }
}

/// <summary>
/// Contains information used to update a specific shard.
/// </summary>
public record ConduitShardUpdate
{
    /// <summary>
    /// The id of the shard to update.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The transport details that you want to update the shard to.
    /// </summary>
    public required ConduitTransportUpdate Transport { get; set; }
}

/// <summary>
/// Contains information used to update the transport mechanism of a specific shard.
/// </summary>
public record ConduitTransportUpdate
{
    /// <summary>
    /// The method to use for the transport.
    /// </summary>
    [JsonConverter(typeof(SnakeCaseJsonStringEnumConverter<ConduitTransportMethod>))]
    public ConduitTransportMethod? Method { get; set; }
    /// <summary>
    /// The callback url where webhook notifications are sent.
    /// The URL must use the HTTPS protocol and port 443.
    /// Specify this field only if <see cref="Method"/> is set to <see cref="ConduitTransportMethod.Webhook"/>.
    /// <b>Note:</b> Redirects are not followed.
    /// </summary>
    public string? Callback { get; set; }
    /// <summary>
    /// The secret used to verify the signature of a webhook notification.
    /// The secret must be an ASCII string that’s a minimum of 10 characters long and a maximum of 100 characters long.
    /// Specify this field only if <see cref="Method"/> is set to <see cref="ConduitTransportMethod.Webhook"/>.
    /// </summary>
    public string? Secret { get; set; }
    /// <summary>
    /// The id of the WebSocket connection to send notifications to.
    /// When you connect to EventSub using WebSockets, the server returns this id in the Welcome message.
    /// Specify this field only if <see cref="Method"/> is set to <see cref="ConduitTransportMethod.Websocket"/>.
    /// </summary>
    public string? SessionId { get; set; }
}
