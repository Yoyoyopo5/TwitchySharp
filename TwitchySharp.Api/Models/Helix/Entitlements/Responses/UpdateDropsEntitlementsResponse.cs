using TwitchySharp.Api.Models.Helix.Entitlements.Enums;
using TwitchySharp.Api.Models.Helix.Entitlements.Models;

namespace TwitchySharp.Api.Models.Helix.Entitlements.Responses;
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