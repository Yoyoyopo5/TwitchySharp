using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;
using TwitchySharp.Helpers.JsonConverters;

namespace TwitchySharp.Api.Helix.HypeTrain;
/// <summary>
/// Contains a list of Hype Train events.
/// </summary>
public record GetHypeTrainEventsResponse
{
    /// <summary>
    /// The list of Hype Train events.
    /// The list is empty if the broadcaster hasn’t run a Hype Train within the last 5 days.
    /// </summary>
    public required HypeTrainEvent[] Data { get; init; }
    /// <summary>
    /// Contains the information used to page through the list of results. 
    /// The <see cref="Pagination.Cursor"/> is <see langword="null"/> if there are no more pages left to page through.
    /// </summary>
    public required Pagination Pagination { get; init; }
}

/// <summary>
/// Contains information about a specific Hype Train event.
/// </summary>
public record HypeTrainEvent
{
    /// <summary>
    /// The id of this event.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The type of event.
    /// For this response, the value is always <c>"hypetrain.progression"</c>.
    /// </summary>
    public required string EventType { get; init; }
    /// <summary>
    /// The date and time when the event occurred.
    /// </summary>
    public required DateTimeOffset EventTimestamp { get; init; }
    /// <summary>
    /// The version number of the definition of the event’s data. 
    /// For example, the value is <c>"1"</c> if the data in <see cref="EventData"/> uses the first definition of the event’s data. 
    /// </summary>
    public required string Version { get; init; }
    /// <summary>
    /// The event data.
    /// </summary>
    public required HypeTrainEventData EventData { get; init; }
}

/// <summary>
/// Contains information about a specific Hype Train progression event.
/// </summary>
public record HypeTrainEventData
{
    /// <summary>
    /// The user id of the broadcaster that has the Hype Train.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The date and time that another Hype Train can start.
    /// </summary>
    public required DateTimeOffset CooldownEndTime { get; init; }
    /// <summary>
    /// The date and time when this Hype Train will end.
    /// </summary>
    public required DateTimeOffset ExpiresAt { get; init; }
    /// <summary>
    /// The value needed to reach the next level.
    /// Each contribution has a <see cref="HypeTrainContribution.Total">value</see>.
    /// </summary>
    public required int Goal { get; init; }
    /// <summary>
    /// The id of the Hype Train.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The most recent contribution to the Hype Train.
    /// </summary>
    public required HypeTrainContribution LastContribution { get; init; }
    /// <summary>
    /// The highest level that the Hype Train has reached.
    /// Levels are from 1-5.
    /// </summary>
    public required int Level { get; init; }
    /// <summary>
    /// The date and time when the Hype Train started.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
    /// <summary>
    /// The top contributors to the Hype Train.
    /// One contribution is listed for each <see cref="HypeTrainContributionType"/>.
    /// For example, the top contributor using <see cref="HypeTrainContributionType.Bits"/> (by aggregate) and the top contributor using <see cref="HypeTrainContributionType.Subs"/> (by count).
    /// </summary>
    public required HypeTrainContribution[] TopContributions { get; init; }
    /// <summary>
    /// The current total amount raised.
    /// This value is aggregated from each <see cref="HypeTrainContribution.Total"/> to the Hype Train.
    /// </summary>
    public required int Total { get; init; }
}

/// <summary>
/// Contains data about a specific contribution to a Hype Train.
/// </summary>
public record HypeTrainContribution
{
    /// <summary>
    /// The total amount contributed.
    /// The exact meaning of this number depends on the value of <see cref="Type"/>:
    /// <list type="table">
    /// <item>
    ///     <term><see cref="HypeTrainContributionType.Bits"/></term>
    ///     <description>The number of bits used.</description>
    /// </item>
    /// <item>
    ///     <term><see cref="HypeTrainContributionType.Subs"/></term>
    ///     <description>Values of 500, 1000, or 2500 represent tier 1, 2, or 3 subscriptions, respectively.</description>
    /// </item>
    /// </list>
    /// </summary>
    public required int Total { get; init; }
    /// <summary>
    /// The contribution method used.
    /// </summary>
    [JsonConverter(typeof(SnakeCaseUpperJsonStringEnumConverter<HypeTrainContributionType>))]
    public required HypeTrainContributionType Type { get; init; }
    /// <summary>
    /// The user id of the user who made the contribution.
    /// </summary>
    public required string User { get; init; }
}

/// <summary>
/// Possible Hype Train contribution types.
/// </summary>
public enum HypeTrainContributionType
{
    /// <summary>
    /// Contributed by cheering with bits.
    /// </summary>
    Bits,
    /// <summary>
    /// Contributed by subscribing or gifting subscriptions.
    /// </summary>
    Subs,
    /// <summary>
    /// Contributed in any other way.
    /// </summary>
    Other
}
