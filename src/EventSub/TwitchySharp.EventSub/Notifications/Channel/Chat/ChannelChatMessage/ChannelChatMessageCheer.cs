namespace TwitchySharp.EventSub.Notifications;

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
