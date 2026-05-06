namespace TwitchySharp.Api.Helix.Entitlements;
/// <summary>
/// Contains a list of updated entitlements.
/// </summary>
public record UpdateDropsEntitlementsResponse
{
    /// <summary>
    /// A list of entitlements that were updated, grouped by <see cref="EntitlementUpdateStatus"/>.
    /// </summary>
    public required UpdatedEntitlements[] Data { get; init; }
}