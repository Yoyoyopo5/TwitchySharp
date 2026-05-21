namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific emote used in a Bits cheer chat message.
/// </summary>
public record BitsChatMessageEmote
{
    public required EmoteId Id { get; init; }
    public required EmoteSetId EmoteSetId { get; init; }
    public required UserId OwnerId { get; init; }
    public required ChatMessageEmoteFormat[] Format { get; init; }
}
