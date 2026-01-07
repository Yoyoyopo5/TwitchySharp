namespace TwitchySharp.EventSub.Models.Events.Channel.Chat;

/// <summary>
/// Contains information about a cheer in a chat message.
/// </summary>
public record ChannelChatMessageCheer
{
    /// <summary>
    /// The amount of bits the user cheered.
    /// </summary>
    public required int Bits { get; init; }
}
