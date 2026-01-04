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
/// <inheritdoc cref="EventSubSubscriptionType.UserAuthorizationRevoke"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#userauthorizationrevoke">User Authorization Revoke</see> for more information.
/// </remarks>
public record UserAuthorizationRevokeNotification : EventSubNotification<UserAuthorizationRevokeEvent, UserAuthorizationRevokeCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.UserAuthorizationRevoke"/>.
/// </summary>
public record UserAuthorizationRevokeCondition : ClientCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.UserAuthorizationRevoke"/> event.
/// </summary>
public record UserAuthorizationRevokeEvent : UserAuthorizationEvent;
