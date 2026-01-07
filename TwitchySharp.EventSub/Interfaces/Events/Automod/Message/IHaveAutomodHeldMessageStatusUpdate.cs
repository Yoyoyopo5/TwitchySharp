using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Enums.Events.Automod.Message;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Interfaces.Events.Automod.Message;

/// <summary>
/// A status update for an Automod held message.
/// </summary>
public interface IHaveAutomodHeldMessageStatusUpdate
{
    /// <summary>
    /// The status of the updated automod message.
    /// </summary>
    AutomodMessageUpdateStatus Status { get; }
}
