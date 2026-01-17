namespace TwitchySharp.Api.Models.Helix.Chat.Models;

/// <summary>
/// Contains data about a user and their selected chat color.
/// </summary>
public record UserChatColor
{
    /// <summary>
    /// The user's id.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The user's login (username).
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The user's display name.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The hex color code that the user uses in chat for their name.
    /// If the user hasn't specified a color in their settings, the string is empty.
    /// </summary>
    public required string Color { get; init; }
}
