namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.CharityCampaignStart"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelcharity_campaignstart">Charity Campaign Start</see> for more information.
/// </remarks>
public record CharityCampaignStartNotification : EventSubNotification<CharityCampaignStartEvent, CharityCampaignStartCondition>;
