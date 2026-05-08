
namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// Contains information about a message that was sent through the API.
/// </summary>
public record SentMessage
{
    /// <summary>
    /// The message id of the message that was sent.
    /// </summary>
    public required MessageId MessageId { get; init; }
    /// <summary>
    /// If the message passed all checks and was sent.
    /// </summary>
    public required bool IsSent { get; init; }
    /// <summary>
    /// The reason the message was dropped, if any.
    /// </summary>
    public MessageDropReason? DropReason { get; init; }
}
