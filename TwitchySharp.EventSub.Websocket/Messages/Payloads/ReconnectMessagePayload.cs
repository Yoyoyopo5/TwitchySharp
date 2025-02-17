using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Websocket.Messages.Payloads;
internal class ReconnectMessagePayload
{
    public required EventSubReconnectSession Session { get; init; }
}
