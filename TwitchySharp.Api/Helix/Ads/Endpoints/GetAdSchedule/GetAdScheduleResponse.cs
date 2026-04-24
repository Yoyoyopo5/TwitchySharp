namespace TwitchySharp.Api.Helix.Ads;
/// <summary>
/// Contains information about a channel's ad schedule.
/// </summary>
public record GetAdScheduleResponse
{
    /// <summary>
    /// A list that contains information related to the channel’s ad schedule.
    /// There should only be one entry?
    /// </summary>
    public required AdSchedule[] Data { get; init; }
}
