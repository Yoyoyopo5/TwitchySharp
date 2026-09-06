namespace TwitchySharp.Api.Helix.Schedule;
/// <inheritdoc cref="ChannelStreamSchedule"/>
public record CreateChannelStreamScheduleSegmentResponseContent
{
    /// <summary>
    /// The broadcaster's updated streaming schedule.
    /// </summary>
    public required ChannelStreamSchedule Data { get; init; }
}
