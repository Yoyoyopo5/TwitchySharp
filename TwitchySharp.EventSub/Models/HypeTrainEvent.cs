using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models;

/// <summary>
/// The base class for Hype Train events.
/// </summary>
/// <remarks>
/// <see cref="EventSubSubscriptionType.HypeTrainBeginV2"/>,
/// <see cref="EventSubSubscriptionType.HypeTrainProgressV2"/>,
/// <see cref="EventSubSubscriptionType.HypeTrainEndV2"/>.
/// </remarks>
public record HypeTrainEvent
{
    /// <summary>
    /// The id of the Hype Train.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) hosting the Hype Train.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) hosting the Hype Train.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) hosting the Hype Train.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The total number of points contributed to the Hype Train.
    /// </summary>
    public required int Total { get; init; }
    /// <summary>
    /// The current level of the Hype Train.
    /// </summary>
    public required int Level { get; init; }
    /// <summary>
    /// The list of broadcasters participating in the Hype Train, if it occurred in a shared chat.
    /// This is <see langword="null"/> if the Hype Train is not in a shared chat.
    /// </summary>
    public SharedHypeTrainParticipant[]? SharedTrainParticipants { get; init; }
    /// <summary>
    /// The date and time when the Hype Train began.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
    /// <summary>
    /// The type of Hype Train.
    /// </summary>
    public required HypeTrainType Type { get; init; }
    /// <summary>
    /// Indicates whether the Hype Train is in a shared chat.
    /// </summary>
    public required bool IsSharedTrain { get; init; }
}

/// <summary>
/// The base class for active Hype Train events.
/// </summary>
/// <remarks>
/// <see cref="EventSubSubscriptionType.HypeTrainBeginV2"/>,
/// <see cref="EventSubSubscriptionType.HypeTrainProgressV2"/>.
/// </remarks>
public record HypeTrainActiveEvent : HypeTrainEvent
{
    /// <summary>
    /// The number of points contributed to the Hype Train at its current level.
    /// </summary>
    public required int Progress { get; init; }
    /// <summary>
    /// The number of point required to reach the next level.
    /// </summary>
    public required int Goal { get; init; }
    /// <summary>
    /// The date and time when the Hype Train will expire.
    /// This is extended when the Hype Train reaches a new level.
    /// </summary>
    public DateTimeOffset ExpiresAt { get; init; }
}

/// <summary>
/// Contains static definitions for possible Hype Train types.
/// </summary>
/// <param name="Value">The string value of the Hype Train type.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<HypeTrainType, string>))]
public record HypeTrainType(string Value) : ValueBackedEnum<string>(Value)
{
    public static HypeTrainType Treasure { get; } = new("treasure");
    public static HypeTrainType GoldenKappa { get; } = new("golden_kappa");
    public static HypeTrainType Regular { get; } = new("regular");
}

/// <summary>
/// Contains information about a specific Hype Train contributor.
/// </summary>
public record HypeTrainTopContributor
{
    /// <summary>
    /// The user id of the contributor.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the contributor.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the contributor.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The contribution type used.
    /// </summary>
    public required HypeTrainContributionType Type { get; init; }
    /// <summary>
    /// The total amount contributed by this user to the Hype Train.
    /// </summary>
    /// <remarks>
    /// If <see cref="Type"/> is <see cref="HypeTrainContributionType.Bits"/>, total represents the amount of Bits used. 
    /// If <see cref="Type"/> is <see cref="HypeTrainContributionType.Subscription"/>, total is 500, 1000, or 2500 to represent tier 1, 2, or 3 subscriptions, respectively.
    /// </remarks>
    public required int Total { get; init; }
}

/// <summary>
/// Contains static definitions for possible Hype Train contribution types.
/// </summary>
/// <param name="Value">The string value of the contribution type.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<HypeTrainContributionType, string>))]
public record HypeTrainContributionType(string Value) : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// Bits contributions with Cheering, Power-ups, and Extensions. 
    /// </summary>
    public static HypeTrainContributionType Bits { get; } = new("bits");
    /// <summary>
    /// Subscription activity like subscribing or gifting subscriptions. 
    /// </summary>
    public static HypeTrainContributionType Subscription { get; } = new("subscription");
    /// <summary>
    /// Covers other contribution methods not listed.
    /// </summary>
    public static HypeTrainContributionType Other { get; } = new("other");
}

/// <summary>
/// Contains information about a specific broadcaster (channel) participating in a Hype Train in a shared chat.
/// </summary>
public record SharedHypeTrainParticipant
{
    /// <summary>
    /// The user id of the broadcaster participating in the shared chat Hype Train.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster participating in the shared chat Hype Train.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster participating in the shared chat Hype Train.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
}
