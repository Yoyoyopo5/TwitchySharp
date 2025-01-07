using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Contains static definitions for possible values for extension types.
/// See <see href="https://dev.twitch.tv/docs/extensions/#extension-types">Extension Types</see>
/// </summary>
/// <param name="Value">
/// A custom value for extension type. 
/// Use this if a static definition is not available for the type you want.
/// </param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<UserExtensionType, string>))] // Using this here so we can deserialize an array of them.
public record UserExtensionType(string Value)
    : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// Displays as part of the video, taking up part of the screen. Component Extensions can be hidden by viewers.
    /// </summary>
    public static UserExtensionType Component { get; } = new("component");
    /// <summary>
    /// Displayed similarly to a panel extension on a mobile device.
    /// </summary>
    public static UserExtensionType Mobile { get; } = new("mobile");
    /// <summary>
    /// Displays on top of the whole video as a transparent overlay.
    /// </summary>
    public static UserExtensionType Overlay { get; } = new("overlay");
    /// <summary>
    /// Displays in a box under the video.
    /// </summary>
    public static UserExtensionType Panel { get; } = new("panel");
}
