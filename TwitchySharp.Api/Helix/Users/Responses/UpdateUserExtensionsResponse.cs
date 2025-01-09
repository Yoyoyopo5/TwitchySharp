using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Contains information about the extensions that were updated.
/// </summary>
public record UpdateUserExtensionsResponse
{
    /// <summary>
    /// The extensions that were updated.
    /// </summary>
    public required UserActiveExtensions Data { get; init; }
}
