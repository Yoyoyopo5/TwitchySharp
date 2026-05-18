using System.Text.Json;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.EventSub.Models.Notifications.Automod.Message;
using TwitchySharp.EventSub.Models.Notifications.Automod.Settings;
using TwitchySharp.EventSub.Models.Notifications.Automod.Terms;
using TwitchySharp.EventSub.Models.Notifications.Channel;
using TwitchySharp.EventSub.Models.Notifications.Channel.AdBreak;
using TwitchySharp.EventSub.Models.Notifications.Channel.Bits;
using TwitchySharp.EventSub.Models.Notifications.Channel.ChannelPoints;
using TwitchySharp.EventSub.Models.Notifications.Channel.CharityCampaign;
using TwitchySharp.EventSub.Models.Notifications.Channel.Chat;
using TwitchySharp.EventSub.Models.Notifications.Channel.ChatSettings;
using TwitchySharp.EventSub.Models.Notifications.Channel.Goals;
using TwitchySharp.EventSub.Models.Notifications.Channel.GuestStar;
using TwitchySharp.EventSub.Models.Notifications.Channel.HypeTrain;
using TwitchySharp.EventSub.Models.Notifications.Channel.Moderator;
using TwitchySharp.EventSub.Models.Notifications.Channel.Polls;
using TwitchySharp.EventSub.Models.Notifications.Channel.Predictions;
using TwitchySharp.EventSub.Models.Notifications.Channel.SharedChat;
using TwitchySharp.EventSub.Models.Notifications.Channel.ShieldMode;
using TwitchySharp.EventSub.Models.Notifications.Channel.Shoutout;
using TwitchySharp.EventSub.Models.Notifications.Channel.Subscription;
using TwitchySharp.EventSub.Models.Notifications.Channel.SuspiciousUser;
using TwitchySharp.EventSub.Models.Notifications.Channel.UnbanRequest;
using TwitchySharp.EventSub.Models.Notifications.Channel.Vip;
using TwitchySharp.EventSub.Models.Notifications.Channel.Warning;
using TwitchySharp.EventSub.Models.Notifications.Conduit;
using TwitchySharp.EventSub.Models.Notifications.Drops;
using TwitchySharp.EventSub.Models.Notifications.Extension;
using TwitchySharp.EventSub.Models.Notifications.Stream;
using TwitchySharp.EventSub.Models.Notifications.User;
using TwitchySharp.EventSub.Models.Notifications.User.Authorization;
using TwitchySharp.EventSub.Models.Notifications.User.Whisper;
using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Serialization;

namespace TwitchySharp.EventSub;

public delegate ValueTask<Validation<IEventSubNotification>> DeserializeNotification(NotificationPayloadStream payload, CancellationToken ct);

/// <summary>
/// Contains static methods for polymorphically deserializing JSON EventSub notifications.
/// </summary>
public static class NotificationDeserializer
{
    /// <summary>
    /// Contains an error message and exception (if applicable) regarding notification deserialization failures.
    /// </summary>
    /// <param name="Message">The error message.</param>
    /// <param name="Exception">The exception associated with the error, if any</param>
    public record NotificationDeserializerError(string Message, Exception? Exception = null) : Error(Message);

    /// <summary>
    /// Create an EventSub notification deserializer function with the given <paramref name="map"/> and <paramref name="serializerOptions"/>.
    /// </summary>
    /// <param name="map">
    /// The subscription type map to use.
    /// Uses the output of <see cref="CreateDefaultMap"/> if left <see langword="null"/>.
    /// You can use this parameter to define your own deserialization logic or (more commonly) extend the default set of subscription types supported by the default map.
    /// </param>
    /// <param name="serializerOptions">The serializer options to use. Defaults to <see cref="JsonConfig.ApiOptions"/> if left <see langword="null"/>.</param>
    /// <returns></returns>
    public static DeserializeNotification CreateDeserializer(
        Func<EventSubSubscriptionType, Func<JsonSerializerOptions, JsonDocument, IEventSubNotification>>? map = null,
        JsonSerializerOptions? serializerOptions = null
        )
    {
        serializerOptions ??= JsonConfig.ApiOptions;
        if (serializerOptions.GetConverter(typeof(IEventSubNotification)) is not NotificationConverter)
            serializerOptions.Converters.Add(new NotificationConverter(map ?? CreateDefaultMap()));

        return (payload, ct) => Deserialize(payload, serializerOptions, ct);
    }

    private async static ValueTask<Validation<IEventSubNotification>> Deserialize(
        this NotificationPayloadStream payload,
        JsonSerializerOptions? options = null,
        CancellationToken ct = default
        )
    {
        try
        {
            return await JsonSerializer.DeserializeAsync<IEventSubNotification>(payload, options, ct) is { } notification
                ? new Validation<IEventSubNotification>(notification)
                : new NotificationDeserializerError("The notification was null.");
        }
        catch(Exception ex)
        {
            return new NotificationDeserializerError(ex.Message, ex);
        }
    }

    private static Func<JsonSerializerOptions, JsonDocument, T?> CreateMapDeserializer<T>()
        where T : IEventSubNotification
        => (options, document) => JsonSerializer.Deserialize<T>(document, options);

    /// <summary>
    /// Creates the default deserializer map for EventSub notification types.
    /// </summary>
    /// <remarks>
    /// You may need to use the output of this method if you want to extend the default subscription type list (e.g. if a specific subscription type is not yet implemented by default). 
    /// </remarks>
    /// <returns>A function mapping <see cref="EventSubSubscriptionType"/> to a specific deserialization function returning <see cref="IEventSubNotification"/> for that subscription type.</returns>
    public static Func<EventSubSubscriptionType, Func<JsonSerializerOptions, JsonDocument, IEventSubNotification?>?> CreateDefaultMap()
        // We could potentially source generate this if maintenance becomes an issue.
        => subscriptionType => subscriptionType.Type switch
        {
            // --- Automod ---
            EventSubSubscriptionTypeNames.AUTOMOD_MESSAGE_HOLD => subscriptionType.Version switch
            {
                EventSubSubscriptionTypeVersions.V1 => CreateMapDeserializer<AutomodMessageHoldNotification>(),
                EventSubSubscriptionTypeVersions.V2 => CreateMapDeserializer<AutomodMessageHoldV2Notification>(),
                _ => null
            },
            EventSubSubscriptionTypeNames.AUTOMOD_MESSAGE_UPDATE => subscriptionType.Version switch
            {
                EventSubSubscriptionTypeVersions.V1 => CreateMapDeserializer<AutomodMessageUpdateNotification>(),
                EventSubSubscriptionTypeVersions.V2 => CreateMapDeserializer<AutomodMessageUpdateV2Notification>(),
                _ => null
            },
            EventSubSubscriptionTypeNames.AUTOMOD_SETTINGS_UPDATE => CreateMapDeserializer<AutomodSettingsUpdateNotification>(),
            EventSubSubscriptionTypeNames.AUTOMOD_TERMS_UPDATE => CreateMapDeserializer<AutomodTermsUpdateNotification>(),

            // --- Channel ---
            EventSubSubscriptionTypeNames.CHANNEL_BAN => CreateMapDeserializer<ChannelBanNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_CHEER => CreateMapDeserializer<ChannelCheerNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_FOLLOW => CreateMapDeserializer<ChannelFollowNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_MODERATE => subscriptionType.Version switch
            {
                EventSubSubscriptionTypeVersions.V1 => CreateMapDeserializer<ChannelModerateNotification>(),
                EventSubSubscriptionTypeVersions.V2 => CreateMapDeserializer<ChannelModerateV2Notification>(),
                _ => null
            },
            EventSubSubscriptionTypeNames.CHANNEL_RAID => CreateMapDeserializer<ChannelRaidNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_SUBSCRIBE => CreateMapDeserializer<ChannelSubscribeNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_UNBAN => CreateMapDeserializer<ChannelUnbanNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_UPDATE => CreateMapDeserializer<ChannelUpdateNotification>(),

            // AdBreak
            EventSubSubscriptionTypeNames.CHANNEL_AD_BREAK_BEGIN => CreateMapDeserializer<ChannelAdBreakBeginNotification>(),

            // Bits
            EventSubSubscriptionTypeNames.CHANNEL_BITS_USE => CreateMapDeserializer<ChannelBitsUseNotification>(),

            // ChannelPoints
            EventSubSubscriptionTypeNames.CHANNEL_POINTS_AUTOMATIC_REWARD_REDEMPTION_ADD => subscriptionType.Version switch
            {
                EventSubSubscriptionTypeVersions.V1 => CreateMapDeserializer<ChannelPointsAutomaticRewardRedemptionAddNotification>(),
                EventSubSubscriptionTypeVersions.V2 => CreateMapDeserializer<ChannelPointsAutomaticRewardRedemptionAddV2Notification>(),
                _ => null
            },
            EventSubSubscriptionTypeNames.CHANNEL_POINTS_CUSTOM_REWARD_ADD => CreateMapDeserializer<ChannelPointsCustomRewardAddNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_POINTS_CUSTOM_REWARD_UPDATE => CreateMapDeserializer<ChannelPointsCustomRewardUpdateNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_POINTS_CUSTOM_REWARD_REMOVE => CreateMapDeserializer<ChannelPointsCustomRewardRemoveNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_POINTS_CUSTOM_REWARD_REDEMPTION_ADD => CreateMapDeserializer<ChannelPointsCustomRewardRedemptionAddNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_POINTS_CUSTOM_REWARD_REDEMPTION_UPDATE => CreateMapDeserializer<ChannelPointsCustomRewardRedemptionUpdateNotification>(),

            // CharityCampaign
            EventSubSubscriptionTypeNames.CHARITY_DONATION => CreateMapDeserializer<CharityDonationNotification>(),
            EventSubSubscriptionTypeNames.CHARITY_CAMPAIGN_START => CreateMapDeserializer<CharityCampaignStartNotification>(),
            EventSubSubscriptionTypeNames.CHARITY_CAMPAIGN_PROGRESS => CreateMapDeserializer<CharityCampaignProgressNotification>(),
            EventSubSubscriptionTypeNames.CHARITY_CAMPAIGN_STOP => CreateMapDeserializer<CharityCampaignStopNotification>(),

            // Chat
            EventSubSubscriptionTypeNames.CHANNEL_CHAT_CLEAR => CreateMapDeserializer<ChannelChatClearNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_CHAT_CLEAR_USER_MESSAGES => CreateMapDeserializer<ChannelChatClearUserMessagesNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_CHAT_MESSAGE => CreateMapDeserializer<ChannelChatMessageNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_CHAT_MESSAGE_DELETE => CreateMapDeserializer<ChannelChatMessageDeleteNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_CHAT_NOTIFICATION => CreateMapDeserializer<ChannelChatNotificationNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_CHAT_USER_MESSAGE_HOLD => CreateMapDeserializer<ChannelChatUserMessageHoldNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_CHAT_USER_MESSAGE_UPDATE => CreateMapDeserializer<ChannelChatUserMessageUpdateNotification>(),

            // ChatSettings
            EventSubSubscriptionTypeNames.CHANNEL_CHAT_SETTINGS_UPDATE => CreateMapDeserializer<ChannelChatSettingsUpdateNotification>(),

            // Goals
            EventSubSubscriptionTypeNames.GOAL_BEGIN => CreateMapDeserializer<GoalBeginNotification>(),
            EventSubSubscriptionTypeNames.GOAL_PROGRESS => CreateMapDeserializer<GoalProgressNotification>(),
            EventSubSubscriptionTypeNames.GOAL_END => CreateMapDeserializer<GoalEndNotification>(),

            // GuestStar
            EventSubSubscriptionTypeNames.CHANNEL_GUEST_STAR_SESSION_BEGIN => CreateMapDeserializer<ChannelGuestStarSessionBeginNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_GUEST_STAR_SESSION_END => CreateMapDeserializer<ChannelGuestStarSessionEndNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_GUEST_STAR_GUEST_UPDATE => CreateMapDeserializer<ChannelGuestStarGuestUpdateNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_GUEST_STAR_SETTINGS_UPDATE => CreateMapDeserializer<ChannelGuestStarSettingsUpdateNotification>(),

            // HypeTrain
            EventSubSubscriptionTypeNames.HYPE_TRAIN_BEGIN => CreateMapDeserializer<HypeTrainBeginV2Notification>(),
            EventSubSubscriptionTypeNames.HYPE_TRAIN_PROGRESS => CreateMapDeserializer<HypeTrainProgressV2Notification>(),
            EventSubSubscriptionTypeNames.HYPE_TRAIN_END => CreateMapDeserializer<HypeTrainEndV2Notification>(),

            // Moderator
            EventSubSubscriptionTypeNames.CHANNEL_MODERATOR_ADD => CreateMapDeserializer<ChannelModeratorAddNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_MODERATOR_REMOVE => CreateMapDeserializer<ChannelModeratorRemoveNotification>(),

            // Polls
            EventSubSubscriptionTypeNames.CHANNEL_POLL_BEGIN => CreateMapDeserializer<ChannelPollBeginNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_POLL_PROGRESS => CreateMapDeserializer<ChannelPollProgressNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_POLL_END => CreateMapDeserializer<ChannelPollEndNotification>(),

            // Predictions
            EventSubSubscriptionTypeNames.CHANNEL_PREDICTION_BEGIN => CreateMapDeserializer<ChannelPredictionBeginNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_PREDICTION_PROGRESS => CreateMapDeserializer<ChannelPredictionProgressNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_PREDICTION_LOCK => CreateMapDeserializer<ChannelPredictionLockNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_PREDICTION_END => CreateMapDeserializer<ChannelPredictionEndNotification>(),

            // SharedChat
            EventSubSubscriptionTypeNames.CHANNEL_SHARED_CHAT_SESSION_BEGIN => CreateMapDeserializer<ChannelSharedChatBeginNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_SHARED_CHAT_SESSION_UPDATE => CreateMapDeserializer<ChannelSharedChatUpdateNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_SHARED_CHAT_SESSION_END => CreateMapDeserializer<ChannelSharedChatEndNotification>(),

            // ShieldMode
            EventSubSubscriptionTypeNames.SHIELD_MODE_BEGIN => CreateMapDeserializer<ShieldModeBeginNotification>(),
            EventSubSubscriptionTypeNames.SHIELD_MODE_END => CreateMapDeserializer<ShieldModeEndNotification>(),

            // Shoutout
            EventSubSubscriptionTypeNames.SHOUTOUT_CREATE => CreateMapDeserializer<ShoutoutCreateNotification>(),
            EventSubSubscriptionTypeNames.SHOUTOUT_RECEIVED => CreateMapDeserializer<ShoutoutReceivedNotification>(),

            // Subscription
            EventSubSubscriptionTypeNames.CHANNEL_SUBSCRIPTION_END => CreateMapDeserializer<ChannelSubscriptionEndNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_SUBSCRIPTION_GIFT => CreateMapDeserializer<ChannelSubscriptionGiftNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_SUBSCRIPTION_MESSAGE => CreateMapDeserializer<ChannelSubscriptionMessageNotification>(),

            // SuspiciousUser
            EventSubSubscriptionTypeNames.CHANNEL_SUSPICIOUS_USER_MESSAGE => CreateMapDeserializer<ChannelSuspiciousUserMessageNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_SUSPICIOUS_USER_UPDATE => CreateMapDeserializer<ChannelSuspiciousUserUpdateNotification>(),

            // UnbanRequest
            EventSubSubscriptionTypeNames.CHANNEL_UNBAN_REQUEST_CREATE => CreateMapDeserializer<ChannelUnbanRequestCreateNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_UNBAN_REQUEST_RESOLVE => CreateMapDeserializer<ChannelUnbanRequestResolveNotification>(),

            // VIP
            EventSubSubscriptionTypeNames.CHANNEL_VIP_ADD => CreateMapDeserializer<ChannelVipAddNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_VIP_REMOVE => CreateMapDeserializer<ChannelVipRemoveNotification>(),

            // Warning
            EventSubSubscriptionTypeNames.CHANNEL_WARNING_ACKNOWLEDGEMENT => CreateMapDeserializer<ChannelWarningAcknowledgementNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_WARNING_SEND => CreateMapDeserializer<ChannelWarningSendNotification>(),

            // --- Conduit ---
            EventSubSubscriptionTypeNames.CONDUIT_SHARD_DISABLED => CreateMapDeserializer<ConduitShardDisabledNotification>(),

            // --- Drops ---
            EventSubSubscriptionTypeNames.DROP_ENTITLEMENT_GRANT => CreateMapDeserializer<DropEntitlementGrantNotification>(),

            // --- Extension ---
            EventSubSubscriptionTypeNames.EXTENSION_BITS_TRANSACTION_CREATE => CreateMapDeserializer<ExtensionBitsTransactionCreateNotification>(),

            // --- Stream ---
            EventSubSubscriptionTypeNames.STREAM_ONLINE => CreateMapDeserializer<StreamOnlineNotification>(),
            EventSubSubscriptionTypeNames.STREAM_OFFLINE => CreateMapDeserializer<StreamOfflineNotification>(),

            // --- User ---
            EventSubSubscriptionTypeNames.USER_AUTHORIZATION_GRANT => CreateMapDeserializer<UserAuthorizationGrantNotification>(),
            EventSubSubscriptionTypeNames.USER_AUTHORIZATION_REVOKE => CreateMapDeserializer<UserAuthorizationRevokeNotification>(),
            EventSubSubscriptionTypeNames.USER_UPDATE => CreateMapDeserializer<UserUpdateNotification>(),
            EventSubSubscriptionTypeNames.WHISPER_RECEIVED => CreateMapDeserializer<WhisperReceivedNotification>(),

            _ => null
        };
}
