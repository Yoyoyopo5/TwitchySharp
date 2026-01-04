using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Enums.Events.Automod.Message;

/// <summary>
/// Contains static definitions for possible Automod categories for held messages.
/// </summary>
/// <param name="Value">The string value of the Automod message category.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<AutomodMessageCategory, string>))]
public record AutomodMessageCategory(string Value) : ValueBackedEnum<string>(Value)
{
    // TODO: figure out what these categories can be, docs did not list.
}
