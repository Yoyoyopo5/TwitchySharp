using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch chat badge set.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[JsonConverter(typeof(WrapperJsonConverter<ChatBadgeSetId, string>))]
public readonly record struct ChatBadgeSetId(string Value) : IWrapValue<string>
{
    public static implicit operator string(ChatBadgeSetId id)
        => id.Value;
    public override string ToString()
        => Value;
}