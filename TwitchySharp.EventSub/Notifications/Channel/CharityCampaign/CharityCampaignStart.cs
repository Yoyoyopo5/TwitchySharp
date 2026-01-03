using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel.CharityCampaign;
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
public record CharityCampaignStartEvent : CharityCampaignLifecycleEvent
{
    /// <summary>
    /// The date and time the charity campaign began.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
}
