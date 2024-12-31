using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains a list of resolved unban requests.
/// </summary>
public record ResolveUnbanRequestsResponse
{
    /// <summary>
    /// A list containing the single resolved unban request.
    /// </summary>
    public required UnbanRequest[] Data { get; init; }
}
