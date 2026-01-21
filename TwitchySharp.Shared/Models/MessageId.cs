using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch chat message.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[JsonConverter(typeof(WrapperJsonConverter<MessageId, string>))]
public readonly record struct MessageId(string Value) : IWrapValue<string>
{
    public static implicit operator string(MessageId id)
        => id.Value;
    public override string ToString()
        => Value;
}