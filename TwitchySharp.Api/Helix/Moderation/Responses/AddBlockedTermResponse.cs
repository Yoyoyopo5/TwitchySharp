using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains a list of newly blocked terms.
/// </summary>
public record AddBlockedTermResponse
{
    /// <summary>
    /// A list containing the single new term that was blocked.
    /// </summary>
    public required BlockedTerm[] Data { get; init; }
}
