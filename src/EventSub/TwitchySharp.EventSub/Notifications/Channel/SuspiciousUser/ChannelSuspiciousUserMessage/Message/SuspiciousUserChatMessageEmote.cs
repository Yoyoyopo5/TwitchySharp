namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about an emote used in a suspicious user chat message.
/// </summary>
public record SuspiciousUserChatMessageEmote
{
    /// <summary>
    /// The id of the emote.
    /// </summary>
    public required EmoteId Id { get; init; }
    /// <summary>
    /// The id of the emote set.
    /// </summary>
    public required EmoteSetId EmoteSetId { get; init; }
}
