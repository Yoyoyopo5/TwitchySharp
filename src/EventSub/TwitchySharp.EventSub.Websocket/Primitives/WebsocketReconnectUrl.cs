using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Websocket;

/// <summary>
/// A Twitch EventSub Websocket reconnect url.
/// </summary>
/// <remarks>
/// Reconnect an EventSub Websocket session to this url to continue receieving events.
/// </remarks>
/// <param name="Value">The string value of the url.</param>
[Wrapper<string>]
public readonly partial record struct WebsocketReconnectUrl(string Value)
{
    public Uri ToUri() => new(Value);
}
