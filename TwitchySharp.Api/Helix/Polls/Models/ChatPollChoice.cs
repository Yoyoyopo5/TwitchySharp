namespace TwitchySharp.Api.Helix.Polls;

/// <summary>
/// Contains information about a specific choice in a chat poll.
/// </summary>
public record ChatPollChoice
{
    /// <summary>
    /// The id of the choice.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The title of the choice.
    /// This may be up to 25 characters.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The total number of votes cast for this choice.
    /// </summary>
    public required int Votes { get; init; }
    /// <summary>
    /// The number of votes that were cast using Channel Points.
    /// </summary>
    public required int ChannelPointsVotes { get; init; }
    /// <summary>
    /// Unused and always set to <c>0</c>.
    /// </summary>
    public required int BitsVotes { get; init; }
}
