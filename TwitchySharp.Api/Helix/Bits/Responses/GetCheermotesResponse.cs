using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Bits;
/// <summary>
/// Contains a list of cheermote data.
/// </summary>
public record GetCheermotesResponse
{
    /// <summary>
    /// The list of Cheermotes. The list is in ascending order by the contained <see cref="CheermoteData.Order"/> property.
    /// </summary>
    public required CheermoteData[] Data { get; init; }
}

/// <summary>
/// Contains data about a specific cheermote.
/// </summary>
public record CheermoteData
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
    public required CheermoteTier Tiers { get; init; }
    /// <summary>
    /// The type of Cheermote.
    /// </summary>
    [JsonConverter(typeof(SnakeCaseLowerJsonStringEnumConverter<CheermoteType>))]
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

/// <summary>
/// Respresents possible values of Twitch cheermote types.
/// </summary>
public enum CheermoteType
{
    /// <summary>
    /// A Twitch-defined Cheermote that is shown in the Bits card.
    /// </summary>
    GlobalFirstParty,
    /// <summary>
    /// A Twitch-defined Cheermote that is not shown in the Bits card.
    /// </summary>
    GlobalThirdParty,
    /// <summary>
    /// A broadcaster-defined Cheermote.
    /// </summary>
    ChannelCustom,
    /// <summary>
    /// Do not use; for internal use only.
    /// </summary>
    DisplayOnly,
    /// <summary>
    /// A sponsor-defined Cheermote. 
    /// When used, the sponsor adds additional Bits to the amount that the user cheered. 
    /// For example, if the user cheered Terminator100, the broadcaster might receive 110 Bits, which includes the sponsor's 10 Bits contribution.
    /// </summary>
    Sponsored
}

/// <summary>
/// Contains data about a specific tier of a cheermote.
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
    public required string Id { get; init; }
    /// <summary>
    /// The hex code of the color associated with this tier level (for example, #979797).
    /// </summary>
    public required string Color { get; init; }
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
