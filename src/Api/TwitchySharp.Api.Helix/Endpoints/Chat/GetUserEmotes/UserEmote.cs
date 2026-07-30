
namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// Contains information about a emote that a user has access to.
/// </summary>
public record UserEmote
{
    /// <summary>
    /// An id that uniquely identifies this emote.
    /// </summary>
    public required EmoteId Id { get; init; }
    /// <summary>
    /// The name of the emote. 
    /// This is the name that viewers type in the chat window to get the emote to appear.
    /// </summary>
    public required EmoteName Name { get; init; }
    /// <summary>
    /// The type of emote.
    /// </summary>
    public required EmoteType EmoteType { get; init; }
    /// <summary>
    /// An id that identifies the emote set that the emote belongs to.
    /// </summary>
    public required EmoteSetId EmoteSetId { get; init; }
    /// <summary>
    /// The user id of the broadcaster who owns this emote.
    /// </summary>
    public required UserId OwnerId { get; init; } // may be nullable?
    /// <summary>
    /// The formats that the emote is available in. 
    /// For example, if the emote is available only as a static PNG, the array contains only <see cref="EmoteFormat.Static"/>. 
    /// But if the emote is available as a static PNG and an animated GIF, the array contains <see cref="EmoteFormat.Static"/> and <see cref="EmoteFormat.Animated"/>.
    /// </summary>
    public required EmoteFormat[] Format { get; init; }
    /// <summary>
    /// The sizes that the emote is available in. 
    /// For example, if the emote is available in small and medium sizes, the array contains <see cref="EmoteScale.Small"/> and <see cref="EmoteScale.Medium"/>.
    /// </summary>
    public required EmoteScale[] Scale { get; init; }
    /// <summary>
    /// The background themes that the emote is available in.
    /// </summary>
    public required EmoteTheme[] ThemeMode { get; init; }
}
