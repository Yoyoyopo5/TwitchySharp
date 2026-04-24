using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Enums.Events.Channel.UnbanRequest;

/// <summary>
/// Contains static definitions for possible statuses for channel unban requests.
/// </summary>
/// <param name="Value">The string value of the status.</param>
[Wrapper<string>]
public readonly partial record struct ChannelUnbanRequestResolutionStatus(string Value)
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
