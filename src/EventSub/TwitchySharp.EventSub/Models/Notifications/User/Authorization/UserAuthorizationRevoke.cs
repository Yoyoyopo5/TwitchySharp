using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models.Notifications.User.Authorization;
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
public record UserAuthorizationRevokeEvent : IHaveClient
{
    /// <summary>
    /// The client id of the application the authorization is associated with.
    /// </summary>
    public required string ClientId { get; init; }
    /// <summary>
    /// The id of the user whose authorization status was revoked.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user whose authorization status was revoked.
    /// </summary>
    /// <remarks>
    /// This is <see langword="null"/> if the user no longer exists.
    /// </remarks>
    public string? UserLogin { get; init; }
    /// <summary>
    /// The display name of the user whose authorization status was revoked.
    /// </summary>
    /// <remarks>
    /// This is <see langword="null"/> if the user no longer exists.
    /// </remarks>
    public string? UserName { get; init; }
}
