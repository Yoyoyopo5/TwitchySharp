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
    public required EventSubSubscription Subscription { get; init; }
}
