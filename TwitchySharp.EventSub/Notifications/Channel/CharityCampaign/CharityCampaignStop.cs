using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel.CharityCampaign;
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
public record CharityCampaignStopEvent : CharityCampaignLifecycleEvent
{
    /// <summary>
    /// The date and time when the charity campaign was stopped by the broadcaster.
    /// </summary>
    public required DateTimeOffset StoppedAt { get; init; }
}
