using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Enums.Events.Automod.Message;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Interfaces.Events.Automod.Message;

/// <summary>
/// The interface for Automod Message Update events.
/// </summary>
/// <remarks>
/// <see cref="EventSubSubscriptionType.AutomodMessageUpdate"/>,
/// <see cref="EventSubSubscriptionType.AutomodMessageUpdateV2"/>,
/// </remarks>
public interface IAutomodMessageUpdateEvent : IHaveModerator
{
    /// <summary>
    /// The status of the updated automod message.
    /// </summary>
    AutomodMessageUpdateStatus Status { get; }
}
