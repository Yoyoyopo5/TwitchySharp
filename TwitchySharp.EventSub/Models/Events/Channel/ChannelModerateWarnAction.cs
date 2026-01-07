using TwitchySharp.EventSub.Enums.Events.Channel;
using TwitchySharp.EventSub.Interfaces.Events;

namespace TwitchySharp.EventSub.Models.Events.Channel;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Warn"/> action.
/// </summary>
public record ChannelModerateWarnAction : IHaveUser
{
    /// <summary>
    /// The id of the user being warned.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user being warned.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user being warned.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The reason given for the warning.
    /// </summary>
    public string? Reason { get; init; }
    /// <summary>
    /// Chat rules cited for the warning.
    /// </summary>
    public string[]? ChatRulesCited { get; init; }
}
