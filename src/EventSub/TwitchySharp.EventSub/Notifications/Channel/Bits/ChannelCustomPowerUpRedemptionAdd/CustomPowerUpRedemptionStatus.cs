using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains static definitions for possible statuses of a custom Bits power-up redemption.
/// </summary>
/// <param name="Value">The value of the status.</param>
[Wrapper<string>]
public readonly partial record struct CustomPowerUpRedemptionStatus(string Value)
{
    public static CustomPowerUpRedemptionStatus Unknown { get; } = new("unknown");
    public static CustomPowerUpRedemptionStatus Unfulfilled { get; } = new("unfulfilled");
    public static CustomPowerUpRedemptionStatus Fulfilled { get; } = new("fulfilled");
}
