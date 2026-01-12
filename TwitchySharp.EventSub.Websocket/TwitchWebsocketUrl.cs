using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Websocket;

/// <summary>
/// Contains a constant value for the Twitch EventSub Websocket server address.
/// </summary>
public static class TwitchWebsocketUrl
{
    /// <summary>
    /// The address of the Twitch EventSub Websocket server.
    /// </summary>
    public const string TWITCH_WEBSOCKET_URL = "wss://eventsub.wss.twitch.tv/ws";
}
