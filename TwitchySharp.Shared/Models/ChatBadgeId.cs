using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch chat badge.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[JsonConverter(typeof(WrapperJsonConverter<ChatBadgeId, string>))]
public readonly record struct ChatBadgeId(string Value) : IWrapValue<string>
{
    public static implicit operator string(ChatBadgeId id)
        => id.Value;
    public override string ToString()
        => Value;
}