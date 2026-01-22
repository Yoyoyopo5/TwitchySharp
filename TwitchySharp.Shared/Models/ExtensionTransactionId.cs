using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch extension Bits transaction.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[JsonConverter(typeof(WrapperJsonConverter<ExtensionTransactionId, string>))]
public readonly record struct ExtensionTransactionId(string Value) : IWrapValue<string>
{
    public static implicit operator string(ExtensionTransactionId id)
        => id.Value;
    public override string ToString()
        => Value;
}