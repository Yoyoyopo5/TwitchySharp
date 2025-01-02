using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Polls;
/// <summary>
/// Contains information about the ended poll.
/// </summary>
public record EndPollResponse
{
    /// <summary>
    /// A list containing the poll that was ended.
    /// </summary>
    public required ChatPoll[] Data { get; init; }
}
