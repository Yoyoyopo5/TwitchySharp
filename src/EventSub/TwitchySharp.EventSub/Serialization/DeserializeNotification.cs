using System.Text.Json;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Serialization;

namespace TwitchySharp.EventSub.Serialization;

/// <summary>
/// Polymorphically deserializes a <see cref="NotificationPayloadStream"/> into specific notification type implementing <see cref="IEventSubNotification"/>.
/// </summary>
/// <remarks>
/// Use <see cref="DeserializeNotificationExtensions.ByPolymorphicJsonDeserialization"/> to create a default deserializer.
/// </remarks>
/// <param name="payload">The notification payload, as a <see cref="Stream"/>.</param>
/// <param name="ct">Cancellation token.</param>
/// <returns>A <see cref="ValueTask"/> containing a <see cref="Validation"/> of the deserialized notification.</returns>
public delegate ValueTask<Result<IEventSubNotification>> DeserializeNotification(NotificationPayloadStream payload, CancellationToken ct);

/// <summary>
/// Creation helpers for <see cref="DeserializeNotification"/>.
/// </summary>
public static class DeserializeNotificationExtensions
{
    /// <summary>
    /// The notification JSON is invalid.
    /// </summary>
    /// <param name="Message">The reason for the invalid notification JSON.</param>
    public record InvalidJsonError(string Message) : Error(Message);
    /// <summary>
    /// An exception was thrown during JSON parsing.
    /// </summary>
    /// <param name="Exception">The exception that was thrown.</param>
    public record JsonParserExceptionError(Exception Exception) : Error(Exception.Message);
    /// <summary>
    /// An exception occurred during notification deserialization.
    /// </summary>
    /// <param name="JsonSerializerException">The exception.</param>
    public record DeserializationExceptionError(Exception JsonSerializerException)
        : Error(JsonSerializerException.Message);
    /// <summary>
    /// A notification deserializer was not found for a specific subscription type.
    /// </summary>
    /// <param name="SubscriptionType">The subscription type that the notification was for.</param>
    public record MissingDeserializerError(EventSubSubscriptionType SubscriptionType) :
        Error($"Missing notification deserializer for {SubscriptionType}");

    extension (DeserializeNotification d)
    {
        /// <summary>
        /// Create a <see cref="DeserializeNotification"/> function that uses a set of
        /// notification deserializer functions corresponding to specific <see cref="EventSubSubscriptionType"/>s.
        /// </summary>
        /// <param name="configureDeserializers">
        /// A function that takes the default set of deserializers and returns a
        /// configured set of deserializers (represented as a function taking the
        /// <see cref="EventSubSubscriptionType"/> and returning the deserializer function).
        /// </param>
        /// <returns>A <see cref="DeserializeNotification"/> function using polymorphic JSON deserialization.</returns>
        public static DeserializeNotification ByPolymorphicJsonDeserialization(
            Func<
                Func<EventSubSubscriptionType, Func<JsonDocument, Result<IEventSubNotification>>?>,
                Func<EventSubSubscriptionType, Func<JsonDocument, Result<IEventSubNotification>>?>
                >? configureDeserializers = null
            )
        {
            Func<EventSubSubscriptionType, Func<JsonDocument, Result<IEventSubNotification>>?> getDeserializer
                = configureDeserializers is null
                    ? CreateDefaultMap()
                    : configureDeserializers(CreateDefaultMap());

            return async (payload, ct) =>
            {
                try
                {
                    using JsonDocument d = await JsonDocument.ParseAsync(payload, default, ct);
                    string json = d.RootElement.ToString();
                    return d.RootElement.GetSubscriptionType()
                        .Bind(subscriptionType => getDeserializer(subscriptionType) is not { } deserialize
                            ? new MissingDeserializerError(subscriptionType)
                            : deserialize(d));
                }
                catch (Exception ex)
                {
                    return new JsonParserExceptionError(ex);
                }
            };
        }
    }

    private static Result<EventSubSubscriptionType> GetSubscriptionType(
        this JsonElement notification
        )
    {
        const string SUBSCRIPTION_PROPERTY_NAME = "subscription";
        const string SUBSCRIPTION_TYPE_PROPERTY_NAME = "type";
        const string SUBSCRIPTION_VERSION_PROPERTY_NAME = "version";

        return notification.ValueKind != JsonValueKind.Object
            ? new InvalidJsonError($"Notification is JSON {notification.ValueKind} (must be JSON object).")
            : !notification.TryGetProperty(SUBSCRIPTION_PROPERTY_NAME, out JsonElement subscriptionElement)
            ? new InvalidJsonError("Notification does not have a subscription property.")
            : !subscriptionElement.TryGetProperty(SUBSCRIPTION_TYPE_PROPERTY_NAME, out JsonElement subscriptionTypeElement)
            ? new InvalidJsonError("Notification does not have a type property.")
            : !subscriptionElement.TryGetProperty(SUBSCRIPTION_VERSION_PROPERTY_NAME, out JsonElement subscriptionVersionElement)
            ? new InvalidJsonError("Notification does not have a version property.")
            : subscriptionTypeElement.ValueKind != JsonValueKind.String || subscriptionTypeElement.GetString() is not string subscriptionType
            ? new InvalidJsonError($"Notification type property is {subscriptionTypeElement.ValueKind} (Expected {nameof(JsonValueKind.String)}).")
            : subscriptionVersionElement.ValueKind != JsonValueKind.String || subscriptionVersionElement.GetString() is not string subscriptionVersion
            ? new InvalidJsonError($"Notification version property is {subscriptionTypeElement.ValueKind} (Expected {nameof(JsonValueKind.String)}).")
            : new EventSubSubscriptionType(new(subscriptionType), new(subscriptionVersion));
    }

    // We could add a static abstract interface to point notification types to subscription types,
    // but we will still need to register each type, so I'm just leaving the mapping here for now.
    private readonly static Dictionary<EventSubSubscriptionType, Func<JsonDocument, Result<IEventSubNotification>>> _defaultMap
        = new Dictionary<EventSubSubscriptionType, Func<JsonDocument, Result<IEventSubNotification>>>()
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

    private static Dictionary<EventSubSubscriptionType, Func<JsonDocument, Result<IEventSubNotification>>> Register<T>(
        this Dictionary<EventSubSubscriptionType, Func<JsonDocument, Result<IEventSubNotification>>> map,
        EventSubSubscriptionType subscriptionType
        )
        where T : IEventSubNotification
    {
        map.Add(subscriptionType, document =>
        {
            try
            {
                return JsonSerializer.Deserialize<T>(document, JsonConfig.ApiOptions) is T value
                    ? value
                    : new InvalidJsonError("Notification was null literal JSON.");
            }
            catch (Exception ex)
            {
                return new DeserializationExceptionError(ex);
            }
        });
        return map;
    }

    /// <summary>
    /// Creates the default deserializer map for EventSub notification types.
    /// </summary>
    /// <remarks>
    /// You may need to use the output of this method if you want to extend the default subscription type list (e.g. if a specific subscription type is not yet implemented by default). 
    /// </remarks>
    /// <returns>A function mapping <see cref="EventSubSubscriptionType"/> to a specific deserialization function returning <see cref="IEventSubNotification"/> for that subscription type.</returns>
    private static Func<EventSubSubscriptionType, Func<JsonDocument, Result<IEventSubNotification>>?> CreateDefaultMap()
        => subscriptionType => _defaultMap.GetValueOrDefault(subscriptionType);
}
