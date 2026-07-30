namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific Automod hold that was triggered by a blocked term.
/// </summary>
public record BlockedTermHold
{
    /// <summary>
    /// The list of blocked terms found in the message.
    /// </summary>
    public required BlockedTerm[] TermsFound { get; init; }
}
