namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific resubscription chat message.
/// </summary>
public record ResubscriptionMessage
{
    /// <summary>
    /// The full text of the message.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// The emotes present in the message.
    /// </summary>
    public required ResubscriptionMessageEmote[] Emotes { get; init; }
}
