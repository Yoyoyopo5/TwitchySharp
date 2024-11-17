using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Contains a list of user's and their selected chat colors.
/// </summary>
public record GetUserChatColorResponse
{
    /// <summary>
    /// The list of users.
    /// </summary>
    public required UserChatColor[] Data { get; init; }
}

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
