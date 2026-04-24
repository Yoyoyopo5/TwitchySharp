using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.HypeTrain;

/// <summary>
/// Contains information about a broadcaster participating in a Hype Train.
/// </summary>
public record HypeTrainParticipant
{
    /// <summary>
    /// The user id of the broadcaster participating in the Hype Train.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster participating in the Hype Train.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster participating in the Hype Train.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
}
