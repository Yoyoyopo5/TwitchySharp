using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Defines the background theme of an emote.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))] // So we can use array of strings to enum array
public enum EmoteTheme
{
    Dark,
    Light
}
