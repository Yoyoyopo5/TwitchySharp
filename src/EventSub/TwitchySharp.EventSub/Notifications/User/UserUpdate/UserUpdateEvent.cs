namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.UserUpdate"/> event.
/// </summary>
public record UserUpdateEvent
{
    /// <summary>
    /// The id of the user.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The email address of the user.
    /// This is <see cref="string.Empty"/> unless the app that created the subscription includes 
    /// the <c>user:read:email</c> scope for this user.
    /// </summary>
    public required UserEmail Email { get; init; }
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
