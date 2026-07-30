namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.UserAuthorizationGrant"/> event.
/// </summary>
public record UserAuthorizationGrantEvent
{
    /// <summary>
    /// The client id of the application the authorization is associated with.
    /// </summary>
    public required ClientId ClientId { get; init; }
    /// <summary>
    /// The id of the user who granted access to the app.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user who granted access to the app.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user who granted access to the app.
    /// </summary>
    public required UserName UserName { get; init; }
}
