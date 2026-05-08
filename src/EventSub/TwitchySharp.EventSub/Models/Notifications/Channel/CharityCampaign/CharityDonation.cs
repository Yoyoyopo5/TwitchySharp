using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Channel.CharityCampaign;
using TwitchySharp.EventSub.Models.Conditions;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.CharityCampaign;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.CharityDonation"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelcharity_campaigndonate">Charity Donation</see> for more information.
/// </remarks>
public record CharityDonationNotification : EventSubNotification<CharityDonationEvent, CharityDonationCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.CharityDonation"/>.
/// </summary>
public record CharityDonationCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.CharityDonation"/> event.
/// </summary>
public record CharityDonationEvent : IHaveBroadcaster, IHaveUser, IHaveCharity
{
    /// <summary>
    /// The id of the donation.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) who is hosting the charity campaign.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) who is hosting the charity campaign.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) who is hosting the charity campaign.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    public required string CharityName { get; init; }
    public required string CharityDescription { get; init; }
    public required string CharityLogo { get; init; }
    public required string CharityWebsite { get; init; }
    /// <summary>
    /// The id of the charity campaign the donation was for.
    /// </summary>
    public required string CampaignId { get; init; }
    /// <summary>
    /// The id of the user who made the donation.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user who made the donation.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user who made the donation.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The amount of money the user donated to the charity campaign.
    /// </summary>
    public required CharityAmount Amount { get; init; }
}
