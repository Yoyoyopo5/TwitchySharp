namespace TwitchySharp.Api.Models.Helix.Bits.Models;

/// <summary>
/// Contains URIs to the images associated with a cheermote's image.
/// </summary>
public record CheermoteImageSet
{
    /// <summary>
    /// The dark theme of the cheermote.
    /// </summary>
    public required CheermoteImageTheme Dark { get; init; }
    /// <summary>
    /// The light theme of the cheermote.
    /// </summary>
    public required CheermoteImageTheme Light { get; init; }
}
