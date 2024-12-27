using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// Contains a list with Guest Star session information.
/// </summary>
public record GetGuestStarSessionResponse
{
    /// <summary>
    /// A list with a single entry of the Guest Star session details.
    /// </summary>
    public required GuestStarSession[] Data { get; init; }
}