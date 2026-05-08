
namespace TwitchySharp.Api.Helix.Moderation;

/// <summary>
/// Contains information about a user's moderated channel.
/// </summary>
public record ModeratedChannel
{
    /// <summary>
    /// The user id of the broadcaster (channel) that the user is a moderator for.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the user is a moderator for.
    /// </summary>
    public required UserLogin BroadcasterLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the user is a moderator for.
    /// </summary>
    public required UserName BroadcasterName { get; init; }
}
