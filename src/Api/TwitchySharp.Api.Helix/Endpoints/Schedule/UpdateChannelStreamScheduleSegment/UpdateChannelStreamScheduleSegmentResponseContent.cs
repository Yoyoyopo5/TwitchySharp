namespace TwitchySharp.Api.Helix.Schedule;
/// <inheritdoc cref="ChannelStreamSchedule"/>
public record UpdateChannelStreamScheduleSegmentResponseContent
{
    /// <summary>
    /// The broadcaster's updated streaming schedule.
    /// </summary>
    public required ChannelStreamSchedule Data { get; init; }
}
