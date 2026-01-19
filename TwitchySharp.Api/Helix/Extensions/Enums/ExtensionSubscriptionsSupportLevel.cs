using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Extensions;

/// <summary>
/// Contains static definitions for possible extension subscriptions support levels.
/// </summary>
/// <param name="Value">The string value of the extension subscriptions support level.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ExtensionSubscriptionsSupportLevel, string>))]
public record ExtensionSubscriptionsSupportLevel(string Value) : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// The extension can't view the user’s subscription level.
    /// </summary>
    public static ExtensionSubscriptionsSupportLevel None { get; } = new("none");
    /// <summary>
    /// The extension can view the user’s subscription level.
    /// </summary>
    public static ExtensionSubscriptionsSupportLevel Optional { get; } = new("optional");
}
