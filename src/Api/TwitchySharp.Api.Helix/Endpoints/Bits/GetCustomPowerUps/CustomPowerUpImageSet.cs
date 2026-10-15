namespace TwitchySharp.Api.Helix.Bits;

/// <summary>
/// A set of images for a specific custom Bits power-up.
/// </summary>
public record CustomPowerUpImageSet
{
    /// <summary>
    /// The URL to a small version of the image.
    /// </summary>
    public required ImageUrl Url1x { get; init; }
    /// <summary>
    /// The URL to a medium version of the image.
    /// </summary>
    public required ImageUrl Url2x { get; init; }
    /// <summary>
    /// The URL to a large version of the image.
    /// </summary>
    public required ImageUrl Url4x { get; init; }
}
