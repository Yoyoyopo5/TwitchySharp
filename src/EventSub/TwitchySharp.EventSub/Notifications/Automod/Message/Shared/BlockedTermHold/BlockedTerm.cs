namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific blocked term found in a message held by Automod.
/// </summary>
public record BlockedTerm
{
    /// <summary>
    /// The id of the blocked term.
    /// </summary>
    public required AutomodBlockedTermId TermId { get; init; }
    /// <summary>
    /// The bounds of the blocked term that caused the message to be caught.
    /// </summary>
    public required AutomodHoldBoundary Boundary { get; init; }
    /// <summary>
    /// The user id of the broadcaster that owns the blocked term.
    /// </summary>
    public required UserId OwnerBroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster that owns the blocked term.
    /// </summary>
    public required UserLogin OwnerBroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster that owns the blocked term.
    /// </summary>
    public required UserName OwnerBroadcasterUserName { get; init; }
}
