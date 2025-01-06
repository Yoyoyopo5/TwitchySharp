using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Contains the list of users that the broadcaster has blocked.
/// </summary>
public record GetUserBlockListResponse
{
    /// <summary>
    /// The list of blocked users, in decending order by when the user was blocked.
    /// </summary>
    public required BlockedUser[] Data { get; init; }
}

/// <summary>
/// Contains information about a specific blocked user.
/// </summary>
public record BlockedUser
{
    /// <summary>
    /// The user id of the blocked user.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the blocked user.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the blocked user.
    /// </summary>
    public required string DisplayName { get; init; }
}
