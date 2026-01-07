using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Channel.CharityCampaign;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.CharityCampaign;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.CharityCampaignStart"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelcharity_campaignstart">Charity Campaign Start</see> for more information.
/// </remarks>
public record CharityCampaignStartNotification : EventSubNotification<CharityCampaignStartEvent, CharityCampaignStartCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.CharityCampaignStart"/>.
/// </summary>
public record CharityCampaignStartCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.CharityCampaignStart"/> event.
/// </summary>
public record CharityCampaignStartEvent : IHaveCharityCampaign, IHaveCharity, IHaveBroadcaster
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
    /// The date and time the charity campaign began.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
}
