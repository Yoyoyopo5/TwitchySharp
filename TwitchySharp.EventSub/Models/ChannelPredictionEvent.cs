using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models;

/// <summary>
/// The base class for Channel Prediction event types.
/// </summary>
/// <remarks>
/// <see cref="EventSubSubscriptionType.ChannelPredictionBegin"/>,
/// <see cref="EventSubSubscriptionType.ChannelPredictionProgress"/>,
/// <see cref="EventSubSubscriptionType.ChannelPredictionLock"/>,
/// <see cref="EventSubSubscriptionType.ChannelPredictionEnd"/>.
/// </remarks>
public record ChannelPredictionEvent
{
    /// <summary>
    /// The id of the prediction.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that is hosting the prediction.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that is hosting the prediction.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that is hosting the prediction.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The title of the prediction.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The outcomes of the prediction.
    /// </summary>
    public required ChannePredictionOutcome[] Outcomes { get; init; }
    /// <summary>
    /// The date and time the prediction started.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
}

/// <summary>
/// Contains information about a specific channel prediction outcome.
/// </summary>
public record ChannePredictionOutcome
{
    /// <summary>
    /// The id of the outcome.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The title of the outcome.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The color of the outcome.
    /// </summary>
    public required ChannelPredictionColor Color { get; init; }
    /// <summary>
    /// The total number of users who chose this outcome.
    /// </summary>
    public required int Users { get; init; }
    /// <summary>
    /// The total number of Channel Points bet on this outcome.
    /// </summary>
    public required int ChannelPoints { get; init; }
    /// <summary>
    /// An array of users who used the highest amount of Channel Points on this outcome.
    /// </summary>
    /// <remarks>
    /// Dev Note: I believe this is at most 10 users. Not sure if the array is sorted.
    /// </remarks>
    public required ChannelPredictionPredictor[] TopPredictors { get; init; }
}

/// <summary>
/// Contains static definitions for possible channel prediction outcome colors.
/// </summary>
/// <param name="Value">The string value of the color.</param>
public record ChannelPredictionColor(string Value) : ValueBackedEnum<string>(Value)
{
    public static ChannelPredictionColor Pink { get; } = new("pink");
    public static ChannelPredictionColor Blue { get; } = new("blue");
}

/// <summary>
/// Contains information on a specific user that pariticpated in a channel prediction.
/// </summary>
public record ChannelPredictionPredictor
{
    /// <summary>
    /// The user id of the predictor.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the predictor.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the predictor.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The amount of Channel Points the predictor won in the prediction.
    /// This is <see langword="null"/> if the underlying prediction has not ended.
    /// This is <c>0</c> if the predictor did not guess the correct outcome or
    /// if the prediction was cancelled.
    /// </summary>
    public int? ChannelPointsWon { get; init; }
    /// <summary>
    /// The amount of Channel Points the predictor used to participate in the prediction.
    /// </summary>
    public required int ChannelPointsUsed { get; init; }
}
