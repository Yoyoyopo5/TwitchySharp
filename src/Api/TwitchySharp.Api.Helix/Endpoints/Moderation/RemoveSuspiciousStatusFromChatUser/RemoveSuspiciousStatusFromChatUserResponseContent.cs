namespace TwitchySharp.Api.Helix.Moderation;

/// <summary>
/// Contains an array containing the updated suspicious user.
/// </summary>
public record RemoveSuspiciousStatusFromChatUserResponseContent
{
    /// <summary>
    /// An array containing a single updated suspicious user.
    /// </summary>
    public required SuspiciousUser[] Data { get; init; }
}
