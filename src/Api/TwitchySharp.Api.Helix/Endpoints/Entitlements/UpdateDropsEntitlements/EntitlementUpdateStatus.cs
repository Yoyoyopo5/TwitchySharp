using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.Entitlements;

/// <summary>
/// Contains static definitions for possible statuses of an entitlement update.
/// </summary>
/// <param name="Value">The string value of the entitlement update status.</param>
[Wrapper<string>]
public readonly partial record struct EntitlementUpdateStatus(string Value)
{
    /// <summary>
    /// The entitlement ids in the request's ids field are not valid.
    /// </summary>
    public static EntitlementUpdateStatus InvalidId { get; } = new("INVALID_ID");
    /// <summary>
    /// The entitlement ids in the request's ids field were not found.
    /// </summary>
    public static EntitlementUpdateStatus NotFound { get; } = new("NOT_FOUND");
    /// <summary>
    /// The status of the entitlements in the request's ids field were successfully updated.
    /// </summary>
    public static EntitlementUpdateStatus Success { get; } = new("SUCCESS");
    /// <summary>
    /// The user or organization identified by the request's user access token is not authorized to update the entitlements.
    /// </summary>
    public static EntitlementUpdateStatus Unauthorized { get; } = new("UNAUTHORIZED");
    /// <summary>
    /// The update failed. These are considered transient errors and the request should be retried later.
    /// </summary>
    public static EntitlementUpdateStatus UpdateFailed { get; } = new("UPDATE_FAILED");
}
