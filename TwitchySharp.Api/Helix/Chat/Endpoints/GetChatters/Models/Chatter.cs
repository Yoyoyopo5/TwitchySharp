using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// Contains user information on a chatter in a broadcaster's stream.
/// </summary>
public record Chatter
{
    /// <summary>
    /// The user id of the chatter.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The user login (username) of the chatter.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the chatter.
    /// </summary>
    public required UserName UserName { get; init; }
}
