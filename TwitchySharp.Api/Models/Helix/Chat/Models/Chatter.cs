namespace TwitchySharp.Api.Models.Helix.Chat.Models;

/// <summary>
/// Contains user information on a chatter in a broadcaster's stream.
/// </summary>
public record Chatter
{
    /// <summary>
    /// The user id of the chatter.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The user login (username) of the chatter.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the chatter.
    /// </summary>
    public required string UserName { get; init; }
}
