namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// A Bits power-up.
/// See <see href="https://help.twitch.tv/s/article/power-ups">Power-ups</see> for more information.
/// </summary>
public record BitsPowerUp
{
    /// <summary>
    /// The type of Power-up.
    /// </summary>
    public required BitsPowerUpType Type { get; init; }
    /// <summary>
    /// The emote that was used with the power-up, if any.
    /// </summary>
    public PowerUpEmote? Emote { get; init; }
    /// <summary>
    /// The id of the message effect that was used with the power-up, if any.
    /// </summary>
    public MessageEffectId? MessageEffectId { get; init; }
}
