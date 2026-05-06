using System;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.HypeTrain;

/// <summary>
/// Contains information about a specific Hype Train.
/// </summary>
public record HypeTrain
{
    /// <summary>
    /// The id of the Hype Train.
    /// </summary>
    public required HypeTrainId Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) the Hype Train is for.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) the Hype Train is for.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) the Hype Train is for.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The current level of the Hype Train.
    /// </summary>
    public required int Level { get; init; }
    /// <summary>
    /// The total points contributed to the Hype Train.
    /// </summary>
    public required int Total { get; init; }
    /// <summary>
    /// The number of points contributed to the current level.
    /// </summary>
    public required int Progress { get; init; }
    /// <summary>
    /// The number of points required to reach the next level.
    /// </summary>
    public required int Goal { get; init; }
    /// <summary>
    /// An array of the top Hype Train contributions.
    /// </summary>
    /// <remarks>
    /// Dev Note: not sure exactly how many will be listed here.
    /// </remarks>
    public required HypeTrainContribution[] TopContributions { get; init; }
    /// <summary>
    /// An array of broadcasters participating in the shared chat Hype Train.
    /// </summary>
    /// <remarks>
    /// This is <see langword="null"/> if the Hype Train is not in a shared chat.
    /// </remarks>
    public HypeTrainParticipant[]? SharedTrainParticipants { get; init; }
    /// <summary>
    /// The date and time when the Hype Train started.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
    /// <summary>
    /// The date and time when the Hype Train expires.
    /// </summary>
    /// <remarks>
    /// The expiration is extended when the Hype Train reaches a new level.
    /// </remarks>
    public required DateTimeOffset ExpiresAt { get; init; }
    /// <summary>
    /// The type of Hype Train.
    /// </summary>
    public required HypeTrainType Type { get; init; }
    /// <summary>
    /// Indicates whether the Hype Train is occurring in a shared chat.
    /// </summary>
    public required bool IsSharedTrain { get; init; }
}