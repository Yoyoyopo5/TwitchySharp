using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains a list of AutoMod settings.
/// </summary>
public record GetAutoModSettingsResponse
{
    /// <summary>
    /// Contains a single entry of a channel's current AutoMod settings.
    /// </summary>
    public required AutoModSettings[] Data { get; init; }
}

/// <summary>
/// Contains information about a channel's AutoMod settings.
/// </summary>
public record AutoModSettings
{
    /// <summary>
    /// The user id of the broadcaster (channel) that the settings belong to.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The user id of the moderator used in the request to get the settings.
    /// </summary>
    public required string ModeratorId { get; init; }
    /// <summary>
    /// The default AutoMod level for the broadcaster. Ranges from 0 (no filtering) to 4 (maximum filtering).
    /// This field is <see langword="null"/> if the broadcaster has set one or more of the individual settings. 
    /// </summary>
    public int? OverallLevel { get; init; }
    /// <summary>
    /// The Automod level for discrimination against disability. 
    /// Ranges from 0 (no filtering) to 4 (maximum filtering).
    /// </summary>
    public required int Disability { get; init; }
    /// <summary>
    /// The Automod level for hostility involving aggression.
    /// Ranges from 0 (no filtering) to 4 (maximum filtering).
    /// </summary>
    public required int Aggression { get; init; }
    /// <summary>
    /// The AutoMod level for discrimination based on sexuality, sex, or gender.
    /// Ranges from 0 (no filtering) to 4 (maximum filtering).
    /// </summary>
    public required int SexualitySexOrGender { get; init; }
    /// <summary>
    /// The Automod level for discrimination against women.
    /// Ranges from 0 (no filtering) to 4 (maximum filtering).
    /// </summary>
    public required int Misogyny { get; init; }
    /// <summary>
    /// The Automod level for hostility involving name calling or insults.
    /// Ranges from 0 (no filtering) to 4 (maximum filtering).
    /// </summary>
    public required int Bullying { get; init; }
    /// <summary>
    /// The Automod level for profanity.
    /// Ranges from 0 (no filtering) to 4 (maximum filtering).
    /// </summary>
    public required int Swearing { get; init; }
    /// <summary>
    /// The Automod level for racial discrimination.
    /// Ranges from 0 (no filtering) to 4 (maximum filtering).
    /// </summary>
    public required int RaceEthnicityOrReligion { get; init; }
    /// <summary>
    /// The Automod level for sexual content.
    /// Ranges from 0 (no filtering) to 4 (maximum filtering).
    /// </summary>
    public required int SexBasedTerms { get; init; }
}