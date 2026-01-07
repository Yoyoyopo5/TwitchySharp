using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Enums.Events.Automod.Message;

/// <summary>
/// Contains static definitions for potential Automod message fragment types.
/// </summary>
/// <param name="Value">The string value for the Automod message fragment type.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<AutomodCaughtMessageFragmentType, string>))]
public record AutomodCaughtMessageFragmentType(string Value)
    : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// A text fragment.
    /// </summary>
    public static AutomodCaughtMessageFragmentType Text { get; } = new("text");
    /// <summary>
    /// An emote fragment.
    /// </summary>
    public static AutomodCaughtMessageFragmentType Emote { get; } = new("emote");
    /// <summary>
    /// A bits cheermote fragment.
    /// </summary>
    public static AutomodCaughtMessageFragmentType Cheermote { get; } = new("cheermote");
}
