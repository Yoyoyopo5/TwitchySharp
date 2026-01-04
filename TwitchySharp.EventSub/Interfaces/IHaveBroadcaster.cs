using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Interfaces;
/// <summary>
/// An event associated with a specific broadcaster.
/// </summary>
public interface IHaveBroadcaster
{
    /// <summary>
    /// The user id of the broadcaster (channel) that the event is for.
    /// </summary>
    string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the event is for.
    /// </summary>
    string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the event is for.
    /// </summary>
    string BroadcasterUserName { get; init; }
}
