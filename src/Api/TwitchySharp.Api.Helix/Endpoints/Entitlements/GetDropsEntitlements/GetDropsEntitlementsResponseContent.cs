namespace TwitchySharp.Api.Helix.Entitlements;
/// <summary>
/// Contains a list of drops entitlements.
/// </summary>
public record GetDropsEntitlementsResponseContent
    : IPageableResponse
{
    /// <summary>
    /// The list of entitlements.
    /// </summary>
    public required DropsEntitlement[] Data { get; init; }
    /// <summary>
    /// The information used to page through the list of results. 
    /// The <see cref="Pagination.Cursor"/> is <see langword="null"/> if there are no more pages left to page through. 
    /// </summary>
    public required Pagination Pagination { get; init; }
}
