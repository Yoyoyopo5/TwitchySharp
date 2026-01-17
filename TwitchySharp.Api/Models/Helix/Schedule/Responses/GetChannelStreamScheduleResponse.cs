using TwitchySharp.Api.Models.Helix.Schedule.Models;
using TwitchySharp.Api.Models.Shared;

namespace TwitchySharp.Api.Models.Helix.Schedule.Responses;
/// <inheritdoc cref="ChannelStreamSchedule"/>
public record GetChannelStreamScheduleResponse
{
    /// <summary>
    /// The broadcaster’s streaming schedule.
    /// </summary>
    public required ChannelStreamSchedule Data { get; init; } // Interestingly, not an array this time.
    /// <inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}
