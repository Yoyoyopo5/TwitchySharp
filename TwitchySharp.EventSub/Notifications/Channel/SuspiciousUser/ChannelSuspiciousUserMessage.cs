using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Notifications.Channel.Chat;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel.SuspiciousUser;
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
public record ChannelSuspiciousUserMessageEvent : ChannelSuspiciousUserEvent
{
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

/// <summary>
/// Contains static definitions for possible suspicious user types.
/// </summary>
/// <param name="Value">The string value of the suspicious user type.</param>
public record ChannelSuspiciousUserType(string Value) : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// The suspicious user was manually tagged by a moderator.
    /// </summary>
    public static ChannelSuspiciousUserType ManuallyAdded { get; } = new("manually_added");
    /// <summary>
    /// The suspicious user was marked by Twitch as a potential ban evader.
    /// </summary>
    public static ChannelSuspiciousUserType BanEvader { get; } = new("ban_evader");
    /// <summary>
    /// The suspicious user was banned in a channel sharing bans with the broadcaster.
    /// </summary>
    public static ChannelSuspiciousUserType BannedInSharedChannel { get; } = new("banned_in_shared_channel");
}

/// <summary>
/// Contains static definitions for possible ban evasion likelihoods for suspicious chat users.
/// </summary>
/// <param name="Value">The string value for the ban evasion evaluation.</param>
public record SuspiciousUserBanEvasionEvaluationLevel(string Value) : ValueBackedEnum<string>(Value)
{
    public static SuspiciousUserBanEvasionEvaluationLevel Unknown { get; } = new("unknown");
    public static SuspiciousUserBanEvasionEvaluationLevel Possible { get; } = new("possible");
    public static SuspiciousUserBanEvasionEvaluationLevel Likely { get; } = new("likely");
}

/// <summary>
/// Contains information about a specific chat message from a suspicious user.
/// </summary>
public record SuspiciousUserChatMessage : ChannelChatMessage
{
    /// <summary>
    /// The id of the message.
    /// </summary>
    public required string MessageId { get; init; }
}