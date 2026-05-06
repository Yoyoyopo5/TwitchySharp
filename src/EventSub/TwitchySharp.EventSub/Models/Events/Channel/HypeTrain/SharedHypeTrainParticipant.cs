using TwitchySharp.EventSub.Interfaces.Events;

namespace TwitchySharp.EventSub.Models.Events.Channel.HypeTrain;

/// <summary>
/// Contains information about a specific broadcaster (channel) participating in a Hype Train in a shared chat.
/// </summary>
public record SharedHypeTrainParticipant : IHaveBroadcaster
{
    /// <summary>
    /// The user id of the broadcaster participating in the shared chat Hype Train.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster participating in the shared chat Hype Train.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster participating in the shared chat Hype Train.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
}
