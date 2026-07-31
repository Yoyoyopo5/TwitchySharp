using System.Text.Json;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Serialization;

namespace TwitchySharp.EventSub.Serialization;

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
        catch (Exception ex)
        {
            return new NotificationDeserializerError(ex.Message, ex);
        }
    }

    private readonly static Dictionary<EventSubSubscriptionType, Func<JsonSerializerOptions, JsonDocument, IEventSubNotification?>> _defaultMap
        = new Dictionary<EventSubSubscriptionType, Func<JsonSerializerOptions, JsonDocument, IEventSubNotification?>>()
            .Register<AutomodMessageHoldNotification>(EventSubSubscriptionType.AutomodMessageHold)
            .Register<AutomodMessageHoldV2Notification>(EventSubSubscriptionType.AutomodMessageHoldV2)
            .Register<AutomodMessageUpdateNotification>(EventSubSubscriptionType.AutomodMessageUpdate)
            .Register<AutomodMessageUpdateV2Notification>(EventSubSubscriptionType.AutomodMessageUpdateV2)
            .Register<AutomodSettingsUpdateNotification>(EventSubSubscriptionType.AutomodSettingsUpdate)
            .Register<AutomodTermsUpdateNotification>(EventSubSubscriptionType.AutomodTermsUpdate)
            .Register<ChannelBanNotification>(EventSubSubscriptionType.ChannelBan)
            .Register<ChannelCheerNotification>(EventSubSubscriptionType.ChannelCheer)
            .Register<ChannelFollowNotification>(EventSubSubscriptionType.ChannelFollow)
            .Register<ChannelModerateNotification>(EventSubSubscriptionType.ChannelModerate)
            .Register<ChannelModerateV2Notification>(EventSubSubscriptionType.ChannelModerateV2)
            .Register<ChannelRaidNotification>(EventSubSubscriptionType.ChannelRaid)
            .Register<ChannelSubscribeNotification>(EventSubSubscriptionType.ChannelSubscribe)
            .Register<ChannelUnbanNotification>(EventSubSubscriptionType.ChannelUnban)
            .Register<ChannelUpdateNotification>(EventSubSubscriptionType.ChannelUpdate)
            .Register<ChannelAdBreakBeginNotification>(EventSubSubscriptionType.ChannelAdBreakBegin)
            .Register<ChannelBitsUseNotification>(EventSubSubscriptionType.ChannelBitsUse)
            .Register<ChannelPointsAutomaticRewardRedemptionAddNotification>(EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAdd)
            .Register<ChannelPointsAutomaticRewardRedemptionAddV2Notification>(EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAddV2)
            .Register<ChannelPointsCustomRewardAddNotification>(EventSubSubscriptionType.ChannelPointsCustomRewardAdd)
            .Register<ChannelPointsCustomRewardUpdateNotification>(EventSubSubscriptionType.ChannelPointsCustomRewardUpdate)
            .Register<ChannelPointsCustomRewardRemoveNotification>(EventSubSubscriptionType.ChannelPointsCustomRewardRemove)
            .Register<ChannelPointsCustomRewardRedemptionAddNotification>(EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionAdd)
            .Register<ChannelPointsCustomRewardRedemptionUpdateNotification>(EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionUpdate)
            .Register<CharityDonationNotification>(EventSubSubscriptionType.CharityDonation)
            .Register<CharityCampaignStartNotification>(EventSubSubscriptionType.CharityCampaignStart)
            .Register<CharityCampaignProgressNotification>(EventSubSubscriptionType.CharityCampaignProgress)
            .Register<CharityCampaignStopNotification>(EventSubSubscriptionType.CharityCampaignStop)
            .Register<ChannelChatClearNotification>(EventSubSubscriptionType.ChannelChatClear)
            .Register<ChannelChatClearUserMessagesNotification>(EventSubSubscriptionType.ChannelChatClearUserMessages)
            .Register<ChannelChatMessageNotification>(EventSubSubscriptionType.ChannelChatMessage)
            .Register<ChannelChatMessageDeleteNotification>(EventSubSubscriptionType.ChannelChatMessageDelete)
            .Register<ChannelChatNotificationNotification>(EventSubSubscriptionType.ChannelChatNotification)
            .Register<ChannelChatUserMessageHoldNotification>(EventSubSubscriptionType.ChannelChatUserMessageHold)
            .Register<ChannelChatUserMessageUpdateNotification>(EventSubSubscriptionType.ChannelChatUserMessageUpdate)
            .Register<ChannelChatSettingsUpdateNotification>(EventSubSubscriptionType.ChannelChatSettingsUpdate)
            .Register<GoalBeginNotification>(EventSubSubscriptionType.GoalBegin)
            .Register<GoalProgressNotification>(EventSubSubscriptionType.GoalProgress)
            .Register<GoalEndNotification>(EventSubSubscriptionType.GoalEnd)
            .Register<ChannelGuestStarSessionBeginNotification>(EventSubSubscriptionType.ChannelGuestStarSessionBegin)
            .Register<ChannelGuestStarSessionEndNotification>(EventSubSubscriptionType.ChannelGuestStarSessionEnd)
            .Register<ChannelGuestStarGuestUpdateNotification>(EventSubSubscriptionType.ChannelGuestStarGuestUpdate)
            .Register<ChannelGuestStarSettingsUpdateNotification>(EventSubSubscriptionType.ChannelGuestStarSettingsUpdate)
            .Register<HypeTrainBeginNotification>(EventSubSubscriptionType.HypeTrainBegin)
            .Register<HypeTrainProgressNotification>(EventSubSubscriptionType.HypeTrainProgress)
            .Register<HypeTrainEndNotification>(EventSubSubscriptionType.HypeTrainEnd)
            .Register<ChannelModeratorAddNotification>(EventSubSubscriptionType.ChannelModeratorAdd)
            .Register<ChannelModeratorRemoveNotification>(EventSubSubscriptionType.ChannelModeratorRemove)
            .Register<ChannelPollBeginNotification>(EventSubSubscriptionType.ChannelPollBegin)
            .Register<ChannelPollProgressNotification>(EventSubSubscriptionType.ChannelPollProgress)
            .Register<ChannelPollEndNotification>(EventSubSubscriptionType.ChannelPollEnd)
            .Register<ChannelPredictionBeginNotification>(EventSubSubscriptionType.ChannelPredictionBegin)
            .Register<ChannelPredictionProgressNotification>(EventSubSubscriptionType.ChannelPredictionProgress)
            .Register<ChannelPredictionLockNotification>(EventSubSubscriptionType.ChannelPredictionLock)
            .Register<ChannelPredictionEndNotification>(EventSubSubscriptionType.ChannelPredictionEnd)
            .Register<ChannelSharedChatBeginNotification>(EventSubSubscriptionType.ChannelSharedChatSessionBegin)
            .Register<ChannelSharedChatUpdateNotification>(EventSubSubscriptionType.ChannelSharedChatSessionUpdate)
            .Register<ChannelSharedChatEndNotification>(EventSubSubscriptionType.ChannelSharedChatSessionEnd)
            .Register<ShieldModeBeginNotification>(EventSubSubscriptionType.ShieldModeBegin)
            .Register<ShieldModeEndNotification>(EventSubSubscriptionType.ShieldModeEnd)
            .Register<ShoutoutCreateNotification>(EventSubSubscriptionType.ShoutoutCreate)
            .Register<ShoutoutReceivedNotification>(EventSubSubscriptionType.ShoutoutReceived)
            .Register<ChannelSubscriptionEndNotification>(EventSubSubscriptionType.ChannelSubscriptionEnd)
            .Register<ChannelSubscriptionGiftNotification>(EventSubSubscriptionType.ChannelSubscriptionGift)
            .Register<ChannelSubscriptionMessageNotification>(EventSubSubscriptionType.ChannelSubscriptionMessage)
            .Register<ChannelSuspiciousUserMessageNotification>(EventSubSubscriptionType.ChannelSuspiciousUserMessage)
            .Register<ChannelSuspiciousUserUpdateNotification>(EventSubSubscriptionType.ChannelSuspiciousUserUpdate)
            .Register<ChannelUnbanRequestCreateNotification>(EventSubSubscriptionType.ChannelUnbanRequestCreate)
            .Register<ChannelUnbanRequestResolveNotification>(EventSubSubscriptionType.ChannelUnbanRequestResolve)
            .Register<ChannelVipAddNotification>(EventSubSubscriptionType.ChannelVIPAdd)
            .Register<ChannelVipRemoveNotification>(EventSubSubscriptionType.ChannelVIPRemove)
            .Register<ChannelWarningAcknowledgementNotification>(EventSubSubscriptionType.ChannelWarningAcknowledgement)
            .Register<ChannelWarningSendNotification>(EventSubSubscriptionType.ChannelWarningSend)
            .Register<ConduitShardDisabledNotification>(EventSubSubscriptionType.ConduitShardDisabled)
            .Register<DropEntitlementGrantNotification>(EventSubSubscriptionType.DropEntitlementGrant)
            .Register<ExtensionBitsTransactionCreateNotification>(EventSubSubscriptionType.ExtensionBitsTransactionCreate)
            .Register<StreamOnlineNotification>(EventSubSubscriptionType.StreamOnline)
            .Register<StreamOfflineNotification>(EventSubSubscriptionType.StreamOffline)
            .Register<UserAuthorizationGrantNotification>(EventSubSubscriptionType.UserAuthorizationGrant)
            .Register<UserAuthorizationRevokeNotification>(EventSubSubscriptionType.UserAuthorizationRevoke)
            .Register<UserUpdateNotification>(EventSubSubscriptionType.UserUpdate)
            .Register<WhisperReceivedNotification>(EventSubSubscriptionType.WhisperReceived);

    private static Dictionary<EventSubSubscriptionType, Func<JsonSerializerOptions, JsonDocument, IEventSubNotification?>> Register<T>(
        this Dictionary<EventSubSubscriptionType, Func<JsonSerializerOptions, JsonDocument, IEventSubNotification?>> map,
        EventSubSubscriptionType subscriptionType
        )
        where T : IEventSubNotification
    {
        map.Add(subscriptionType, (options, document) => JsonSerializer.Deserialize<T>(document, options));
        return map;
    }

    /// <summary>
    /// Creates the default deserializer map for EventSub notification types.
    /// </summary>
    /// <remarks>
    /// You may need to use the output of this method if you want to extend the default subscription type list (e.g. if a specific subscription type is not yet implemented by default). 
    /// </remarks>
    /// <returns>A function mapping <see cref="EventSubSubscriptionType"/> to a specific deserialization function returning <see cref="IEventSubNotification"/> for that subscription type.</returns>
    public static Func<EventSubSubscriptionType, Func<JsonSerializerOptions, JsonDocument, IEventSubNotification?>?> CreateDefaultMap()
        => subscriptionType => _defaultMap.GetValueOrDefault(subscriptionType);
}
