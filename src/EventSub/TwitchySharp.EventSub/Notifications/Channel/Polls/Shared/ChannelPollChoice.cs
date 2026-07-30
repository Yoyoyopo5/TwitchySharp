namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific choice in a channel poll.
/// </summary>
public record ChannelPollChoice
{
    /// <summary>
    /// The id of the choice.
    /// </summary>
    public required PollChoiceId Id { get; init; }
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
