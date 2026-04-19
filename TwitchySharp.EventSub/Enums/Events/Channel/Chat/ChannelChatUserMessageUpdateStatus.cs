using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Enums.Events.Channel.Chat;

/// <summary>
/// Contains static definitions for possible user message update statuses.
/// </summary>
/// <param name="Value">The string value of the status.</param>
[Wrapper<string>]
public readonly partial record struct ChannelChatUserMessageUpdateStatus(string Value)
{
    public static ChannelChatUserMessageUpdateStatus Approved { get; } = new("approved");
    public static ChannelChatUserMessageUpdateStatus Denied { get; } = new("denied");
    public static ChannelChatUserMessageUpdateStatus Invalid { get; } = new("invalid");
}
