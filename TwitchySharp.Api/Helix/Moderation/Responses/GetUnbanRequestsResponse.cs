using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains a list of unban requests for a specific channel.
/// </summary>
public record GetUnbanRequestsResponse
{
    /// <summary>
    /// The list of unban requests.
    /// </summary>
    public required UnbanRequest[] Data { get; init; }
    /// <inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}
