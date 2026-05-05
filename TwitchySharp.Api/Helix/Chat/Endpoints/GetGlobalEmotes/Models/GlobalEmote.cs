using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// Contains information about a global emote.
/// </summary>
public record GlobalEmote
{
    /// <summary>
    /// An ID that identifies this emote.
    /// </summary>
    public required EmoteId Id { get; init; }
    /// <summary>
    /// The name of the emote. 
    /// This is the name that viewers type in the chat window to get the emote to appear.
    /// </summary>
    public required string Name { get; init; }
    /// <summary>
    /// The image URLs for the emote. 
    /// These image URLs always provide a static, non-animated emote image with a light background.
    /// <b>NOTE:</b> You should use the <see cref="EmoteImageTemplateString"/> in the Template property to fetch the image instead of using these URLs.
    /// </summary>
    public required EmoteImage Images { get; init; }
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
