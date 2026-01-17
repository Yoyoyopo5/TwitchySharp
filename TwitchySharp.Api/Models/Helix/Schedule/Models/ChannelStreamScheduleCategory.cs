namespace TwitchySharp.Api.Models.Helix.Schedule.Models;

/// <summary>
/// Contains information about a category in a channel's stream schedule.
/// </summary>
public record ChannelStreamScheduleCategory
{
    /// <summary>
    /// The id of the category the broadcaster plans to stream.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The name of the category the broadcaster plans to stream.
    /// </summary>
    public required string Name { get; init; }
}
