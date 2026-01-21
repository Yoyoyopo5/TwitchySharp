using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific shared chat.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[JsonConverter(typeof(WrapperJsonConverter<SharedChatSessionId, string>))]
public readonly record struct SharedChatSessionId(string Value) : IWrapValue<string>
{
    public static implicit operator string(SharedChatSessionId id)
        => id.Value;
    public override string ToString()
        => Value;
}