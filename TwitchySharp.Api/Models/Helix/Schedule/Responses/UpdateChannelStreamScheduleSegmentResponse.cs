using TwitchySharp.Api.Models.Helix.Schedule.Models;

namespace TwitchySharp.Api.Models.Helix.Schedule.Responses;
/// <inheritdoc cref="ChannelStreamSchedule"/>
public record UpdateChannelStreamScheduleSegmentResponse
{
    /// <summary>
    /// The broadcaster's updated streaming schedule.
    /// </summary>
    public required ChannelStreamSchedule Data { get; init; }
}
