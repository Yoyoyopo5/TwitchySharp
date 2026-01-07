using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Interfaces.Events;

/// <summary>
/// An event associated with a specific client (app).
/// </summary>
public interface IHaveClient
{
    /// <summary>
    /// The client id of the application the event is associated with.
    /// </summary>
    string ClientId { get; }
}
