namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.AutomodSettingsUpdate"/> event.
/// </summary>
public record AutomodSettingsUpdateEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) that the Automod settings were updated for.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the Automod settings were updated for.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the Automod settings were updated for.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the moderator that updated the Automod settings.
    /// </summary>
    public required UserId ModeratorUserId { get; init; }
    /// <summary>
    /// The login (username) of the moderator that updated the Automod settings.
    /// </summary>
    public required UserLogin ModeratorUserLogin { get; init; }
    /// <summary>
    /// The display name of the moderator that updated the Automod settings.
    /// </summary>
    public required UserName ModeratorUserName { get; init; }
    /// <summary>
    /// The overall Automod level for the channel.
    /// This is <see langword="null"/> if the channel has custom levels for specific categories.
    /// </summary>
    public AutomodFilteringLevel? OverallLevel { get; init; }
    /// <summary>
    /// The Automod level for hostility involving name calling or insults.
    /// </summary>
    public AutomodFilteringLevel Bullying { get; init; }
    /// <summary>
    /// The Automod level for discrimination against disability.
    /// </summary>
    public AutomodFilteringLevel Disability { get; init; }
    /// <summary>
    /// The Automod level for racial discrimination.
    /// </summary>
    public AutomodFilteringLevel RaceEthnicityOrReligion { get; init; }
    /// <summary>
    /// The Automod level for discrimination against women.
    /// </summary>
    public AutomodFilteringLevel Misogyny { get; init; }
    /// <summary>
    /// The AutoMod level for discrimination based on sexuality, sex, or gender.
    /// </summary>
    public AutomodFilteringLevel SexualitySexOrGender { get; init; }
    /// <summary>
    /// The Automod level for hostility involving aggression.
    /// </summary>
    public AutomodFilteringLevel Aggression { get; init; }
    /// <summary>
    /// The Automod level for sexual content.
    /// </summary>
    public AutomodFilteringLevel SexBasedTerms { get; init; }
    /// <summary>
    /// The Automod level for profanity.
    /// </summary>
    public AutomodFilteringLevel Swearing { get; init; }
}
