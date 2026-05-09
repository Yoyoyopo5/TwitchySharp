using TwitchySharp.Api.Helix.EventSub.Subscriptions;

namespace TwitchySharp.Api.Helix.EventSub;

/// <summary>
/// Contains a method mapping <see cref="EventSubSubscriptionType"/> references to the specific <see cref="ConditionKey"/>
/// for the if of the user that must be authorized for the subscription.
/// </summary>
/// <remarks>
/// This is used in <see cref="DeleteEventSubSubscriptionRequest"/> to map <see cref="EventSubSubscription"/> objects returned
/// from the API to a specific <see cref="TwitchIdentity.User"/> for request authorization.
/// </remarks>
public static class AuthorizingUserConditionKeys
{
    private static ConditionKey Get<T>()
        where T : IUserAuthorizedSubscriptionTypeSpecification
        => T.AuthorizingUserConditionKey;

    /// <summary>
    /// The mapping from <see cref="EventSubSubscriptionType"/> to <see cref="ConditionKey"/> that identifies
    /// which condition key corresponds to the id of the user that must authorize subscription delete requests.
    /// </summary>
    public static ConditionKey? GetAuthorizingUserKey(this EventSubSubscriptionType subscriptionType)
        => subscriptionType.Type switch
        {
            // --- Automod ---
            EventSubSubscriptionTypeNames.AUTOMOD_MESSAGE_HOLD => subscriptionType.Version switch
            {
                EventSubSubscriptionTypeVersions.V1 => Get<AutomodMessageHold>(),
                EventSubSubscriptionTypeVersions.V2 => Get<AutomodMessageHoldV2>(),
                _ => null
            },
            EventSubSubscriptionTypeNames.AUTOMOD_MESSAGE_UPDATE => subscriptionType.Version switch
            {
                EventSubSubscriptionTypeVersions.V1 => Get<AutomodMessageUpdate>(),
                EventSubSubscriptionTypeVersions.V2 => Get<AutomodMessageUpdateV2>(),
                _ => null
            },
            EventSubSubscriptionTypeNames.AUTOMOD_SETTINGS_UPDATE => Get<AutomodSettingsUpdate>(),
            EventSubSubscriptionTypeNames.AUTOMOD_TERMS_UPDATE => Get<AutomodTermsUpdate>(),

            // --- Channel ---
            EventSubSubscriptionTypeNames.CHANNEL_BAN => Get<ChannelBan>(),
            EventSubSubscriptionTypeNames.CHANNEL_BITS_USE => Get<ChannelBitsUse>(),
            EventSubSubscriptionTypeNames.CHANNEL_CHEER => Get<ChannelCheer>(),
            EventSubSubscriptionTypeNames.CHANNEL_FOLLOW => Get<ChannelFollow>(),
            EventSubSubscriptionTypeNames.CHANNEL_MODERATE => subscriptionType.Version switch
            {
                EventSubSubscriptionTypeVersions.V1 => Get<ChannelModerate>(),
                EventSubSubscriptionTypeVersions.V2 => Get<ChannelModerateV2>(),
                _ => null
            },
            EventSubSubscriptionTypeNames.CHANNEL_SUBSCRIBE => Get<ChannelSubscribe>(),
            EventSubSubscriptionTypeNames.CHANNEL_UNBAN => Get<ChannelUnban>(),
            // No auth required
            // EventSubSubscriptionTypeNames.CHANNEL_UPDATE => Get<ChannelUpdate>(),
            // EventSubSubscriptionTypeNames.CHANNEL_RAID => Get<ChannelRaid>(),

            // AdBreak
            EventSubSubscriptionTypeNames.CHANNEL_AD_BREAK_BEGIN => Get<ChannelAdBreakBegin>(),

            // Channel Points
            EventSubSubscriptionTypeNames.CHANNEL_POINTS_AUTOMATIC_REWARD_REDEMPTION_ADD => subscriptionType.Version switch
            {
                EventSubSubscriptionTypeVersions.V1 => Get<ChannelPointsAutomaticRewardRedemptionAdd>(),
                EventSubSubscriptionTypeVersions.V2 => Get<ChannelPointsAutomaticRewardRedemptionAddV2>(),
                _ => null
            },
            EventSubSubscriptionTypeNames.CHANNEL_POINTS_CUSTOM_REWARD_ADD => Get<ChannelPointsCustomRewardAdd>(),
            EventSubSubscriptionTypeNames.CHANNEL_POINTS_CUSTOM_REWARD_REDEMPTION_ADD => Get<ChannelPointsCustomRewardRedemptionAdd>(),
            EventSubSubscriptionTypeNames.CHANNEL_POINTS_CUSTOM_REWARD_REDEMPTION_UPDATE => Get<ChannelPointsCustomRewardRedemptionUpdate>(),
            EventSubSubscriptionTypeNames.CHANNEL_POINTS_CUSTOM_REWARD_REMOVE => Get<ChannelPointsCustomRewardRemove>(),
            EventSubSubscriptionTypeNames.CHANNEL_POINTS_CUSTOM_REWARD_UPDATE => Get<ChannelPointsCustomRewardUpdate>(),

            // Charity
            // No auth required
            // EventSubSubscriptionTypeNames.CHANNEL_CHARITY_CAMPAIGN_PROGRESS => Get<CharityCampaignProgress>(),
            // EventSubSubscriptionTypeNames.CHANNEL_CHARITY_CAMPAIGN_START => Get<CharityCampaignStart>(),
            // EventSubSubscriptionTypeNames.CHANNEL_CHARITY_CAMPAIGN_STOP => Get<CharityCampaignStop>(),
            // EventSubSubscriptionTypeNames.CHANNEL_CHARITY_DONATION => Get<CharityDonation>(),

            // Chat
            EventSubSubscriptionTypeNames.CHANNEL_CHAT_CLEAR => Get<ChannelChatClear>(),
            EventSubSubscriptionTypeNames.CHANNEL_CHAT_CLEAR_USER_MESSAGES => Get<ChannelChatClearUserMessages>(),
            EventSubSubscriptionTypeNames.CHANNEL_CHAT_MESSAGE => Get<ChannelChatMessage>(),
            EventSubSubscriptionTypeNames.CHANNEL_CHAT_MESSAGE_DELETE => Get<ChannelChatMessageDelete>(),
            EventSubSubscriptionTypeNames.CHANNEL_CHAT_NOTIFICATION => Get<ChannelChatNotification>(),
            EventSubSubscriptionTypeNames.CHANNEL_CHAT_SETTINGS_UPDATE => Get<ChannelChatSettingsUpdate>(),
            EventSubSubscriptionTypeNames.CHANNEL_CHAT_USER_MESSAGE_HOLD => Get<ChannelChatUserMessageHold>(),
            EventSubSubscriptionTypeNames.CHANNEL_CHAT_USER_MESSAGE_UPDATE => Get<ChannelChatUserMessageUpdate>(),

            // Goals
            EventSubSubscriptionTypeNames.GOAL_BEGIN => Get<GoalBegin>(),
            EventSubSubscriptionTypeNames.GOAL_END => Get<GoalEnd>(),
            EventSubSubscriptionTypeNames.GOAL_PROGRESS => Get<GoalProgress>(),

            // GuestStar
            EventSubSubscriptionTypeNames.CHANNEL_GUEST_STAR_GUEST_UPDATE => Get<ChannelGuestStarGuestUpdate>(),
            EventSubSubscriptionTypeNames.CHANNEL_GUEST_STAR_SESSION_BEGIN => Get<ChannelGuestStarSessionBegin>(),
            EventSubSubscriptionTypeNames.CHANNEL_GUEST_STAR_SESSION_END => Get<ChannelGuestStarSessionEnd>(),
            EventSubSubscriptionTypeNames.CHANNEL_GUEST_STAR_SETTINGS_UPDATE => Get<ChannelGuestStarSettingsUpdate>(),

            // HypeTrain
            EventSubSubscriptionTypeNames.HYPE_TRAIN_BEGIN => Get<HypeTrainBeginV2>(),
            EventSubSubscriptionTypeNames.HYPE_TRAIN_END => Get<HypeTrainEndV2>(),
            EventSubSubscriptionTypeNames.HYPE_TRAIN_PROGRESS => Get<HypeTrainProgressV2>(),

            // Moderator
            EventSubSubscriptionTypeNames.CHANNEL_MODERATOR_ADD => Get<ChannelModeratorAdd>(),
            EventSubSubscriptionTypeNames.CHANNEL_MODERATOR_REMOVE => Get<ChannelModeratorRemove>(),

            // Polls
            EventSubSubscriptionTypeNames.CHANNEL_POLL_BEGIN => Get<ChannelPollBegin>(),
            EventSubSubscriptionTypeNames.CHANNEL_POLL_END => Get<ChannelPollEnd>(),
            EventSubSubscriptionTypeNames.CHANNEL_POLL_PROGRESS => Get<ChannelPollProgress>(),

            // Predictions
            EventSubSubscriptionTypeNames.CHANNEL_PREDICTION_BEGIN => Get<ChannelPredictionBegin>(),
            EventSubSubscriptionTypeNames.CHANNEL_PREDICTION_END => Get<ChannelPredictionEnd>(),
            EventSubSubscriptionTypeNames.CHANNEL_PREDICTION_LOCK => Get<ChannelPredictionLock>(),
            EventSubSubscriptionTypeNames.CHANNEL_PREDICTION_PROGRESS => Get<ChannelPredictionProgress>(),

            // ShieldMode
            EventSubSubscriptionTypeNames.SHIELD_MODE_BEGIN => Get<ShieldModeBegin>(),
            EventSubSubscriptionTypeNames.SHIELD_MODE_END => Get<ShieldModeEnd>(),

            // Shoutout
            EventSubSubscriptionTypeNames.SHOUTOUT_CREATE => Get<ShoutoutCreate>(),
            EventSubSubscriptionTypeNames.SHOUTOUT_RECEIVED => Get<ShoutoutReceived>(),

            // SharedChat
            // No auth required
            // EventSubSubscriptionTypeNames.CHANNEL_SHARED_CHAT_SESSION_BEGIN => Get<ChannelSharedChatSessionBegin>(),
            // EventSubSubscriptionTypeNames.CHANNEL_SHARED_CHAT_SESSION_END => Get<ChannelSharedChatSessionEnd>(),
            // EventSubSubscriptionTypeNames.CHANNEL_SHARED_CHAT_SESSION_UPDATE => Get<ChannelSharedChatSessionUpdate>(),

            // Subscription
            EventSubSubscriptionTypeNames.CHANNEL_SUBSCRIPTION_END => Get<ChannelSubscriptionEnd>(),
            EventSubSubscriptionTypeNames.CHANNEL_SUBSCRIPTION_GIFT => Get<ChannelSubscriptionGift>(),
            EventSubSubscriptionTypeNames.CHANNEL_SUBSCRIPTION_MESSAGE => Get<ChannelSubscriptionMessage>(),

            // SuspiciousUser
            EventSubSubscriptionTypeNames.CHANNEL_SUSPICIOUS_USER_MESSAGE => Get<ChannelSuspiciousUserMessage>(),
            EventSubSubscriptionTypeNames.CHANNEL_SUSPICIOUS_USER_UPDATE => Get<ChannelSuspiciousUserUpdate>(),

            // UnbanRequest
            EventSubSubscriptionTypeNames.CHANNEL_UNBAN_REQUEST_CREATE => Get<ChannelUnbanRequestCreate>(),
            EventSubSubscriptionTypeNames.CHANNEL_UNBAN_REQUEST_RESOLVE => Get<ChannelUnbanRequestResolve>(),

            // VIP
            EventSubSubscriptionTypeNames.CHANNEL_VIP_ADD => Get<ChannelVipAdd>(),
            EventSubSubscriptionTypeNames.CHANNEL_VIP_REMOVE => Get<ChannelVipRemove>(),

            // Warning
            EventSubSubscriptionTypeNames.CHANNEL_WARNING_ACKNOWLEDGEMENT => Get<ChannelWarningAcknowledgement>(),
            EventSubSubscriptionTypeNames.CHANNEL_WARNING_SEND => Get<ChannelWarningSend>(),

            // --- Extension ---
            // Not supported
            // EventSubSubscriptionTypeNames.EXTENSION_BITS_TRANSACTION_CREATE => Get<ExtensionBitsTransactionCreate>(),

            // --- Drops ---
            // Not supported
            // EventSubSubscriptionTypeNames.DROP_ENTITLEMENT_GRANT => Get<DropEntitlementGrant>(),

            // --- Conduit ---
            // No auth required
            // EventSubSubscriptionTypeNames.CONDUIT_SHARD_DISABLED => Get<ConduitShardDisabled>(),

            // --- Stream ---
            // No auth required
            // EventSubSubscriptionTypeNames.STREAM_OFFLINE => Get<StreamOffline>(),
            // EventSubSubscriptionTypeNames.STREAM_ONLINE => Get<StreamOnline>(),

            // --- User ---
            EventSubSubscriptionTypeNames.WHISPER_RECEIVED => Get<WhisperReceived>(),
            // No auth required
            // EventSubSubscriptionTypeNames.USER_UPDATE => Get<UserUpdate>(),
            // Not supported
            // EventSubSubscriptionTypeNames.USER_AUTHORIZATION_GRANT => Get<UserAuthorizationGrant>(),
            // EventSubSubscriptionTypeNames.USER_AUTHORIZATION_REVOKE => Get<UserAuthorizationRevoke>(),

            _ => null
        };
}
