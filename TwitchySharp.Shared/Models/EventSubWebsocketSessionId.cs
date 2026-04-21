using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch EventSub websocket session.
/// </summary>
/// <remarks>
/// This can be obtained from the welcome message when connecting to the Twitch websocket server.
/// </remarks>
/// <param name="Value">The string value of the id.</param>
[Wrapper<string>]
public readonly partial record struct EventSubWebsocketSessionId(string Value);