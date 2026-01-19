namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains a list of blocked terms on a specific channel.
/// </summary>
public record GetBlockedTermsResponse
{
    /// <summary>
    /// The list of blocked terms.
    /// The list is in descending order by <see cref="BlockedTerm.CreatedAt"/> (newest first).
    /// </summary>
    public required BlockedTerm[] Data { get; init; }
    /// <inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}
