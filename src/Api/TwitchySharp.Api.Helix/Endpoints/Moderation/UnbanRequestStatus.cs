using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.Moderation;

/// <summary>
/// Contains static definitions for possible unban request statuses.
/// </summary>
/// <param name="Value">The string value of the unban request status.</param>
[Wrapper<string>]
public readonly partial record struct UnbanRequestStatus(string Value)
{
    public static UnbanRequestStatus Pending { get; } = new("pending");
    public static UnbanRequestStatus Approved { get; } = new("approved");
    public static UnbanRequestStatus Denied { get; } = new("denied");
    public static UnbanRequestStatus Acknowledged { get; } = new("acknowledged");
    public static UnbanRequestStatus Canceled { get; } = new("canceled");
}
