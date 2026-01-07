using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Enums.Events.Automod.Terms;

/// <summary>
/// Contains static definitions for the possible Automod terms update actions.
/// </summary>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<AutomodTermsUpdateAction, string>))]
public record AutomodTermsUpdateAction(string Value) : ValueBackedEnum<string>(Value)
{
    public static AutomodTermsUpdateAction AddPermitted { get; } = new("add_permitted");
    public static AutomodTermsUpdateAction RemovePermitted { get; } = new("remove_permitted");
    public static AutomodTermsUpdateAction AddBlocked { get; } = new("add_blocked");
    public static AutomodTermsUpdateAction RemoveBlocked { get; } = new("remove_blocked");
}
