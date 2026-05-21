namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.CharityDonation"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelcharity_campaigndonate">Charity Donation</see> for more information.
/// </remarks>
public record CharityDonationNotification : EventSubNotification<CharityDonationEvent, CharityDonationCondition>;
