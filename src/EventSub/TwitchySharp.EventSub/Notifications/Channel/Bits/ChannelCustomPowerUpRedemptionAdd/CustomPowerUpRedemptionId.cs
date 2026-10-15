using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// A id for a specific custom Bits power-up redemption.
/// </summary>
/// <param name="Value">The value of the id.</param>
[Wrapper<string>]
public readonly partial record struct CustomPowerUpRedemptionId(string Value);
