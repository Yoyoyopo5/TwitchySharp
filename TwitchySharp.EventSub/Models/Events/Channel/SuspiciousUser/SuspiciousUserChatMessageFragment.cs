using TwitchySharp.EventSub.Enums.Events.Channel.SuspiciousUser;
using TwitchySharp.EventSub.Interfaces;
using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Models.Events.Channel.SuspiciousUser;

/// <summary>
/// Contains information about a specific fragment of a suspicious user chat message.
/// </summary>
public record SuspiciousUserChatMessageFragment : IChatMessageFragment
{
    public required string Text { get; init; }
    public required SuspiciousUserChatMessageFragmentType Type { get; init; }
    ValueBackedEnum<string> IChatMessageFragment.Type => Type;

    public SuspiciousUserChatMessageEmote? Emote { get; init; }
    IChatMessageEmote? IChatMessageFragment.Emote => Emote;
    public SuspiciousUserChatMessageCheermote? Cheermote { get; init; }
    IChatMessageCheermote? IChatMessageFragment.Cheermote => Cheermote;
    /// <summary>
    /// Not supported for this event type.
    /// Defaults to <see langword="null"/>.
    /// </summary>
    public IChatMessageMention? Mention => null;
}
