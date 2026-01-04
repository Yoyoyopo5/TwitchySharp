using TwitchySharp.EventSub.Enums.Automod.Message;

namespace TwitchySharp.EventSub.Models.Automod.Message;

/// <summary>
/// Contains information about an individual message fragment that triggered Automod.
/// </summary>
public record AutomodCaughtMessageFragment
{
    /// <summary>
    /// The type of message fragment.
    /// </summary>
    public required AutomodCaughtMessageFragmentType Type { get; init; }
    /// <summary>
    /// The text of the fragment.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// The emote that triggered the Automod, if any.
    /// </summary>
    public AutomodCaughtChatEmote? Emote { get; init; }
    /// <summary>
    /// The bits cheermote that triggered the Automod, if any.
    /// </summary>
    public AutomodCaughtCheermote? Cheermote { get; init; }
}
