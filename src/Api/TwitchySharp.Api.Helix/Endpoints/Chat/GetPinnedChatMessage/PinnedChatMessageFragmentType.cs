using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// Contains static definitions for pinned chat message fragment types.
/// </summary>
/// <param name="Value">The string value of the type.</param>
[Wrapper<string>]
public readonly partial record struct PinnedChatMessageFragmentType(string Value)
{
    public static PinnedChatMessageFragmentType Text { get; } = new("text");
    public static PinnedChatMessageFragmentType Emote { get; } = new("emote");
    public static PinnedChatMessageFragmentType Cheermote { get; } = new("cheermote");
    public static PinnedChatMessageFragmentType Mention { get; } = new("mention");
}
