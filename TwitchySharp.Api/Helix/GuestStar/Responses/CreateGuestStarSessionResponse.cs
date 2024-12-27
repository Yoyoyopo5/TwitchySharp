using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// Contains data about the newly created Guest Star session.
/// </summary>
public record CreateGuestStarSessionResponse
{
    /// <summary>
    /// A list containing a single entry for the newly created Guest Star session.
    /// </summary>
    public required GuestStarSession[] Data { get; init; }
}
