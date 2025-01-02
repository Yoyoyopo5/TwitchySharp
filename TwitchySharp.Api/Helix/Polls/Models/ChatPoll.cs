using System;
using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Polls;

/// <summary>
/// Contains information about a specific poll.
/// </summary>
public record ChatPoll
{
    /// <summary>
    /// The id of the poll.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that the poll was created for.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the poll was created for.
    /// </summary>
    public required string BroadcasterName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the poll was created for.
    /// </summary>
    public required string BroadcasterLogin { get; init; }
    /// <summary>
    /// The question that viewers are voting on.
    /// This may be up to 60 characters.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// A list of choices that viewers can choose from. 
    /// The list will contain a minimum of two choices and up to a maximum of five choices.
    /// </summary>
    public required ChatPollChoice[] Choices { get; init; } 
    /// <summary>
    /// Unused and always set to <see langword="false"/>.
    /// </summary>
    public required bool BitsVotingEnabled { get; init; }
    /// <summary>
    /// Unused and always set to <c>0</c>.
    /// </summary>
    public required int BitsPerVote { get; init; }
    /// <summary>
    /// Indicates whether viewers may cast additional votes using Channel Points.
    /// </summary>
    public required bool ChannelPointsVotingEnabled { get; init; }
    /// <summary>
    /// The number of points the viewer must spend to cast one additional vote, if <see cref="ChannelPointsVotingEnabled"/> is set to <see langword="true"/>.
    /// Otherwise, <c>0</c>.
    /// </summary>
    public required int ChannelPointsPerVote { get; init; }
    /// <summary>
    /// The poll's status.
    /// </summary>
    [JsonConverter(typeof(SnakeCaseUpperJsonStringEnumConverter<ChatPollStatus>))]
    public required ChatPollStatus Status { get; init; }
    /// <summary>
    /// The length of time in <b>seconds</b> that the poll will run for.
    /// </summary>
    public required int Duration { get; init; }
    /// <summary>
    /// The date and time when the poll began.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
    /// <summary>
    /// The date and time when the poll ended.
    /// If the poll is <see cref="ChatPollStatus.Active"/>, this is set to <see langword="null"/>.
    /// </summary>
    public DateTimeOffset? EndedAt { get; init; }
}
