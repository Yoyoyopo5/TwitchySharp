using TwitchySharp.Api.Models.Helix.Entitlements.Enums;

namespace TwitchySharp.Api.Models.Helix.Entitlements.Models;

/// <summary>
/// Contains information on a group of entitlements updated with a specific <see cref="EntitlementUpdateStatus"/>.
/// </summary>
public record UpdatedEntitlements
{
    /// <summary>
    /// The status of the update.
    /// </summary>
    public required EntitlementUpdateStatus Status { get; init; }
    /// <summary>
    /// The ids of the entitlements that were updated.
    /// </summary>
    public required string[] Ids { get; init; }
}