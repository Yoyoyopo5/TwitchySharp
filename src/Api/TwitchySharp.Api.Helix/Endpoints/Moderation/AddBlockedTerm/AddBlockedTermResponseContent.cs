namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains a list of newly blocked terms.
/// </summary>
public record AddBlockedTermResponseContent
{
    /// <summary>
    /// A list containing the single new term that was blocked.
    /// </summary>
    public required BlockedTerm[] Data { get; init; }
}
