using TwitchySharp.EventSub.Enums.Events.Channel;
using TwitchySharp.EventSub.Models.Events.Channel;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Interfaces.Events.Channel;

/// <summary>
/// A channel moderation event.
/// </summary>
/// <remarks>
/// <see cref="EventSubSubscriptionType.ChannelModerate"/>,
/// <see cref="EventSubSubscriptionType.ChannelModerateV2"/>.
/// </remarks>
public interface IHaveChannelModerateAction
{
    /// <summary>
    /// The type of moderation action that was performed.
    /// </summary>
    /// <remarks>
    /// You can use this to determine which of the other properties should be populated.
    /// </remarks>
    ChannelModerateActionType Action { get; }
    /// <summary>
    /// Data associated with the followers mode command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.FollowersOnlyModeOn"/>.
    /// </summary>
    ChannelModerateFollowersModeAction? Followers { get; }
    /// <summary>
    /// Data associated with the slow mode command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.SlowModeOn"/>.
    /// </summary>
    ChannelModerateSlowModeAction? Slow { get; }
    /// <summary>
    /// Data associated with the vip command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.Vip"/>.
    /// </summary>
    ChannelModerateVipAction? Vip { get; }
    /// <summary>
    /// Data associated with the unvip command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.Unvip"/>.
    /// </summary>
    ChannelModerateUnvipAction? Unvip { get; }
    /// <summary>
    /// Data associated with the mod command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.Mod"/>.
    /// </summary>
    ChannelModerateModAction? Mod { get; }
    /// <summary>
    /// Data associated with the unmod command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.Unmod"/>.
    /// </summary>
    ChannelModerateUnmodAction? Unmod { get; }
    /// <summary>
    /// Data associated with the ban command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.Ban"/>.
    /// </summary>
    ChannelModerateBanAction? Ban { get; }
    /// <summary>
    /// Data associated with the unban command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.Unban"/>.
    /// </summary>
    ChannelModerateUnbanAction? Unban { get; }
    /// <summary>
    /// Data associated with the timeout command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.Timeout"/>.
    /// </summary>
    ChannelModerateTimeoutAction? Timeout { get; }
    /// <summary>
    /// Data associated with the untimeout command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.Untimeout"/>.
    /// </summary>
    ChannelModerateUntimeoutAction? Untimeout { get; }
    /// <summary>
    /// Data associated with the raid command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.Raid"/>.
    /// </summary>
    ChannelModerateRaidAction? Raid { get; }
    /// <summary>
    /// Data associated with the unraid command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.Unraid"/>.
    /// </summary>
    ChannelModerateUnraidAction? Unraid { get; }
    /// <summary>
    /// Data associated with the delete command.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.DeleteMessage"/>.
    /// </summary>
    ChannelModerateDeleteMessageAction? Delete { get; }
    /// <summary>
    /// Data associated with automod terms changes.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.AddBlockedTerm"/>, <see cref="ChannelModerateActionType.AddPermittedTerm"/>, <see cref="ChannelModerateActionType.RemoveBlockedTerm"/>, or <see cref="ChannelModerateActionType.RemovePermittedTerm"/>.
    /// </summary>
    ChannelModerateAutomodTermsAction? AutomodTerms { get; }
    /// <summary>
    /// Data associated with an unban request resolution.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.ApproveUnbanRequest"/> or <see cref="ChannelModerateActionType.DenyUnbanRequest"/>.
    /// </summary>
    ChannelModerateUnbanRequestAction? UnbanRequest { get; }
    /// <summary>
    /// Data associated with a ban action in a shared chat.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.SharedChatBan"/>.
    /// </summary>
    ChannelModerateBanAction? SharedChatBan { get; }
    /// <summary>
    /// Data associated with an unban action in a shared chat.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.SharedChatUnban"/>.
    /// </summary>
    ChannelModerateUnbanAction? SharedChatUnban { get; }
    /// <summary>
    /// Data associated with a timeout action in a shared chat.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.SharedChatTimeout"/>.
    /// </summary>
    ChannelModerateTimeoutAction? SharedChatTimeout { get; }
    /// <summary>
    /// Data associated with an untimeout action in a shared chat.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.SharedChatUntimeout"/>.
    /// </summary>
    ChannelModerateUntimeoutAction? SharedChatUntimeout { get; }
    /// <summary>
    /// Data associated with a delete message action in a shared chat.
    /// This is <see langword="null"/> unless <see cref="Action"/> is set to <see cref="ChannelModerateActionType.DeleteMessage"/>.
    /// </summary>
    ChannelModerateDeleteMessageAction? SharedChatDelete { get; }
}
