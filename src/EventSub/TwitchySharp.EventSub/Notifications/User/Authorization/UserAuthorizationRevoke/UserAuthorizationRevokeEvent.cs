namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.UserAuthorizationRevoke"/> event.
/// </summary>
public record UserAuthorizationRevokeEvent
{
    /// <summary>
    /// The client id of the application the authorization is associated with.
    /// </summary>
    public required ClientId ClientId { get; init; }
    /// <summary>
    /// The id of the user whose authorization status was revoked.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user whose authorization status was revoked.
    /// </summary>
    /// <remarks>
    /// This is <see langword="null"/> if the user no longer exists.
    /// </remarks>
    public UserLogin? UserLogin { get; init; }
    /// <summary>
    /// The display name of the user whose authorization status was revoked.
    /// </summary>
    /// <remarks>
    /// This is <see langword="null"/> if the user no longer exists.
    /// </remarks>
    public UserName? UserName { get; init; }
}
