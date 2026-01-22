using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch EventSub websocket session.
/// </summary>
/// <remarks>
/// This can be obtained from the welcome message when connecting to the Twitch websocket server.
/// </remarks>
/// <param name="Value">The string value of the id.</param>
[JsonConverter(typeof(WrapperJsonConverter<EventSubWebsocketSessionId, string>))]
public readonly record struct EventSubWebsocketSessionId(string Value) : IWrapValue<string>
{
    public static implicit operator string(EventSubWebsocketSessionId id)
        => id.Value;
    public override string ToString()
        => Value;
}