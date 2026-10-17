namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// An emote used in a pinned message.
/// </summary>
public record PinnedMessageEmote
{
    /// <summary>
    /// The id of the emote.
    /// </summary>
    public required EmoteId Id { get; init; }
    /// <summary>
    /// The id of the set the emote belongs to.
    /// </summary>
    public required EmoteSetId EmoteSetId { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) who owns the emote.
    /// </summary>
    public required UserId OwnerId { get; init; }
    /// <summary>
    /// The emote formats available.
    /// </summary>
    public required EmoteFormat[] Format { get; init; }
}
