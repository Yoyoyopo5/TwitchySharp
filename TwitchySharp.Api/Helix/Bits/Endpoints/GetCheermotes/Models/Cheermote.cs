using System;

namespace TwitchySharp.Api.Helix.Bits;

/// <summary>
/// Contains information about a specific cheermote.
/// </summary>
public record Cheermote
{
    /// <summary>
    /// The name portion of the Cheermote string that you use in chat to cheer Bits. 
    /// The full Cheermote string is the concatenation of {prefix} + {number of Bits}. 
    /// For example, if the prefix is “Cheer” and you want to cheer 100 Bits, the full Cheermote string is Cheer100. 
    /// When the Cheermote string is entered in chat, Twitch converts it to the image associated with the Bits tier that was cheered.
    /// </summary>
    public required string Prefix { get; init; }
    /// <summary>
    /// A list of tier levels that the Cheermote supports. 
    /// Each tier identifies the range of Bits that you can cheer at that tier level and an image that graphically identifies the tier level.
    /// </summary>
    public required CheermoteTier[] Tiers { get; init; }
    /// <summary>
    /// The type of Cheermote.
    /// </summary>
    public required CheermoteType Type { get; init; }
    /// <summary>
    /// The order that the Cheermotes are shown in the Bits card. 
    /// The numbers may not be consecutive. For example, the numbers may jump from 1 to 7 to 13. 
    /// The order numbers are unique within a Cheermote type but may not be unique amongst all Cheermotes in the response.
    /// </summary>
    public required int Order { get; init; }
    /// <summary>
    /// The date and time when this Cheermote was last updated.
    /// </summary>
    public required DateTimeOffset LastUpdated { get; init; }
    /// <summary>
    /// A boolean value that indicates whether this Cheermote provides a charitable contribution match during charity campaigns.
    /// </summary>
    public required bool IsCharitable { get; init; }
}
