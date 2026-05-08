namespace TwitchySharp.Api.Helix.Schedule;
/// <inheritdoc cref="ChannelStreamSchedule"/>
public record GetChannelStreamScheduleResponse
    : IPageableResponse
{
    /// <summary>
    /// The broadcaster’s streaming schedule.
    /// </summary>
    public required ChannelStreamSchedule Data { get; init; } // Interestingly, not an array this time.
    /// <inheritdoc cref="Api.Pagination"/>
    public required Pagination Pagination { get; init; }
}
