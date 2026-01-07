using TwitchySharp.EventSub.Enums.Events.Channel;
using TwitchySharp.EventSub.Interfaces.Events;

namespace TwitchySharp.EventSub.Models.Events.Channel;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Ban"/> or <see cref="ChannelModerateActionType.SharedChatBan"/> action.
/// </summary>
public record ChannelModerateBanAction : IHaveUser
{
    /// <summary>
    /// The id of the user that was banned.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that was banned.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that was banned.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The moderator-provided reason for the ban, if any.
    /// </summary>
    public string? Reason { get; init; }
}
