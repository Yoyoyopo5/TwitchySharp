namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific emote in a chat message.
/// </summary>
public record ChannelChatMessageEmote
{
    /// <summary>
    /// The id of the emote.
    /// </summary>
    public required EmoteId Id { get; init; }
    /// <summary>
    /// The id of the set the emote belongs to.
    /// </summary>
    public required EmoteSetId EmoteSetId { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) who owns the emote.
    /// </summary>
    public required UserId OwnerId { get; init; }
    /// <summary>
    /// The formats the emote is available in.
    /// </summary>
    public required ChatMessageEmoteFormat[] Format { get; init; }
}
