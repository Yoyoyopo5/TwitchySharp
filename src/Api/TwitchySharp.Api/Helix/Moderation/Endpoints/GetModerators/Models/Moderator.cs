using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;

/// <summary>
/// Contains information about a specific channel moderator.
/// </summary>
public record Moderator
{
    /// <summary>
    /// The user id of the moderator.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the moderator.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the moderator.
    /// </summary>
    public required UserName UserName { get; init; }
}
