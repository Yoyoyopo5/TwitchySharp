using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Enums.Events.Channel.UnbanRequest;

/// <summary>
/// Contains static definitions for possible statuses for channel unban requests.
/// </summary>
/// <param name="Value">The string value of the status.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ChannelUnbanRequestResolutionStatus, string>))]
public record ChannelUnbanRequestResolutionStatus(string Value) : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// The unban request was approved by a moderator.
    /// </summary>
    public static ChannelUnbanRequestResolutionStatus Approved { get; } = new("approved");
    /// <summary>
    /// The unban request was canceled.
    /// </summary>
    /// <remarks>
    /// Dev Note: Not exactly sure when this applies. It might be that the user canceled their own request,
    /// or potentially that the user was unbanned before the request was resolved.
    /// </remarks>
    public static ChannelUnbanRequestResolutionStatus Canceled { get; } = new("canceled");
    /// <summary>
    /// The unban request was denied by a moderator.
    /// </summary>
    public static ChannelUnbanRequestResolutionStatus Denied { get; } = new("denied");
}
