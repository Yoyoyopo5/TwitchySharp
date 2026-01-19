namespace TwitchySharp.Api.Helix.Ads;
/// <summary>
/// Contains information about the snoozed ad.
/// </summary>
public record SnoozeNextAdResponse
{
    /// <summary>
    /// A list that contains information about the channel’s snoozes and next upcoming ad after successfully snoozing.
    /// </summary>
    public required AdSnoozeData[] Data { get; init; }
}
