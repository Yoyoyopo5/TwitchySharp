﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace TwitchySharp.Api.Helix.EventSub;
/// <summary>
/// Contains name constants for each EventSub subscription type.
/// </summary>
internal static class EventSubSubscriptionTypeNames
{
    /// <summary>
    /// An array of all EventSub type names generated by reflection.
    /// </summary>
    public static string[] TypeNames { get; } = typeof(EventSubSubscriptionTypeNames)
        .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
        .Where(field => field.IsLiteral && !field.IsInitOnly && field.FieldType == typeof(string))
        .Select(field => field.GetRawConstantValue())
        .Cast<string>()
        .ToArray();

    public const string AUTOMOD_MESSAGE_HOLD = "automod.message.hold";
    public const string AUTOMOD_MESSAGE_UPDATE = "automod.message.update";
    public const string AUTOMOD_SETTINGS_UPDATE = "automod.settings.update";
    public const string AUTOMOD_TERMS_UPDATE = "automod.terms.update";
    public const string CHANNEL_UPDATE = "channel.update";
    public const string CHANNEL_FOLLOW = "channel.follow";
    public const string CHANNEL_AD_BREAK_BEGIN = "channel.ad_break.begin";
    public const string CHANNEL_CHAT_CLEAR = "channel.chat.clear";
    public const string CHANNEL_CHAT_CLEAR_USER_MESSAGES = "channel.chat.clear_user_messages";
    public const string CHANNEL_CHAT_MESSAGE = "channel.chat.message";
    public const string CHANNEL_CHAT_MESSAGE_DELETE = "channel.chat.message_delete";
    public const string CHANNEL_CHAT_NOTIFICATION = "channel.chat.notification";
    public const string CHANNEL_CHAT_SETTINGS_UPDATE = "channel.chat_settings.update";
    public const string CHANNEL_CHAT_USER_MESSAGE_HOLD = "channel.chat.user_message_hold";
    public const string CHANNEL_CHAT_USER_MESSAGE_UPDATE = "channel.chat.user_message_update";
    public const string CHANNEL_SHARED_CHAT_SESSION_BEGIN = "channel.shared_chat.begin";
    public const string CHANNEL_SHARED_CHAT_SESSION_UPDATE = "channel.shared_chat.update";
    public const string CHANNEL_SHARED_CHAT_SESSION_END = "channel.shared_chat.end";
    public const string CHANNEL_SUBSCRIBE = "channel.subscribe";
    public const string CHANNEL_SUBSCRIPTION_END = "channel.subscription.end";
    public const string CHANNEL_SUBSCRIPTION_GIFT = "channel.subscription.gift";
    public const string CHANNEL_SUBSCRIPTION_MESSAGE = "channel.subscription.message";
    public const string CHANNEL_CHEER = "channel.cheer";
    public const string CHANNEL_RAID = "channel.raid";
    public const string CHANNEL_BAN = "channel.ban";
    public const string CHANNEL_UNBAN = "channel.unban";
    public const string CHANNEL_UNBAN_REQUEST_CREATE = "channel.unban_request.create";
    public const string CHANNEL_UNBAN_REQUEST_RESOLVE = "channel.unban_request.resolve";
    public const string CHANNEL_MODERATE = "channel.moderate";
    public const string CHANNEL_MODERATOR_ADD = "channel.moderator.add";
    public const string CHANNEL_MODERATOR_REMOVE = "channel.moderator.remove";
    public const string CHANNEL_GUEST_STAR_SESSION_BEGIN = "channel.guest_star_session.begin";
    public const string CHANNEL_GUEST_STAR_SESSION_END = "channel.guest_star_session.end";
    public const string CHANNEL_GUEST_STAR_GUEST_UPDATE = "channel.guest_star_guest.update";
    public const string CHANNEL_GUEST_STAR_SETTINGS_UPDATE = "channel.guest_star_settings.update";
    public const string CHANNEL_POINTS_AUTOMATIC_REWARD_REDEMPTION = "channel.channel_points_automatic_reward_redemption.add";
    public const string CHANNEL_POINTS_CUSTOM_REWARD_ADD = "channel.channel_points_custom_reward.add";
    public const string CHANNEL_POINTS_CUSTOM_REWARD_UPDATE = "channel.channel_points_custom_reward.update";
    public const string CHANNEL_POINTS_CUSTOM_REWARD_REMOVE = "channel.channel_points_custom_reward.remove";
    public const string CHANNEL_POINTS_CUSTOM_REWARD_REDEMPTION_ADD = "channel.channel_points_custom_reward_redemption.add";
    public const string CHANNEL_POINTS_CUSTOM_REWARD_REDEMPTION_UPDATE = "channel.channel_points_custom_reward_redemption.update";
    public const string CHANNEL_POLL_BEGIN = "channel.poll.begin";
    public const string CHANNEL_POLL_PROGRESS = "channel.poll.progress";
    public const string CHANNEL_POLL_END = "channel.poll.end";
    public const string CHANNEL_PREDICTION_BEGIN = "channel.prediction.begin";
    public const string CHANNEL_PREDICTION_PROGRESS = "channel.prediction.progress";
    public const string CHANNEL_PREDICTION_LOCK = "channel.prediction.lock";
    public const string CHANNEL_PREDICTION_END = "channel.prediction.end";
    public const string CHANNEL_SUSPICIOUS_USER_MESSAGE = "channel.suspicious_user.message";
    public const string CHANNEL_SUSPICIOUS_USER_UPDATE = "channel.suspicious_user.update";
    public const string CHANNEL_VIP_ADD = "channel.vip.add";
    public const string CHANNEL_VIP_REMOVE = "channel.vip.remove";
    public const string CHANNEL_WARNING_ACKNOWLEDGEMENT = "channel.warning.acknowledge";
    public const string CHANNEL_WARNING_SEND = "channel.warning.send";
    public const string CHARITY_DONATION = "channel.charity_campaign.donate";
    public const string CHARITY_CAMPAIGN_START = "channel.charity_campaign.start";
    public const string CHARITY_CAMPAIGN_PROGRESS = "channel.charity_campaign.progress";
    public const string CHARITY_CAMPAIGN_STOP = "channel.charity_campaign.stop";
    public const string CONDUIT_SHARD_DISABLED = "conduit.shard.disabled";
    public const string DROP_ENTITLEMENT_GRANT = "drop.entitlement.grant";
    public const string EXTENSION_BITS_TRANSACTION_CREATE = "extension.bits_transaction.create";
    public const string GOAL_BEGIN = "channel.goal.begin";
    public const string GOAL_PROGRESS = "channel.goal.progress";
    public const string GOAL_END = "channel.goal.end";
    public const string HYPE_TRAIN_BEGIN = "channel.hype_train.begin";
    public const string HYPE_TRAIN_PROGRESS = "channel.hype_train.progress";
    public const string HYPE_TRAIN_END = "channel.hype_train.end";
    public const string SHIELD_MODE_BEGIN = "channel.shield_mode.begin";
    public const string SHIELD_MODE_END = "channel.shield_mode.end";
    public const string SHOUTOUT_CREATE = "channel.shoutout.create";
    public const string SHOUTOUT_RECEIVED = "channel.shoutout.receive";
    public const string STREAM_ONLINE = "stream.online";
    public const string STREAM_OFFLINE = "stream.offline";
    public const string USER_AUTHORIZATION_GRANT = "user.authorization.grant";
    public const string USER_AUTHORIZATION_REVOKE = "user.authorization.revoke";
    public const string USER_UPDATE = "user.update";
    public const string WHISPER_RECEIVED = "user.whisper.message";
}
