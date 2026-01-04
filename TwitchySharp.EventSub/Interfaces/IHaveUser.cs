using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Interfaces;

/// <summary>
/// An event associated with a specific user. 
/// </summary>
public interface IHaveUser
{
    /// <summary>
    /// The user id of the user associated with the event.
    /// </summary>
    string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user associated with the event.
    /// </summary>
    string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user associated with the event.
    /// </summary>
    string UserName { get; init; }
}
