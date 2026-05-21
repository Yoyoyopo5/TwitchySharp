namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific fragment in a Bits cheer chat message.
/// </summary>
public record BitsChatMessageFragment
{
    public required string Text { get; init; }
    public required BitsChatMessageFragmentType Type { get; init; }
    public BitsChatMessageEmote? Emote { get; init; }
    public BitsChatMessageCheermote? Cheermote { get; init; }
}
