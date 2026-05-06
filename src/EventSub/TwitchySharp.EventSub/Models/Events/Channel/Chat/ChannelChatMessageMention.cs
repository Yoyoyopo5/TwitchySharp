using TwitchySharp.EventSub.Interfaces.Events;

namespace TwitchySharp.EventSub.Models.Events.Channel.Chat;

/// <summary>
/// Contains information about a specific mention in a chat message.
/// </summary>
public record ChannelChatMessageMention : IChatMessageMention, IHaveUser
{
    /// <summary>
    /// The id of the user that was mentioned.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The display name of the user that was mentioned.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The login (username) of the user that was mentioned.
    /// </summary>
    public required string UserLogin { get; init; }
}
