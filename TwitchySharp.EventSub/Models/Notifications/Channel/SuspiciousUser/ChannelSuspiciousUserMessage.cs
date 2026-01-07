using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Enums.Events.Channel.SuspiciousUser;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Channel.SuspiciousUser;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Events.Channel.SuspiciousUser;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.EventSub.Notifications.Channel.Chat;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.SuspiciousUser;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelSuspiciousUserMessage"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelsuspicious_usermessage">Channel Suspicious User Message</see> for more information.
/// </remarks>
public record ChannelSuspiciousUserMessageNotification : EventSubNotification<ChannelSuspiciousUserMessageEvent, ChannelSuspiciousUserMessageCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelSuspiciousUserMessage"/>.
/// </summary>
public record ChannelSuspiciousUserMessageCondition : BroadcasterModeratorCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelSuspiciousUserMessage"/> event.
/// </summary>
public record ChannelSuspiciousUserMessageEvent : IHaveSuspiciousUser, IHaveBroadcaster
{
    /// <summary>
    /// The user id of the broadcaster (channel) in whose chat the suspicious user event occurred.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) in whose chat the suspicious user event occurred.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) in whose chat the suspicious user event occurred.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The user id of the suspicious user.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The display name of the suspicious user.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The login (username) of the suspicious user.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The current status of the suspicious user as set by a moderator.
    /// </summary>
    public required SuspiciousUserStatus LowTrustStatus { get; init; }
    /// <summary>
    /// An array of broadcaster (channel) user ids that the broadcaster is sharing bans with where the suspicious user is also banned.
    /// </summary>
    public required string[] SharedBanChannelIds { get; init; }
    /// <summary>
    /// The suspicious user types that apply to the suspicious user.
    /// </summary>
    public required ChannelSuspiciousUserType[] Types { get; init; }
    /// <summary>
    /// An evaluation of the likelihood the suspicious user is evading a ban on the broadcaster's channel.
    /// </summary>
    public required SuspiciousUserBanEvasionEvaluationLevel BanEvasionEvaluation { get; init; } // May be nullable, not clear in spec.
    /// <summary>
    /// The chat message sent by the suspicious user.
    /// </summary>
    public required SuspiciousUserChatMessage Message { get; init; }
}
