using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models;

namespace TwitchySharp.EventSub.Websocket.Messages.Payloads;
public class RevocationMessagePayload
{
    public required EventSubSubscription Subscription { get; init; }
}
