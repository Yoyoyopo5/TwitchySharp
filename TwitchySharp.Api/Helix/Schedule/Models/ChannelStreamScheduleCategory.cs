using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Schedule;

/// <summary>
/// Contains information about a category in a channel's stream schedule.
/// </summary>
public record ChannelStreamScheduleCategory
{
    /// <summary>
    /// The id of the category the broadcaster plans to stream.
    /// </summary>
    public required GameId Id { get; init; }
    /// <summary>
    /// The name of the category the broadcaster plans to stream.
    /// </summary>
    public required string Name { get; init; }
}
