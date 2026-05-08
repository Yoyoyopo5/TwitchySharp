
namespace TwitchySharp.Api.Helix.Schedule;

/// <summary>
/// Contains information about a specific broadcaster's stream schedule.
/// </summary>
public record ChannelStreamSchedule
{
    /// <summary>
    /// The list of broadcasts in the broadcaster’s streaming schedule.
    /// </summary>
    public required ChannelStreamScheduleSegment[] Segments { get; init; }
    /// <summary>
    /// The user id of the broadcaster that owns the broadcast schedule.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The display name of the broadcaster that owns the broadcast schedule.
    /// </summary>
    public required UserName BroadcasterName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster that owns the broadcast schedule.
    /// </summary>
    public required UserLogin BroadcasterLogin { get; init; }
    /// <summary>
    /// The dates when the broadcaster is on vacation and not streaming. 
    /// Is set to <see langword="null"/> if vacation mode is not enabled.
    /// </summary>
    public ChannelStreamScheduleVacation? Vacation { get; init; }

}
