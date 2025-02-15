using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications;
public record EventSubTransport
{
    public required EventSubTransportMethod Method { get; init; }
    public string? Callback { get; init; } // Set if Method is webhook
    public string? SessionId { get; init; } // Set if Method is websocket
}
