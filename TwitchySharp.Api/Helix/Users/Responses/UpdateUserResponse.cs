using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Contains information about an updated user.
/// </summary>
public record UpdateUserResponse
{
    /// <summary>
    /// A list containing the single user that was updated.
    /// </summary>
    public required TwitchUser[] Data { get; init; }
}
