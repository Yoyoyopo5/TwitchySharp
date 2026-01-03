using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models;

/// <summary>
/// The base class for channel poll event types.
/// </summary>
/// <remarks>
/// <see cref="EventSubSubscriptionType.ChannelPollBegin"/>,
/// <see cref="EventSubSubscriptionType.ChannelPollProgress"/>,
/// <see cref="EventSubSubscriptionType.ChannelPollEnd"/>.
/// </remarks>
public record ChannelPollEvent
{
    /// <summary>
    /// The id of the poll.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) hosting the poll.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) hosting the poll.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) hosting the poll.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The title of the poll.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The choices for the poll, including their vote count.
    /// </summary>
    public required ChannelPollChoice[] Choices { get; init; }
    /// <summary>
    /// The setting for Channel Points voting.
    /// </summary>
    public required ChannelPollChannelPointsVotingSetting ChannelPointsVoting { get; init; }
    /// <summary>
    /// The date and time the poll began.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
}

/// <summary>
/// Contains information about a specific choice in a channel poll.
/// </summary>
public record ChannelPollChoice
{
    /// <summary>
    /// The id of the choice.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The text displayed to viewers for the choice.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The number of votes received for the choice using Channel Points.
    /// </summary>
    public required int ChannelPointsVotes { get; init; }
    /// <summary>
    /// The total number of votes receieved for the choice across all voting methods.
    /// </summary>
    public required int Votes { get; init; }
}

/// <summary>
/// Contains information about the Channel Points voting setting for a channel poll.
/// </summary>
public record ChannelPollChannelPointsVotingSetting
{
    /// <summary>
    /// Indicates whether Channel Points can be used for voting.
    /// </summary>
    public required bool IsEnabled { get; init; }
    /// <summary>
    /// The amount of Channel Points required per vote.
    /// </summary>
    public required int AmountPerVote { get; init; }
}
