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
/// <inheritdoc cref="EventSubSubscriptionType.CharityCampaignProgress"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelcharity_campaignprogress">Charity Campaign Progress</see> for more information.
/// </remarks>
public record CharityCampaignProgressNotification : EventSubNotification<CharityCampaignProgressEvent, CharityCampaignProgressCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.CharityCampaignProgress"/>.
/// </summary>
public record CharityCampaignProgressCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.CharityCampaignProgress"/> event.
/// </summary>
public record CharityCampaignProgressEvent : CharityCampaignLifecycleEvent;
