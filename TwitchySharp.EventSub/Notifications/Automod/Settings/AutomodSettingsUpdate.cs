using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.Enums;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Automod;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.AutomodSettingsUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#automodsettingsupdate">Automod Settings Update</see> for more information.
/// </remarks>
public record AutomodSettingsUpdateNotification : EventSubNotification<AutomodSettingsUpdateEvent, AutomodSettingsUpdateCondition>;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.AutomodSettingsUpdate"/>
/// </summary>
public record AutomodSettingsUpdateCondition
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get Automod Settings Update notifications for.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's chat to get Automod Settings Update notifications on behalf of.
    /// </summary>
    public required string ModeratorUserId { get; init; }
}

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.AutomodSettingsUpdate"/> event.
/// </summary>
public record AutomodSettingsUpdateEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) that the Automod settings were updated for.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the Automod settings were updated for.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the Automod settings were updated for.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the moderator that updated the Automod settings.
    /// </summary>
    public required string ModeratorUserId { get; init; }
    /// <summary>
    /// The login (username) of the moderator that updated the Automod settings.
    /// </summary>
    public required string ModeratorUserLogin { get; init; }
    /// <summary>
    /// The display name of the moderator that updated the Automod settings.
    /// </summary>
    public required string ModeratorUserName { get; init; }
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
