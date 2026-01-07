using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Models.Notifications;

namespace TwitchySharp.EventSub.Models.Notifications.User.Authorization;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.UserAuthorizationGrant"/>
/// </summary>
/// <remarks>
/// See <see href="">User Authorization Grant</see> for more information.
/// </remarks>
public record UserAuthorizationGrantNotification : EventSubNotification<UserAuthorizationGrantEvent, UserAuthorizationGrantCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.UserAuthorizationGrant"/>.
/// </summary>
public record UserAuthorizationGrantCondition : ClientCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.UserAuthorizationGrant"/> event.
/// </summary>
public record UserAuthorizationGrantEvent : IHaveClient, IHaveUser
{
    /// <summary>
    /// The client id of the application the authorization is associated with.
    /// </summary>
    public required string ClientId { get; init; }
    /// <summary>
    /// The id of the user who granted access to the app.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user who granted access to the app.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user who granted access to the app.
    /// </summary>
    public required string UserName { get; init; }
}
