using System;
using TwitchySharp.Shared.Enums;

namespace TwitchySharp.Api.Helix.Conduits;

/// <summary>
/// Contains information used to update the webhook transport mechanism of a specific shard.
/// </summary>
public record ConduitWebhookTransportUpdate
    : ConduitTransportUpdate
{
    /// <summary>
    /// <inheritdoc cref="ConduitWebhookTransportUpdate"/>
    /// </summary>
    public ConduitWebhookTransportUpdate()
        => Method = ConduitTransportMethod.Webhook;

    /// <summary>
    /// <inheritdoc cref="ConduitTransportUpdate.Callback/"/>
    /// </summary>
    public new Uri? Callback
    {
        get => base.Callback;
        set => base.Callback = value;
    }

    /// <summary>
    /// <inheritdoc cref="ConduitTransportUpdate.Secret"/>
    /// </summary>
    public new string? Secret
    {
        get => base.Secret;
        set => base.Secret = value;
    }
}
