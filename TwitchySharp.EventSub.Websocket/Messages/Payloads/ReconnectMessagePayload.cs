using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Websocket.Messages.Payloads;
/// <summary>
/// A reconnect message payload.
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/handling-websocket-events#reconnect-message">Reconnect Message</see> for more information.
/// </remarks>
public class ReconnectMessagePayload
{
    /// <summary>
    /// The reconnection session details.
    /// This contains the URL to reconnect to.
    /// </summary>
    public required EventSubReconnectSession Session { get; init; }
}
