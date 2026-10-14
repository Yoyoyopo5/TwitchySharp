namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// A custom Bits power-up.
/// </summary>
/// <remarks>
/// See <see href="https://help.twitch.tv/s/article/power-ups">Power-ups</see> for more information.
/// </remarks>
public record CustomBitsPowerUp
{
    /// <summary>
    /// The title of the custom Power-up.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The id of the custom Power-up.
    /// </summary>
    public required CustomBitsPowerUpId RewardId { get; init; }
}
