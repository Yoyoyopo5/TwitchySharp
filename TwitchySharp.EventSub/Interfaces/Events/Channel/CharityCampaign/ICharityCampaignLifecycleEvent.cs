using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Interfaces.Events.Channel.CharityCampaign;

/// <summary>
/// The interface for charity campaign lifecycle events.
/// </summary>
/// <remarks>
/// <see cref="EventSubSubscriptionType.CharityCampaignStart"/>,
/// <see cref="EventSubSubscriptionType.CharityCampaignProgress"/>,
/// <see cref="EventSubSubscriptionType.CharityCampaignStop"/>.
/// </remarks>
public interface ICharityCampaignLifecycleEvent : IHaveCharityCampaign, IHaveCharity, IHaveBroadcaster;
