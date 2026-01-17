using TwitchySharp.Shared.Enums;

namespace TwitchySharp.Api.Models.Helix.Conduits.Models;

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
    public new string? SessionId
    {
        get => base.SessionId;
        set => base.SessionId = value;
    }
}
