using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// Contains details about the session that was ended.
/// </summary>
public record EndGuestStarSessionResponse
{
    /// <summary>
    /// Contains a single entry of a summary of the session details when the session was ended.
    /// </summary>
    public required GuestStarSession[] Data { get; init; }
}
