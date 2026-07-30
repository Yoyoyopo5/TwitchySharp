namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelModerateV2"/> event.
/// </summary>
public record ChannelModerateV2Event
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
    public required UserId SourceBroadcasterUserId { get; init; } // Docs are inconsistent on these, I'll leave required unless testing reveals null values
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the moderation action occurred in within a shared chat.
    /// </summary>
    public required UserLogin SourceBroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the moderation action occurred in within a shared chat.
    /// </summary>
    public required UserName SourceBroadcasterUserName { get; init; }
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
    /// <inheritdoc cref="ChannelModerateEvent.Action"/>
    public required ChannelModerateActionType Action { get; init; }
    /// <inheritdoc cref="ChannelModerateEvent.Followers"/>
    public ChannelModerateFollowersModeAction? Followers { get; init; }
    /// <inheritdoc cref="ChannelModerateEvent.Slow"/>
    public ChannelModerateSlowModeAction? Slow { get; init; }
    /// <inheritdoc cref="ChannelModerateEvent.Vip"/>
    public ChannelModerateVipAction? Vip { get; init; }
    /// <inheritdoc cref="ChannelModerateEvent.Unvip"/>
    public ChannelModerateUnvipAction? Unvip { get; init; }
    /// <inheritdoc cref="ChannelModerateEvent.Mod"/>
    public ChannelModerateModAction? Mod { get; init; }
    /// <inheritdoc cref="ChannelModerateEvent.Unmod"/>
    public ChannelModerateUnmodAction? Unmod { get; init; }
    /// <inheritdoc cref="ChannelModerateEvent.Ban"/>
    public ChannelModerateBanAction? Ban { get; init; }
    /// <inheritdoc cref="ChannelModerateEvent.Unban"/>
    public ChannelModerateUnbanAction? Unban { get; init; }
    /// <inheritdoc cref="ChannelModerateEvent.Timeout"/>
    public ChannelModerateTimeoutAction? Timeout { get; init; }
    /// <inheritdoc cref="ChannelModerateEvent.Untimeout"/>
    public ChannelModerateUntimeoutAction? Untimeout { get; init; }
    /// <inheritdoc cref="ChannelModerateEvent.Raid"/>
    public ChannelModerateRaidAction? Raid { get; init; }
    /// <inheritdoc cref="ChannelModerateEvent.Unraid"/>
    public ChannelModerateUnraidAction? Unraid { get; init; }
    /// <inheritdoc cref="ChannelModerateEvent.Delete"/>
    public ChannelModerateDeleteMessageAction? Delete { get; init; }
    /// <inheritdoc cref="ChannelModerateEvent.AutomodTerms"/>
    public ChannelModerateAutomodTermsAction? AutomodTerms { get; init; }
    /// <inheritdoc cref="ChannelModerateEvent.UnbanRequest"/>
    public ChannelModerateUnbanRequestAction? UnbanRequest { get; init; }
    /// <inheritdoc cref="ChannelModerateEvent.SharedChatBan"/>
    public ChannelModerateBanAction? SharedChatBan { get; init; }
    /// <inheritdoc cref="ChannelModerateEvent.SharedChatUnban"/>
    public ChannelModerateUnbanAction? SharedChatUnban { get; init; }
    /// <inheritdoc cref="ChannelModerateEvent.SharedChatTimeout"/>
    public ChannelModerateTimeoutAction? SharedChatTimeout { get; init; }
    /// <inheritdoc cref="ChannelModerateEvent.SharedChatUntimeout"/>
    public ChannelModerateUntimeoutAction? SharedChatUntimeout { get; init; }
    /// <inheritdoc cref="ChannelModerateEvent.Delete"/>
    public ChannelModerateDeleteMessageAction? SharedChatDelete { get; init; }
    /// <summary>
    /// Data associated with a warn command.
    /// This is <see langword="null"/> unless <see cref="ChannelModerateEvent.Action"/> is set to <see cref="ChannelModerateActionType.Warn"/>.
    /// </summary>
    public ChannelModerateWarnAction? Warn { get; init; }
}
