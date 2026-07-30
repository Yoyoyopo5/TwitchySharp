namespace TwitchySharp.EventSub.Notifications;

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
