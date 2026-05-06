using TwitchySharp.EventSub.Models.Notifications.Channel;
using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Enums.Events.Channel;

/// <summary>
/// Contains static definitions for possible moderation actions in a <see cref="ChannelModerateEvent"/>.
/// </summary>
/// <param name="Value">The string value of the action.</param>
[Wrapper<string>]
public readonly partial record struct ChannelModerateActionType(string Value)
{
    public static ChannelModerateActionType Ban { get; } = new("ban");
    public static ChannelModerateActionType Timeout { get; } = new("timeout");
    public static ChannelModerateActionType Unban { get; } = new("unban");
    public static ChannelModerateActionType Untimeout { get; } = new("untimeout");
    public static ChannelModerateActionType ClearChat { get; } = new("clear");
    public static ChannelModerateActionType EmoteOnlyModeOn { get; } = new("emoteonly");
    public static ChannelModerateActionType EmoteOnlyModeOff { get; } = new("emoteonlyoff");
    public static ChannelModerateActionType FollowersOnlyModeOn { get; } = new("followers");
    public static ChannelModerateActionType FollowersOnlyModeOff { get; } = new("followersoff");
    public static ChannelModerateActionType UniqueChatModeOn { get; } = new("uniquechat");
    public static ChannelModerateActionType UniqueChatModeOff { get; } = new("uniquechatoff");
    public static ChannelModerateActionType SlowModeOn { get; } = new("slow");
    public static ChannelModerateActionType SlowModeOff { get; } = new("slowoff");
    public static ChannelModerateActionType SubscribersOnlyModeOn { get; } = new("subscribers");
    public static ChannelModerateActionType SubscribersOnlyModeOff { get; } = new("subscribersoff");
    public static ChannelModerateActionType Unraid { get; } = new("unraid");
    public static ChannelModerateActionType DeleteMessage { get; } = new("delete");
    public static ChannelModerateActionType Unvip { get; } = new("unvip");
    public static ChannelModerateActionType Vip { get; } = new("vip");
    public static ChannelModerateActionType Raid { get; } = new("raid");
    public static ChannelModerateActionType AddBlockedTerm { get; } = new("add_blocked_term");
    public static ChannelModerateActionType AddPermittedTerm { get; } = new("add_permitted_term");
    public static ChannelModerateActionType RemoveBlockedTerm { get; } = new("remove_blocked_term");
    public static ChannelModerateActionType RemovePermittedTerm { get; } = new("remove_permitted_term");
    public static ChannelModerateActionType Mod { get; } = new("mod");
    public static ChannelModerateActionType Unmod { get; } = new("unmod");
    public static ChannelModerateActionType Warn { get; } = new("warn"); // V2 only
    public static ChannelModerateActionType ApproveUnbanRequest { get; } = new("approve_unban_request");
    public static ChannelModerateActionType DenyUnbanRequest { get; } = new("deny_unban_request");
    public static ChannelModerateActionType SharedChatBan { get; } = new("shared_chat_ban");
    public static ChannelModerateActionType SharedChatTimeout { get; } = new("shared_chat_timeout");
    public static ChannelModerateActionType SharedChatUntimeout { get; } = new("shared_chat_untimeout");
    public static ChannelModerateActionType SharedChatUnban { get; } = new("shared_chat_unban");
    public static ChannelModerateActionType SharedChatDeleteMessage { get; } = new("shared_chat_delete");
}
