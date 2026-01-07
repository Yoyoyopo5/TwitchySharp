using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Enums.Events.Channel;

namespace TwitchySharp.EventSub.Models.Events.Channel;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.DeleteMessage"/> or <see cref="ChannelModerateActionType.SharedChatDeleteMessage"/> action.
/// </summary>
public record ChannelModerateDeleteMessageAction : IHaveUser
{
    /// <summary>
    /// The id of user whose message is being deleted.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of user whose message is being deleted.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of user whose message is being deleted.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The id of the message that was deleted.
    /// </summary>
    public required string MessageId { get; init; }
    /// <summary>
    /// The message that was deleted, in <see langword="string"/> format.
    /// </summary>
    public required string MessageBody { get; init; }
}
