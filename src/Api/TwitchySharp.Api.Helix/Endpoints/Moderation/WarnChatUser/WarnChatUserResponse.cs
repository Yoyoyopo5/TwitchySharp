namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains information about a warning issued to a user in chat.
/// </summary>
public record WarnChatUserResponse
{
    /// <summary>
    /// A list containing a single object describing the warning that was issued.
    /// </summary>
    public required IssuedChatUserWarning[] Data { get; init; }
}
