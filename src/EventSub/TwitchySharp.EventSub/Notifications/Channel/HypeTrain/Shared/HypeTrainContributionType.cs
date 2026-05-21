using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains static definitions for possible Hype Train contribution types.
/// </summary>
/// <param name="Value">The string value of the contribution type.</param>
[Wrapper<string>]
public readonly partial record struct HypeTrainContributionType(string Value)
{
    /// <summary>
    /// Bits contributions with Cheering, Power-ups, and Extensions. 
    /// </summary>
    public static HypeTrainContributionType Bits { get; } = new("bits");
    /// <summary>
    /// Subscription activity like subscribing or gifting subscriptions. 
    /// </summary>
    public static HypeTrainContributionType Subscription { get; } = new("subscription");
    /// <summary>
    /// Covers other contribution methods not listed.
    /// </summary>
    public static HypeTrainContributionType Other { get; } = new("other");
}
