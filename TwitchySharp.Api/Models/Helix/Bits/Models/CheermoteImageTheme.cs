using System.Collections.Generic;

namespace TwitchySharp.Api.Models.Helix.Bits.Models;

/// <summary>
/// Contains URIs to images associated with a cheermote's image and theme.
/// </summary>
public record CheermoteImageTheme
{
    /// <summary>
    /// The animated format of the cheermote. The keys represent sizes (1, 1.5, 2, 3, 4), and the values are URIs to the image data.
    /// </summary>
    public required Dictionary<string, string> Animated { get; init; }
    /// <summary>
    /// The static (non-animated) format of the cheermote. The keys represent sizes (1, 1.5, 2, 3, 4), and the values are URIs to the image data.
    /// </summary>
    public required Dictionary<string, string> Static { get; init; }
}
