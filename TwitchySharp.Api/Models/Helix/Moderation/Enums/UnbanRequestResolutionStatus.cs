using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Moderation.Enums;
/// <summary>
/// Contains static definitions for possible unban request resolution statuses.
/// </summary>
/// <param name="Value">The string value of the resolution status.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<UnbanRequestResolutionStatus, string>))]
public record UnbanRequestResolutionStatus(string Value) : ValueBackedEnum<string>(Value)
{
    public static UnbanRequestResolutionStatus Approved { get; } = new("approved");
    public static UnbanRequestResolutionStatus Denied { get; } = new("denied");
}
