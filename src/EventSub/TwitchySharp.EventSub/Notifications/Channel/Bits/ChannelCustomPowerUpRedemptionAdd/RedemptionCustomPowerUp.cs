namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// A custom Bits power-up.
/// </summary>
public record RedemptionCustomPowerUp
{
    /// <summary>
    /// The id of the custom Bits power-up.
    /// </summary>
    public required CustomBitsPowerUpId Id { get; init; }
    /// <summary>
    /// The name of the power-up.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The amount of bits required to redeem the power-up.
    /// </summary>
    public required int Bits { get; init; }
    /// <summary>
    /// The creator-provided description for the power-up.
    /// </summary>
    public required string Prompt { get; init; }
}
