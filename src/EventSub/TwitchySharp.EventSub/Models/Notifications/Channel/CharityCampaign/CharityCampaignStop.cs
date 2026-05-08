using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Channel.CharityCampaign;
using TwitchySharp.EventSub.Models.Conditions;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.CharityCampaign;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.CharityCampaignStop"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelcharity_campaignstop">Charity Campaign Stop</see> for more information.
/// </remarks>
public record CharityCampaignStopNotification : EventSubNotification<CharityCampaignStopEvent, CharityCampaignStopCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.CharityCampaignStop"/>.
/// </summary>
public record CharityCampaignStopCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.CharityCampaignStop"/> event.
/// </summary>
public record CharityCampaignStopEvent : IHaveCharityCampaign, IHaveCharity, IHaveBroadcaster
{
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
    public required string Id { get; init; }
    public required CharityAmount CurrentAmount { get; init; }
    public required CharityAmount TargetAmount { get; init; }
    /// <summary>
    /// The date and time when the charity campaign was stopped by the broadcaster.
    /// </summary>
    public required DateTimeOffset StoppedAt { get; init; }
}
