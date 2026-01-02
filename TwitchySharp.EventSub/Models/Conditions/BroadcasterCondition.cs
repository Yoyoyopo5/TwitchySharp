using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Models.Conditions;

/// <summary>
/// An EventSub notification condition with only a broadcaster user id.
/// </summary>
public record BroadcasterCondition
{
    /// <summary>
    /// The user id of the broadcaster (channel) that the notification is for.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
}