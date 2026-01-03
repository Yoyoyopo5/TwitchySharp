using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.EventSub.Models;

/// <summary>
/// The base class for charity campaign events.
/// </summary>
/// <remarks>
/// <see cref="EventSubSubscriptionType.CharityDonation"/>,
/// <see cref="EventSubSubscriptionType.CharityCampaignStart"/>,
/// <see cref="EventSubSubscriptionType.CharityCampaignProgress"/>,
/// <see cref="EventSubSubscriptionType.CharityCampaignStop"/>.
/// </remarks>
public record CharityCampaignEvent
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
    /// <summary>
    /// The name of the charity.
    /// </summary>
    public required string CharityName { get; init; }
    /// <summary>
    /// The description of the charity.
    /// </summary>
    public required string CharityDescription { get; init; }
    /// <summary>
    /// A URL pointing to a 100x100 PNG image of the charity's logo.
    /// </summary>
    public required string CharityLogo { get; init; }
    /// <summary>
    /// The URL of the charity's website.
    /// </summary>
    public required string CharityWebsite { get; init; }
}

/// <summary>
/// The base class for charity campaign lifetime events.
/// </summary>
/// <remarks>
/// <see cref="EventSubSubscriptionType.CharityCampaignStart"/>,
/// <see cref="EventSubSubscriptionType.CharityCampaignProgress"/>,
/// <see cref="EventSubSubscriptionType.CharityCampaignStop"/>.
/// </remarks>
public record CharityCampaignLifecycleEvent : CharityCampaignEvent
{
    public required string Id { get; init; }
    public required CharityAmount CurrentAmount { get; init; }
    public required CharityAmount TargetAmount { get; init; }
}
