using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Bits;

/// <summary>
/// Contains information about a specific tier of a cheermote.
/// </summary>
public record CheermoteTier
{
    /// <summary>
    /// The minimum number of Bits that you must cheer at this tier level. 
    /// The maximum number of Bits that you can cheer at this level is determined by the required minimum Bits of the next tier level minus 1. 
    /// For example, if <see cref="MinBits"/> is 1 and <see cref="MinBits"/> for the next tier is 100, the Bits range for this tier level is 1 through 99. 
    /// The minimum Bits value of the last tier is the maximum number of Bits you can cheer using this Cheermote. For example, 10000.
    /// </summary>
    public required int MinBits { get; init; }
    /// <summary>
    /// The tier level. Possible tiers are:
    /// 1, 100, 500, 1000, 5000, 10000, 100000
    /// </summary>
    public required CheermoteTierLevel Id { get; init; }
    /// <summary>
    /// The hex code of the color associated with this tier level (for example, #979797).
    /// </summary>
    public required RgbColor Color { get; init; }
    /// <summary>
    /// The animated and static image sets for the Cheermote. 
    /// The dictionary of images is organized by theme, format, and size. 
    /// The theme keys are dark and light. 
    /// Each theme is a dictionary of formats: animated and static. 
    /// Each format is a dictionary of sizes: 1, 1.5, 2, 3, and 4. 
    /// The value of each size contains the URL to the image.
    /// </summary>
    public required CheermoteImageSet Images { get; init; }
    /// <summary>
    /// A boolean value that determines whether users can cheer at this tier level.
    /// </summary>
    public required bool CanCheer { get; init; }
    /// <summary>
    /// A boolean value that determines whether this tier level is shown in the Bits card. 
    /// Is <see langword="true"/> if this tier level is shown in the Bits card.
    /// </summary>
    public required bool ShowInBitsCard { get; init; }
}
