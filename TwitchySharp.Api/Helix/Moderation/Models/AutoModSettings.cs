using TwitchySharp.Shared.Enums;

namespace TwitchySharp.Api.Helix.Moderation;

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
    public AutomodFilteringLevel? OverallLevel { get; init; }
    /// <summary>
    /// The Automod level for discrimination against disability. 
    /// </summary>
    public required AutomodFilteringLevel Disability { get; init; }
    /// <summary>
    /// The Automod level for hostility involving aggression.
    /// </summary>
    public required AutomodFilteringLevel Aggression { get; init; }
    /// <summary>
    /// The AutoMod level for discrimination based on sexuality, sex, or gender.
    /// </summary>
    public required AutomodFilteringLevel SexualitySexOrGender { get; init; }
    /// <summary>
    /// The Automod level for discrimination against women.
    /// </summary>
    public required AutomodFilteringLevel Misogyny { get; init; }
    /// <summary>
    /// The Automod level for hostility involving name calling or insults.
    /// </summary>
    public required AutomodFilteringLevel Bullying { get; init; }
    /// <summary>
    /// The Automod level for profanity.
    /// </summary>
    public required AutomodFilteringLevel Swearing { get; init; }
    /// <summary>
    /// The Automod level for racial discrimination.
    /// </summary>
    public required AutomodFilteringLevel RaceEthnicityOrReligion { get; init; }
    /// <summary>
    /// The Automod level for sexual content.
    /// </summary>
    public required AutomodFilteringLevel SexBasedTerms { get; init; }
}