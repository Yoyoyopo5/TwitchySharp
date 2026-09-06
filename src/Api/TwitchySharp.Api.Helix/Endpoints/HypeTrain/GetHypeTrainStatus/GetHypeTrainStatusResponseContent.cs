namespace TwitchySharp.Api.Helix.HypeTrain;
/// <summary>
/// Contains an array of Hype Train data for a specific broadcaster (channel).
/// </summary>
public record GetHypeTrainStatusResponseContent
{
    /// <summary>
    /// An array containing a single object with Hype Train information for a specific channel.
    /// </summary>
    public required HypeTrainStatus[] Data { get; init; }
}
