namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelModerate"/> event.
/// </summary>
public record ChannelModerateEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) chat that the moderation action occurred in.
    /// In a shared chat, use <see cref="SourceBroadcasterUserId"/>.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) chat that the moderation action occurred in.
    /// In a shared chat, use <see cref="SourceBroadcasterUserLogin"/>.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) chat that the moderation action occurred in.
    /// In a shared chat, use <see cref="SourceBroadcasterUserName"/>.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that the moderation action occurred in within a shared chat.
    /// </summary>
    public required UserId? SourceBroadcasterUserId { get; init; } // Docs are inconsistent on these, I'll leave required unless testing reveals null values
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the moderation action occurred in within a shared chat.
    /// </summary>
    public required UserLogin? SourceBroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the moderation action occurred in within a shared chat.
    /// </summary>
    public required UserName? SourceBroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the moderator that performed the moderation action.
    /// </summary>
    public required UserId ModeratorUserId { get; init; }
    /// <summary>
    /// The login (username) of the moderator that performed the moderation action.
    /// </summary>
    public required UserLogin ModeratorUserLogin { get; init; }
    /// <summary>
    /// The display name of the moderator that performed the moderation action.
    /// </summary>
    public required UserName ModeratorUserName { get; init; }
    /// <summary>
    /// The type of moderation action that was performed.
    /// </summary>
    /// <remarks>
    /// You can use this to determine which of the other properties should be populated.
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
