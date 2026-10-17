namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// A mention in a pinned message.
/// </summary>
public record PinnedMessageMention
{
    /// <summary>
    /// The id of the mentioned user.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login of the mentioned user.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the mentioned user.
    /// </summary>
    public required UserName UserName { get; init; }
}
