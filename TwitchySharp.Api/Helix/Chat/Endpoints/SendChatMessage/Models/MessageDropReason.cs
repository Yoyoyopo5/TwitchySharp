namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// Contains information about why a chat message was not sent successfully.
/// </summary>
public record MessageDropReason
{
    /// <summary>
    /// Code for why the message was dropped.
    /// </summary>
    public required string Code { get; init; }
    /// <summary>
    /// Message for why the message was dropped.
    /// </summary>
    public required string Message { get; init; }
}
