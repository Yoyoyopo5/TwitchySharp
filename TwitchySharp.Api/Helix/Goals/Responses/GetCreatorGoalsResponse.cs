﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Goals;
/// <summary>
/// Contains a list of a creator's active goals.
/// </summary>
public record GetCreatorGoalsResponse
{
    /// <summary>
    /// The list of goals.
    /// This list is empty if the broadcaster hasn't created any goals.
    /// </summary>
    public required CreatorGoal[] Data { get; init; }
}

/// <summary>
/// Contains information about a specific goal that a broadcaster has created.
/// </summary>
public record CreatorGoal
{
    /// <summary>
    /// The goal's id.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster who created the goal.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The display name of the broadcaster who created the goal.
    /// </summary>
    public required string BroadcasterName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster who created the goal.
    /// </summary>
    public required string BroadcasterLogin { get; init; }
    /// <summary>
    /// The type of goal.
    /// </summary>
    [JsonConverter(typeof(SnakeCaseLowerJsonStringEnumConverter<CreatorGoalType>))]
    public required CreatorGoalType Type { get; init; }
    /// <summary>
    /// A description of the goal. Is an empty string if not specified.
    /// </summary>
    public required string Description { get; init; }
    /// <summary>
    /// The goal’s current value. The meaning of this depends on the <see cref="Type"/>.
    /// </summary>
    public required int CurrentAmount { get; init; }
    /// <summary>
    /// The goal's target value. The meaning of this depends on the <see cref="Type"/>.
    /// </summary>
    public required int TargetAmount { get; init; }
    /// <summary>
    /// The date and time when the broadcaster created the goal.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
}

/// <summary>
/// Possible goal types.
/// </summary>
public enum CreatorGoalType
{
    /// <summary>
    /// The goal is to increase followers.
    /// </summary>
    Follower,
    /// <summary>
    /// The goal is to increase subscription points.
    /// Higher tier subscriptions contribute more to this type of goal.
    /// </summary>
    Subscription,
    /// <summary>
    /// The goal is to increase subscriptions.
    /// This type shows the net increase or decrease in the number of subscriptions.
    /// </summary>
    SubscriptionCount,
    /// <summary>
    /// The goal is to increase subscriptions.
    /// This type shows only the net increase in tier points associated with new subscriptions (from users that have not subscribed before).
    /// Higher tier subscriptions contribute more to this type of goal.
    /// </summary>
    NewSubscription,
    /// <summary>
    /// The goal is to increase subscriptions.
    /// This type shows only the net increase in subscription count associated with new subscriptions (from users that have not subscribed before).
    /// </summary>
    NewSubscriptionCount
}
