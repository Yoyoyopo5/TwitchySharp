using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Websocket;

/// <summary>
/// A <see cref="Stream"/> wrapper for an incoming Twitch EventSub Websocket message.
/// </summary>
/// <param name="Value">The <see cref="Stream"/> value of the Websocket message stream.</param>
[Wrapper<Stream>]
public readonly partial record struct WebsocketMessageStream(Stream Value);
