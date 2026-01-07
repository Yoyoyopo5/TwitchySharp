using TwitchySharp.EventSub.Models.Events.Channel.Chat;

namespace TwitchySharp.EventSub.Interfaces.Events.Channel.Chat;

/// <summary>
/// A channel chat message.
/// </summary>
public interface IHaveChannelChatMessage
{
    /// <summary>
    /// The id of the message.
    /// </summary>
    string MessageId { get; }
    /// <summary>
    /// The message.
    /// </summary>
    ChannelChatMessage Message { get; }
}
