using System.Text.Json.Serialization;
using TwitchySharp.Api.Models.Helix.Moderation.Requests;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Moderation.Enums;

/// <summary>
/// Contains static definitions for held AutoMod message actions.
/// Used with <see cref="ManageHeldAutoModMessagesRequest"/>.
/// </summary>
/// <param name="Value">The name of the action to take.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<AutoModAction, string>))]
public record AutoModAction(string Value)
    : ValueBackedEnum<string>(Value)
{
    public static AutoModAction Allow { get; } = new("ALLOW");
    public static AutoModAction Deny { get; } = new("DENY");
}
