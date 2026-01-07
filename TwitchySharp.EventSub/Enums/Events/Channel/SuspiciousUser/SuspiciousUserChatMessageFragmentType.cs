using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Enums.Events.Channel.SuspiciousUser;

/// <summary>
/// Contains static definitions of possible chat message fragment types for suspicious user messages.
/// </summary>
/// <param name="Value">The string value of the fragment type.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<SuspiciousUserChatMessageFragmentType, string>))]
public record SuspiciousUserChatMessageFragmentType(string Value) : ValueBackedEnum<string>(Value)
{
    public static SuspiciousUserChatMessageFragmentType Text { get; } = new("text");
    public static SuspiciousUserChatMessageFragmentType Cheermote { get; } = new("cheermote");
    public static SuspiciousUserChatMessageFragmentType Emote { get; } = new("emote");
}
