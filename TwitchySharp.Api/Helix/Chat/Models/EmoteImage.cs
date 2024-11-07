namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// Contains URLs for an emote's images.
/// Note that these are always static emote images with a light background theme.
/// </summary>
public record EmoteImage
{
    /// <summary>
    /// A URL to the small version (28px x 28px) of the emote.
    /// </summary>
    public required string Url_1x { get; init; }
    /// <summary>
    /// A URL to the medium version (56px x 56px) of the emote.
    /// </summary>
    public required string Url_2x { get; init; }
    /// <summary>
    /// A URL to the large version (112px x 112px) of the emote.
    /// </summary>
    public required string Url_4x { get; init; }
}