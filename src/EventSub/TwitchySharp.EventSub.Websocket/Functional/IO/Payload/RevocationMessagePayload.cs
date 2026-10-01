using System.Collections.Immutable;

namespace TwitchySharp.EventSub.Websocket.Functional;
/// <summary>
/// A subscription revocation message payload.
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/handling-websocket-events#revocation-message">Recovation Message</see> for more information.
/// </remarks>
public readonly record struct RevocationMessagePayload
{
    /// <summary>
    /// The subscription being revoked.
    /// </summary>
    // We use ImmutableDictionary vs FrozenDictionary because STJ won't deserialize Frozen natively.
    public required EventSubSubscription<ImmutableDictionary<string, string>> Subscription { get; init; }
}
