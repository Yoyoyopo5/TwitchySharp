using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Interfaces.Events.Channel.UnbanRequest;

/// <summary>
/// A channel unban request.
/// </summary>
public interface IHaveUnbanRequest
{
    /// <summary>
    /// The id of the unban request.
    /// </summary>
    string Id { get; }
}
