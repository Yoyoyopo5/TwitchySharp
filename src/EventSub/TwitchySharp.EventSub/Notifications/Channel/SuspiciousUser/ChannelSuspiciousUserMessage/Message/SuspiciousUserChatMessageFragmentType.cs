using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains static definitions of possible chat message fragment types for suspicious user messages.
/// </summary>
/// <param name="Value">The string value of the fragment type.</param>
[Wrapper<string>]
public readonly partial record struct SuspiciousUserChatMessageFragmentType(string Value)
{
    public static SuspiciousUserChatMessageFragmentType Text { get; } = new("text");
    public static SuspiciousUserChatMessageFragmentType Cheermote { get; } = new("cheermote");
    public static SuspiciousUserChatMessageFragmentType Emote { get; } = new("emote");
}
