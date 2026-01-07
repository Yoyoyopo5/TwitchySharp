using TwitchySharp.EventSub.Interfaces.Events;

namespace TwitchySharp.EventSub.Models.Events.Channel.Polls;

/// <summary>
/// Contains information about the Channel Points voting setting for a channel poll.
/// </summary>
public record ChannelPollChannelPointsVotingSetting : ISetting<int>
{
    /// <summary>
    /// Indicates whether Channel Points can be used for voting.
    /// </summary>
    public required bool IsEnabled { get; init; }
    /// <summary>
    /// The amount of Channel Points required per vote.
    /// </summary>
    public required int AmountPerVote { get; init; }
    int ISetting<int>.Value => AmountPerVote;
}
