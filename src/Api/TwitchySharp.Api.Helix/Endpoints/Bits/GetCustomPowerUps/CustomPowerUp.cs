namespace TwitchySharp.Api.Helix.Bits;

/// <summary>
/// Contains information about a specific custom Bits power-up.
/// </summary>
public record CustomPowerUp
{
    /// <summary>
    /// The user id of the broadcaster who owns the custom power-up.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The login of the broadcaster who owns the custom power-up.
    /// </summary>
    public required UserLogin BroadcasterLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster who owns the custom power-up.
    /// </summary>
    public required UserName BroadcasterName { get; init; }
    /// <summary>
    /// The id of the power-up.
    /// </summary>
    public required CustomBitsPowerUpId Id { get; init; }
    /// <summary>
    /// The title of the power-up.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The prompt shown to the viewer when they redeem the custom power-up
    /// if <see cref="IsUserInputRequired"/> is <see langword="true"/>.
    /// </summary>
    public string? Prompt { get; init; }
    /// <summary>
    /// The amount of Bits required to redeem the custom power-up.
    /// </summary>
    public required int Bits { get; init; }
    /// <summary>
    /// The set of custom images for the power-up.
    /// </summary>
    /// <remarks>
    /// This is <see langword="null"/> if the broadcaster didn't upload images.
    /// </remarks>
    public CustomPowerUpImageSet? Image { get; init; }
    /// <summary>
    /// The set of default images for the power-up
    /// </summary>
    public required CustomPowerUpImageSet DefaultImage { get; init; }
    /// <summary>
    /// The background color used for the power-up.
    /// </summary>
    public required RgbColor BackgroundColor { get; init; }
    /// <summary>
    /// Indicates whether the power-up is enabled.
    /// </summary>
    /// <remarks>
    /// Disabled power-ups are not shown to viewers.
    /// </remarks>
    public required bool IsEnabled { get; init; }
    /// <summary>
    /// Indicates whether the user must enter text when redeeming the power-up.
    /// </summary>
    public required bool IsUserInputRequired { get; init; }
    /// <summary>
    /// The setting for max power-up uses per stream.
    /// </summary>
    public required MaxPerStreamSetting MaxPerStreamSetting { get; init; }
    /// <summary>
    /// The setting for max power-up uses per user per stream.
    /// </summary>
    public required MaxPerUserPerStreamSetting MaxPerUserPerStreamSetting { get; init; }
    /// <summary>
    /// The setting for the power-up's global cooldown.
    /// </summary>
    public required GlobalCooldownSetting GlobalCooldownSetting { get; init; }
    /// <summary>
    /// Indicates whether the power-up redemptions are currently paused.
    /// </summary>
    /// <remarks>
    /// Viewers cannot redeem paused power-ups.
    /// </remarks>
    public required bool IsPaused { get; init; }
    /// <summary>
    /// Indicates whether the power-up has available redemptions remaining for the current stream.
    /// </summary>
    public required bool IsInStock { get; init; }
    /// <summary>
    /// The number of redemptions redeemed during the current stream.
    /// </summary>
    /// <remarks>
    /// This is <see langword="null"/> if the stream is not live or
    /// the <see cref="MaxPerStreamSetting"/> is not enabled.
    /// </remarks>
    public int? RedemptionsRedeemedCurrentStream { get; init; }
    /// <summary>
    /// The time when the power-up redemption cooldown expires.
    /// </summary>
    /// <remarks>
    /// This is <see langword="null"/> if the power-up is off cooldown or
    /// <see cref="GlobalCooldownSetting"/> is not enabled.
    /// </remarks>
    public DateTimeOffset? CooldownExpiresAt { get; init; }
}
