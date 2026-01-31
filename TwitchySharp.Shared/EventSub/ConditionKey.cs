using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.EventSub;

/// <summary>
/// Represents a specific EventSub Subscription condition key.
/// </summary>
/// <remarks>
/// See <see cref="https://dev.twitch.tv/docs/eventsub/eventsub-reference/#conditions">Conditions</see> for more information.
/// </remarks>
/// <param name="Value">The string value of the key.</param>
[JsonConverter(typeof(WrapperJsonConverter<ConditionKey, string>))]
public readonly record struct ConditionKey(string Value) : IWrapValue<string>
{
    public static implicit operator string(ConditionKey key)
        => key.Value;
    public override string ToString()
        => Value;
}