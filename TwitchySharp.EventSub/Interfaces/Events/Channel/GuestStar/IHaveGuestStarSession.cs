using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Interfaces.Events.Channel.GuestStar;

/// <summary>
/// A guest star session.
/// </summary>
public interface IHaveGuestStarSession
{
    /// <summary>
    /// The id of the Guest Star session that was started.
    /// </summary>
    string SessionId { get; }
    /// <summary>
    /// The date and time when the Guest Star session began.
    /// </summary>
    DateTimeOffset StartedAt { get; }
}
