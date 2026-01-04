using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Conditions;

namespace TwitchySharp.EventSub.Notifications.User.Authorization;
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
public record UserAuthorizationGrantEvent : UserAuthorizationEvent
{
    /// <summary>
    /// <inheritdoc cref="UserAuthorizationEvent.UserLogin"/>
    /// </summary>
    public new required string UserLogin { get; init; }
    /// <summary>
    /// <inheritdoc cref="UserAuthorizationEvent.UserName"/>
    /// </summary>
    public new required string UserName { get; init; }
}
