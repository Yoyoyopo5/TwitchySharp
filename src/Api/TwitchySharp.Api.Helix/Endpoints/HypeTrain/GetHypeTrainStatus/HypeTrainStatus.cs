namespace TwitchySharp.Api.Helix.HypeTrain;

/// <summary>
/// Contains information about Hype Trains for a specific broadcaster.
/// </summary>
public record HypeTrainStatus
{
    /// <summary>
    /// The current Hype Train, if one is active.
    /// </summary>
    public HypeTrain? Current { get; init; }
    /// <summary>
    /// The all-time high Hype Train record for the channel.
    /// </summary>
    /// <remarks>
    /// This is <see langword="null"/> if a Hype Train has never occurred on the channel.
    /// </remarks>
    public HypeTrainRecord? AllTimeHigh { get; init; }
    /// <summary>
    /// The all-time high Hype Train record for shared chats the broadcaster participated in.
    /// </summary>
    /// <remarks>
    /// This is <see langword="null"/> if the broadcaster has not participated in a Hype Train in a shared chat.
    /// </remarks>
    public HypeTrainRecord? SharedAllTimeHigh { get; init; }
}
