namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.CharityCampaignProgress"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelcharity_campaignprogress">Charity Campaign Progress</see> for more information.
/// </remarks>
public record CharityCampaignProgressNotification : EventSubNotification<CharityCampaignProgressEvent, CharityCampaignProgressCondition>;
