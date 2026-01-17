namespace TwitchySharp.Api.Models.Helix.Moderation.Enums;

using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

/// <summary>
/// Contains static definitions for possible unban request statuses.
/// </summary>
/// <param name="Value">The string value of the unban request status.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<UnbanRequestStatus, string>))]
public record UnbanRequestStatus(string Value) : ValueBackedEnum<string>(Value)
{
    public static UnbanRequestStatus Pending { get; } = new("pending");
    public static UnbanRequestStatus Approved { get; } = new("approved");
    public static UnbanRequestStatus Denied { get; } = new("denied");
    public static UnbanRequestStatus Acknowledged { get; } = new("acknowledged");
    public static UnbanRequestStatus Canceled { get; } = new("canceled");
}