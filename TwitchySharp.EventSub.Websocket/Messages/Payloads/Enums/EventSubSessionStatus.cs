using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Websocket.Messages.Payloads;
/// <summary>
/// Contains static definitions for possible EventSub session statuses.
/// </summary>
/// <param name="Value"></param>

[Wrapper<string>]
public readonly partial record struct EventSubSessionStatus(string Value)
{
    /// <summary>
    /// The value used in a welcome message.
    /// </summary>
    public static EventSubSessionStatus Connected { get; } = new("connected");
    /// <summary>
    /// The value used in a reconnect session message.
    /// </summary>
    public static EventSubSessionStatus Reconnecting { get; } = new("reconnecting");
}
