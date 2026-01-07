using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Interfaces.Events;

namespace TwitchySharp.EventSub.Notifications.User;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.UserUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#userupdate">User Update</see> for more information.
/// </remarks>
public record UserUpdateNotification : EventSubNotification<UserUpdateEvent, UserUpdateCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.UserUpdate"/>.
/// </summary>
public record UserUpdateCondition : UserCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.UserUpdate"/> event.
/// </summary>
public record UserUpdateEvent : IHaveUser
{
    /// <summary>
    /// The id of the user.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The email address of the user.
    /// This is <see cref="string.Empty"/> unless the app that created the subscription includes 
    /// the <c>user:read:email</c> scope for this user.
    /// </summary>
    public required string Email { get; init; }
    /// <summary>
    /// Indicates whether the user has verified their email address.
    /// If <see cref="Email"/> is <see cref="string.Empty"/>, this should be ignored.
    /// </summary>
    public required bool EmailVerified { get; init; }
    /// <summary>
    /// The user's description.
    /// </summary>
    public required string Description { get; init; }
}
