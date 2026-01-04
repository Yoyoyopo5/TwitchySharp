using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models;

/// <summary>
/// The base class for user authorization events.
/// </summary>
/// <remarks>
/// <see cref="EventSubSubscriptionType.UserAuthorizationGrant"/>,
/// <see cref="EventSubSubscriptionType.UserAuthorizationRevoke"/>.
/// </remarks>
public record UserAuthorizationEvent
{
    /// <summary>
    /// The client id of the application the authorization is associated with.
    /// </summary>
    public required string ClientId { get; init; }
    /// <summary>
    /// The id of the user whose authorization status changed.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user whose authorization status changed.
    /// </summary>
    /// <remarks>
    /// This is <see langword="null"/> if the user no longer exists.
    /// </remarks>
    public string? UserLogin { get; init; }
    /// <summary>
    /// The display name of the user whose authorization status changed.
    /// </summary>
    /// <remarks>
    /// This is <see langword="null"/> if the user no longer exists.
    /// </remarks>
    public string? UserName { get; init; }
}
