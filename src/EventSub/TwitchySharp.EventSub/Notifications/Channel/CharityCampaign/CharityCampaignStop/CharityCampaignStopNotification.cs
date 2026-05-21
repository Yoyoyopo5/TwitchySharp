namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.CharityCampaignStop"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelcharity_campaignstop">Charity Campaign Stop</see> for more information.
/// </remarks>
public record CharityCampaignStopNotification : EventSubNotification<CharityCampaignStopEvent, CharityCampaignStopCondition>;
