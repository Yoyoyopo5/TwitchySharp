using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// Contains data about a user and their selected chat color.
/// </summary>
public record UserChatColor
{
    /// <summary>
    /// The user's id.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The user's login (username).
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The user's display name.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The color that the user uses in chat for their name.
    /// If the user hasn't specified a color in their settings, the string is empty (defaults to black).
    /// </summary>
    public required RgbColor Color { get; init; }
}
