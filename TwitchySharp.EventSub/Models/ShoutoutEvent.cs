using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models;

/// <summary>
/// The base class for shoutout events.
/// </summary>
/// <remarks>
/// <see cref="EventSubSubscriptionType.ShoutoutCreate"/>,
/// <see cref="EventSubSubscriptionType.ShoutoutReceived"/>.
/// </remarks>
public record ShoutoutEvent
{
    /// <summary>
    /// The number of viewers that were watching the sending broadcaster's stream at the time of the shoutout.
    /// </summary>
    public required int ViewerCount { get; init; }
    /// <summary>
    /// The date and time when the shoutout was sent.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
}
