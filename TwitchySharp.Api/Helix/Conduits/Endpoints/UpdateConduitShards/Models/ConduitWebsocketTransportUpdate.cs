using TwitchySharp.Shared.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Conduits;

/// <summary>
/// Contains information used to update the websocket transport mechanism of a specific shard.
/// </summary>
public record ConduitWebsocketTransportUpdate
    : ConduitTransportUpdate
{
    /// <summary>
    /// <inheritdoc cref="ConduitWebsocketTransportUpdate"/>
    /// </summary>
    public ConduitWebsocketTransportUpdate()
        => Method = ConduitTransportMethod.Websocket;

    /// <summary>
    /// <inheritdoc cref="ConduitTransportUpdate.SessionId"/>
    /// </summary>
    public new EventSubWebsocketSessionId? SessionId
    {
        get => base.SessionId;
        init => base.SessionId = value;
    }
}
