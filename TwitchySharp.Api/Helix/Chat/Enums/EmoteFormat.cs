using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Defines the format of the emote.
/// </summary>

[JsonConverter(typeof(JsonStringEnumConverter))] // So we can use array of strings to enum array
public enum EmoteFormat
{
    Animated,
    Static
}
