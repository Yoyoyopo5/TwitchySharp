using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Interfaces.Events;

/// <summary>
/// An event associated with a specific moderator.
/// </summary>
public interface IHaveModerator
{
    /// <summary>
    /// The user id of the moderator associated with the event.
    /// </summary>
    string ModeratorUserId { get; }
    /// <summary>
    /// The display name of the moderator associated with the event.
    /// </summary>
    string ModeratorUserName { get; }
    /// <summary>
    /// The login (username) of the moderator associated with the event.
    /// </summary>
    string ModeratorUserLogin { get; }
}
