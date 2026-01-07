using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models;
/// <summary>
/// Contains information about how a specific EventSub subscription's notifications are delivered.
/// </summary>
public record EventSubTransport
{
    /// <summary>
    /// The method of transport for EventSub notifications.
    /// </summary>
    public required EventSubTransportMethod Method { get; init; }
    /// <summary>
    /// The callback URL that webhook notifications are sent to.
    /// This is <see langword="null"/> unless <see cref="Method"/> is <see cref="EventSubTransportMethod.Webhook"/>.
    /// </summary>
    public string? Callback { get; init; }
    /// <summary>
    /// The websocket session that notifications are sent to.
    /// This is <see langword="null"/> unless <see cref="Method"/> is <see cref="EventSubTransportMethod.Websocket"/>.
    /// </summary>
    public string? SessionId { get; init; }
}
