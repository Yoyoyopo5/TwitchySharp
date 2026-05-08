using TwitchySharp.EventSub.Enums.Events.Channel;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Channel;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Events.Channel;

namespace TwitchySharp.EventSub.Models.Notifications.Channel;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelModerateV2"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelmoderate-v2">Channel Moderate V2</see> for more information.
/// </remarks>
public record ChannelModerateV2Notification : EventSubNotification<ChannelModerateV2Event, ChannelModerateV2Condition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelModerateV2"/>.
/// </summary>
public record ChannelModerateV2Condition : BroadcasterModeratorCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelModerateV2"/> event.
/// </summary>
public record ChannelModerateV2Event : IHaveChannelModerateAction, IHaveBroadcaster, IHaveModerator // This may come back to bite us in the ass later.
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
    public required ChannelModerateActionType Action { get; init; }
    public ChannelModerateFollowersModeAction? Followers { get; init; }
    public ChannelModerateSlowModeAction? Slow { get; init; }
    public ChannelModerateVipAction? Vip { get; init; }
    public ChannelModerateUnvipAction? Unvip { get; init; }
    public ChannelModerateModAction? Mod { get; init; }
    public ChannelModerateUnmodAction? Unmod { get; init; }
    public ChannelModerateBanAction? Ban { get; init; }
    public ChannelModerateUnbanAction? Unban { get; init; }
    public ChannelModerateTimeoutAction? Timeout { get; init; }
    public ChannelModerateUntimeoutAction? Untimeout { get; init; }
    public ChannelModerateRaidAction? Raid { get; init; }
    public ChannelModerateUnraidAction? Unraid { get; init; }
    public ChannelModerateDeleteMessageAction? Delete { get; init; }
    public ChannelModerateAutomodTermsAction? AutomodTerms { get; init; }
    public ChannelModerateUnbanRequestAction? UnbanRequest { get; init; }
    public ChannelModerateBanAction? SharedChatBan { get; init; }
    public ChannelModerateUnbanAction? SharedChatUnban { get; init; }
    public ChannelModerateTimeoutAction? SharedChatTimeout { get; init; }
    public ChannelModerateUntimeoutAction? SharedChatUntimeout { get; init; }
    public ChannelModerateDeleteMessageAction? SharedChatDelete { get; init; }
    /// <summary>
    /// Data associated with a warn command.
    /// This is <see langword="null"/> unless <see cref="ChannelModerateEvent.Action"/> is set to <see cref="ChannelModerateActionType.Warn"/>.
    /// </summary>
    public ChannelModerateWarnAction? Warn { get; init; }
}
