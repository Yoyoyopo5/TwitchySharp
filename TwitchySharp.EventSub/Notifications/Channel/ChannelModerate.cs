using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Helpers.JsonConverters;
using TwitchySharp.EventSub.Models.Conditions;

namespace TwitchySharp.EventSub.Notifications.Channel;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelModerate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelmoderate">Channel Moderate</see> for more information.
/// </remarks>
public record ChannelModerateNotification : EventSubNotification<ChannelModerateEvent, ChannelModerateCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelModerate"/>.
/// </summary>
public record ChannelModerateCondition : BroadcasterModeratorCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelModerate"/> event.
/// </summary>
public record ChannelModerateEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) chat that the moderation action occurred in.
    /// In a shared chat, use <see cref="SourceBroadcasterUserId"/>.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) chat that the moderation action occurred in.
    /// In a shared chat, use <see cref="SourceBroadcasterUserLogin"/>.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) chat that the moderation action occurred in.
    /// In a shared chat, use <see cref="SourceBroadcasterUserName"/>.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that the moderation action occurred in within a shared chat.
    /// </summary>
    public required string SourceBroadcasterUserId { get; init; } // Docs are inconsistent on these, I'll leave required unless testing reveals null values
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the moderation action occurred in within a shared chat.
    /// </summary>
    public required string SourceBroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the moderation action occurred in within a shared chat.
    /// </summary>
    public required string SourceBroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the moderator that performed the moderation action.
    /// </summary>
    public required string ModeratorUserId { get; init; }
    /// <summary>
    /// The login (username) of the moderator that performed the moderation action.
    /// </summary>
    public required string ModeratorUserLogin { get; init; }
    /// <summary>
    /// The display name of the moderator that performed the moderation action.
    /// </summary>
    public required string ModeratorUserName { get; init; }
    /// <summary>
    /// The type of moderation action that was performed.
    /// </summary>
    /// <remarks>
    /// You can use this to determine which of the other properties are populated.
    /// </remarks>
    public required ChannelModerateActionType Action { get; init; }
    /// <summary>
    /// Data associated with the followers mode command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.FollowersOnlyModeOn"/>.
    /// </summary>
    public ChannelModerateFollowersModeAction? Followers { get; init; }
    /// <summary>
    /// Data associated with the slow mode command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.SlowModeOn"/>.
    /// </summary>
    public ChannelModerateSlowModeAction? Slow { get; init; }
    /// <summary>
    /// Data associated with the vip command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.Vip"/>.
    /// </summary>
    public ChannelModerateVipAction? Vip { get; init; }
    /// <summary>
    /// Data associated with the unvip command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.Unvip"/>.
    /// </summary>
    public ChannelModerateUnvipAction? Unvip { get; init; }
    /// <summary>
    /// Data associated with the mod command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.Mod"/>.
    /// </summary>
    public ChannelModerateModAction? Mod { get; init; }
    /// <summary>
    /// Data associated with the unmod command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.Unmod"/>.
    /// </summary>
    public ChannelModerateUnmodAction? Unmod { get; init; }
    /// <summary>
    /// Data associated with the ban command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.Ban"/>.
    /// </summary>
    public ChannelModerateBanAction? Ban { get; init; }
    /// <summary>
    /// Data associated with the unban command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.Unban"/>.
    /// </summary>
    public ChannelModerateUnbanAction? Unban { get; init; }
    /// <summary>
    /// Data associated with the timeout command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.Timeout"/>.
    /// </summary>
    public ChannelModerateTimeoutAction? Timeout { get; init; }
    /// <summary>
    /// Data associated with the untimeout command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.Untimeout"/>.
    /// </summary>
    public ChannelModerateUntimeoutAction? Untimeout { get; init; }
    /// <summary>
    /// Data associated with the raid command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.Raid"/>.
    /// </summary>
    public ChannelModerateRaidAction? Raid { get; init; }
    /// <summary>
    /// Data associated with the unraid command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.Unraid"/>.
    /// </summary>
    public ChannelModerateUnraidAction? Unraid { get; init; }
    /// <summary>
    /// Data associated with the delete command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.DeleteMessage"/>.
    /// </summary>
    public ChannelModerateDeleteMessageAction? Delete { get; init; }
    /// <summary>
    /// Data associated with automod terms changes.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.AddBlockedTerm"/>, <see cref="ChannelModerateActionType.AddPermittedTerm"/>, <see cref="ChannelModerateActionType.RemoveBlockedTerm"/>, or <see cref="ChannelModerateActionType.RemovePermittedTerm"/>.
    /// </summary>
    public ChannelModerateAutomodTermsAction? AutomodTerms { get; init; }
    /// <summary>
    /// Data associated with an unban request resolution.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.ApproveUnbanRequest"/> or <see cref="ChannelModerateActionType.DenyUnbanRequest"/>.
    /// </summary>
    public ChannelModerateUnbanRequestAction? UnbanRequest { get; init; }
    /// <summary>
    /// Data associated with a ban action in a shared chat.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.SharedChatBan"/>.
    /// </summary>
    public ChannelModerateBanAction? SharedChatBan { get; init; }
    /// <summary>
    /// Data associated with an unban action in a shared chat.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.SharedChatUnban"/>.
    /// </summary>
    public ChannelModerateUnbanAction? SharedChatUnban { get; init; }
    /// <summary>
    /// Data associated with a timeout action in a shared chat.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.SharedChatTimeout"/>.
    /// </summary>
    public ChannelModerateTimeoutAction? SharedChatTimeout { get; init; }
    /// <summary>
    /// Data associated with an untimeout action in a shared chat.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.SharedChatUntimeout"/>.
    /// </summary>
    public ChannelModerateUntimeoutAction? SharedChatUntimeout { get; init; }
    /// <summary>
    /// Data associated with a delete message action in a shared chat.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.DeleteMessage"/>.
    /// </summary>
    public ChannelModerateDeleteMessageAction? SharedChatDelete { get; init; }
}

/// <summary>
/// Contains static definitions for possible moderation actions in a <see cref="ChannelModerateEvent"/>.
/// </summary>
/// <param name="Value">The string value of the action.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ChannelModerateActionType, string>))]
public record ChannelModerateActionType(string Value) : ValueBackedEnum<string>(Value)
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

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.FollowersOnlyModeOn"/> action.
/// </summary>
public record ChannelModerateFollowersModeAction
{
    /// <summary>
    /// The length of time that followers must have followed the broadcaster to send messages in chat.
    /// </summary>
    [JsonConverter(typeof(MinutesTimeSpanJsonConverter))]
    [JsonPropertyName("follow_duration_minutes")]
    public required TimeSpan FollowDuration { get; init; }
}

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.SlowModeOn"/> action.
/// </summary>
public record ChannelModerateSlowModeAction
{
    /// <summary>
    /// The amount of time that users need to wait between sending messages in chat.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    [JsonPropertyName("wait_time_seconds")]
    public required TimeSpan WaitTime { get; init; }
}

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Vip"/> action.
/// </summary>
public record ChannelModerateVipAction
{
    /// <summary>
    /// The id of the user gaining VIP status.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user gaining VIP status.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user gaining VIP status.
    /// </summary>
    public required string UserName { get; init; }
}

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Unvip"/> action.
/// </summary>
public record ChannelModerateUnvipAction
{
    /// <summary>
    /// The id of the user losing VIP status.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user losing VIP status.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user losing VIP status.
    /// </summary>
    public required string UserName { get; init; }
}

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Mod"/> action.
/// </summary>
public record ChannelModerateModAction
{
    /// <summary>
    /// The id of the user gaining moderator status.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user gaining moderator status.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user gaining moderator status.
    /// </summary>
    public required string UserName { get; init; }
}

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Unmod"/> action.
/// </summary>
public record ChannelModerateUnmodAction
{
    /// <summary>
    /// The id of the user losing moderator status.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user losing moderator status.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user losing moderator status.
    /// </summary>
    public required string UserName { get; init; }
}

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Ban"/> or <see cref="ChannelModerateActionType.SharedChatBan"/> action.
/// </summary>
public record ChannelModerateBanAction
{
    /// <summary>
    /// The id of the user that was banned.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that was banned.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that was banned.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The moderator-provided reason for the ban, if any.
    /// </summary>
    public string? Reason { get; init; }
}

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Unban"/> or <see cref="ChannelModerateActionType.Unban"/> action.
/// </summary>
public record ChannelModerateUnbanAction
{
    /// <summary>
    /// The id of the user that was unbanned.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that was unbanned.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that was unbanned.
    /// </summary>
    public required string UserName { get; init; }
}

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Timeout"/> or <see cref="ChannelModerateActionType.SharedChatTimeout"/> action.
/// </summary>
public record ChannelModerateTimeoutAction
{
    /// <summary>
    /// The id of the user that was timed out.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that was timed out.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that was timed out.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The moderator-provided reason for the timeout, if any.
    /// </summary>
    public string? Reason { get; init; }
    /// <summary>
    /// The date and time at which the timeout will end.
    /// </summary>
    public required DateTimeOffset ExpiresAt { get; init; }
}

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Untimeout"/> or <see cref="ChannelModerateActionType.SharedChatUntimeout"/> action.
/// </summary>
public record ChannelModerateUntimeoutAction
{
    /// <summary>
    /// The id of the user that was untimed out.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that was untimed out.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that was untimed out.
    /// </summary>
    public required string UserName { get; init; }
}

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Raid"/> action.
/// </summary>
public record ChannelModerateRaidAction
{
    /// <summary>
    /// The user id of the broadcaster (channel) being raided.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) being raided.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) being raided.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The viewer count.
    /// </summary>
    /// <remarks>
    /// Dev Note: I'm not sure if this is viewer count of the stream at the moment the raid is started,
    /// or if it's the amount of viewers joining the raid.
    /// </remarks>
    public required int ViewerCount { get; init; }
}

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Unraid"/> action.
/// </summary>
public record ChannelModerateUnraidAction
{
    /// <summary>
    /// The user id of the broadcaster (channel) no longer being raided.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) no longer being raided.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) no longer being raided.
    /// </summary>
    public required string UserName { get; init; }
}

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.DeleteMessage"/> or <see cref="ChannelModerateActionType.SharedChatDeleteMessage"/> action.
/// </summary>
public record ChannelModerateDeleteMessageAction
{
    /// <summary>
    /// The id of user whose message is being deleted.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of user whose message is being deleted.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of user whose message is being deleted.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The id of the message that was deleted.
    /// </summary>
    public required string MessageId { get; init; }
    /// <summary>
    /// The message that was deleted, in <see langword="string"/> format.
    /// </summary>
    public required string MessageBody { get; init; }
}

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.AddBlockedTerm"/>, <see cref="ChannelModerateActionType.AddPermittedTerm"/>, <see cref="ChannelModerateActionType.RemoveBlockedTerm"/>, or <see cref="ChannelModerateActionType.RemovePermittedTerm"/> action.
/// </summary>
public record ChannelModerateAutomodTermsAction
{
    /// <summary>
    /// Contains static definitions for possible Automod terms action types.
    /// </summary>
    /// <param name="Value">The string value of the action type.</param>
    [JsonConverter(typeof(ValueBackedEnumJsonConverter<ActionType, string>))]
    public record ActionType(string Value) : ValueBackedEnum<string>(Value)
    {
        public static ActionType Add { get; } = new("add");
        public static ActionType Remove { get; } = new("remove");
    }
    /// <summary>
    /// Contains static definitions for possible Automod terms list types.
    /// </summary>
    /// <param name="Value">The string value of the list type.</param>
    [JsonConverter(typeof(ValueBackedEnumJsonConverter<ListType, string>))]
    public record ListType(string Value) : ValueBackedEnum<string>(Value)
    {
        public static ListType BlockedTerms { get; } = new("blocked");
        public static ListType PermittedTerms { get; } = new("permitted");
    }

    /// <summary>
    /// The Automod terms action that was performed.
    /// </summary>
    public required ActionType Action { get; init; }
    /// <summary>
    /// The Automod terms list that the action was performed on.
    /// </summary>
    public required ListType List { get; init; }
    /// <summary>
    /// The terms that were added or removed.
    /// </summary>
    public required string[] Terms { get; init; }
    /// <summary>
    /// Indicates whether the action was due to an Automod message approve or deny action.
    /// </summary>
    /// <remarks>
    /// Dev Note: I think this is refering to when Automod prompts moderators to respond to a specific flagged message.
    /// </remarks>
    public required bool FromAutomod { get; init; }
}

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.ApproveUnbanRequest"/> or <see cref="ChannelModerateActionType.DenyUnbanRequest"/> action.
/// </summary>
public record ChannelModerateUnbanRequestAction
{
    /// <summary>
    /// Indicates whether the unban request was approved or denied.
    /// </summary>
    public required bool IsApproved { get; init; }
    /// <summary>
    /// The id of the user that created the unban request.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that created the unban request.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that created the unban request.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The moderator-provided message explaining the unban request response.
    /// </summary>
    public string? ModeratorMessage { get; init; } // Pretty sure this is optional, although not indicated in docs.
}