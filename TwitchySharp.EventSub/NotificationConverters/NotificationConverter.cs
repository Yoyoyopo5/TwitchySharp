using System.Text.Json;
using TwitchySharp.EventSub.Interfaces;
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
using TwitchySharp.Shared;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.NotificationConverters;

/// <summary>
/// The default implementation of <see cref="INotificationConverter"/>.
/// Enables conversion between JSON EventSub notifications and C# instance types using a notification type map.
/// </summary>
/// <param name="notificationTypes">
/// The type map used to deserialize notifications into instances.
/// Type values must implement <see cref="IEventSubNotification"/> or <see cref="Deserialize(JsonDocument, EventSubSubscriptionType)"/> will throw <see cref="InvalidCastException"/>.
/// <para/>
/// If left null, the <see cref="DefaultNotificationTypes"/> map is used.
/// <para/>
/// Leave this null unless you know what you're doing. 
/// You can copy <see cref="DefaultNotificationTypes"/> and add new types if the type you need hasn't been included yet.
/// </param>
public class NotificationConverter(IReadOnlyDictionary<EventSubSubscriptionType, Type>? notificationTypes = null)
    : INotificationConverter
{
    /// <summary>
    /// The default notification type map supplied with TwitchySharp.
    /// Contains key value pairs mapping keys of <see cref="EventSubSubscriptionType"/> to corresponding types implementing <see cref="IEventSubNotification"/>.
    /// </summary>
    public static readonly IReadOnlyDictionary<EventSubSubscriptionType, Type> DefaultNotificationTypes = new Dictionary<EventSubSubscriptionType, Type>()
    {
        { EventSubSubscriptionType.AutomodMessageHold, typeof(AutomodMessageHoldNotification) },
        { EventSubSubscriptionType.AutomodMessageHoldV2, typeof(AutomodMessageHoldV2Notification) },
        { EventSubSubscriptionType.AutomodMessageUpdate, typeof(AutomodMessageUpdateNotification) },
        { EventSubSubscriptionType.AutomodMessageUpdateV2, typeof(AutomodMessageUpdateV2Notification) },
        { EventSubSubscriptionType.AutomodSettingsUpdate, typeof(AutomodSettingsUpdateNotification) },
        { EventSubSubscriptionType.AutomodTermsUpdate, typeof(AutomodTermsUpdateNotification) },
        { EventSubSubscriptionType.ChannelBitsUse, typeof(ChannelBitsUseNotification) },
        { EventSubSubscriptionType.ChannelUpdate, typeof(ChannelUpdateNotification) },
        { EventSubSubscriptionType.ChannelFollow, typeof(ChannelFollowNotification) },
        { EventSubSubscriptionType.ChannelAdBreakBegin, typeof(ChannelAdBreakBeginNotification) },
        { EventSubSubscriptionType.ChannelChatClear, typeof(ChannelChatClearNotification) },
        { EventSubSubscriptionType.ChannelChatClearUserMessages, typeof(ChannelChatClearUserMessagesNotification) },
        { EventSubSubscriptionType.ChannelChatMessage, typeof(ChannelChatMessageNotification) },
        { EventSubSubscriptionType.ChannelChatMessageDelete, typeof(ChannelChatMessageDeleteNotification) },
        { EventSubSubscriptionType.ChannelChatNotification, typeof(ChannelChatNotificationNotification) },
        { EventSubSubscriptionType.ChannelChatSettingsUpdate, typeof(ChannelChatSettingsUpdateNotification) },
        { EventSubSubscriptionType.ChannelChatUserMessageHold, typeof(ChannelChatUserMessageHoldNotification) },
        { EventSubSubscriptionType.ChannelChatUserMessageUpdate, typeof(ChannelChatUserMessageUpdateNotification) },
        { EventSubSubscriptionType.ChannelSharedChatSessionBegin, typeof(ChannelSharedChatBeginNotification) },
        { EventSubSubscriptionType.ChannelSharedChatSessionUpdate, typeof(ChannelSharedChatUpdateNotification) },
        { EventSubSubscriptionType.ChannelSharedChatSessionEnd, typeof(ChannelSharedChatEndNotification) },
        { EventSubSubscriptionType.ChannelSubscribe, typeof(ChannelSubscribeNotification) },
        { EventSubSubscriptionType.ChannelSubscriptionEnd, typeof(ChannelSubscriptionEndNotification) },
        { EventSubSubscriptionType.ChannelSubscriptionGift, typeof(ChannelSubscriptionGiftNotification) },
        { EventSubSubscriptionType.ChannelSubscriptionMessage, typeof(ChannelSubscriptionMessageNotification) },
        { EventSubSubscriptionType.ChannelCheer, typeof(ChannelCheerNotification) },
        { EventSubSubscriptionType.ChannelRaid, typeof(ChannelRaidNotification) },
        { EventSubSubscriptionType.ChannelBan, typeof(ChannelBanNotification) },
        { EventSubSubscriptionType.ChannelUnban, typeof(ChannelUnbanNotification) },
        { EventSubSubscriptionType.ChannelUnbanRequestCreate, typeof(ChannelUnbanRequestCreateNotification) },
        { EventSubSubscriptionType.ChannelUnbanRequestResolve, typeof(ChannelUnbanRequestResolveNotification) },
        { EventSubSubscriptionType.ChannelModerate, typeof(ChannelModerateNotification) },
        { EventSubSubscriptionType.ChannelModerateV2, typeof(ChannelModerateV2Notification) },
        { EventSubSubscriptionType.ChannelModeratorAdd, typeof(ChannelModeratorAddNotification) },
        { EventSubSubscriptionType.ChannelModeratorRemove, typeof(ChannelModeratorRemoveNotification) },
        { EventSubSubscriptionType.ChannelGuestStarSessionBegin, typeof(ChannelGuestStarSessionBeginNotification) },
        { EventSubSubscriptionType.ChannelGuestStarSessionEnd, typeof(ChannelGuestStarSessionEndNotification) },
        { EventSubSubscriptionType.ChannelGuestStarGuestUpdate, typeof(ChannelGuestStarGuestUpdateNotification) },
        { EventSubSubscriptionType.ChannelGuestStarSettingsUpdate, typeof(ChannelGuestStarSettingsUpdateNotification) },
        { EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAdd, typeof(ChannelPointsAutomaticRewardRedemptionAddNotification) },
        { EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAddV2, typeof(ChannelPointsAutomaticRewardRedemptionAddV2Notification) },
        { EventSubSubscriptionType.ChannelPointsCustomRewardAdd, typeof(ChannelPointsCustomRewardAddNotification) },
        { EventSubSubscriptionType.ChannelPointsCustomRewardUpdate, typeof(ChannelPointsCustomRewardUpdateNotification) },
        { EventSubSubscriptionType.ChannelPointsCustomRewardRemove, typeof(ChannelPointsCustomRewardRemoveNotification) },
        { EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionAdd, typeof(ChannelPointsCustomRewardRedemptionAddNotification) },
        { EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionUpdate, typeof(ChannelPointsCustomRewardRedemptionUpdateNotification) },
        { EventSubSubscriptionType.ChannelPollBegin, typeof(ChannelPollBeginNotification) },
        { EventSubSubscriptionType.ChannelPollProgress, typeof(ChannelPollProgressNotification) },
        { EventSubSubscriptionType.ChannelPollEnd, typeof(ChannelPollEndNotification) },
        { EventSubSubscriptionType.ChannelPredictionBegin, typeof(ChannelPredictionBeginNotification) },
        { EventSubSubscriptionType.ChannelPredictionProgress, typeof(ChannelPredictionProgressNotification) },
        { EventSubSubscriptionType.ChannelPredictionLock, typeof(ChannelPredictionLockNotification) },
        { EventSubSubscriptionType.ChannelPredictionEnd, typeof(ChannelPredictionEndNotification) },
        { EventSubSubscriptionType.ChannelSuspiciousUserMessage, typeof(ChannelSuspiciousUserMessageNotification) },
        { EventSubSubscriptionType.ChannelSuspiciousUserUpdate, typeof(ChannelSuspiciousUserUpdateNotification) },
        { EventSubSubscriptionType.ChannelVIPAdd, typeof(ChannelVipAddNotification) },
        { EventSubSubscriptionType.ChannelVIPRemove, typeof(ChannelVipRemoveNotification) },
        { EventSubSubscriptionType.ChannelWarningAcknowledgement, typeof(ChannelWarningAcknowledgementNotification) },
        { EventSubSubscriptionType.ChannelWarningSend, typeof(ChannelWarningSendNotification) },
        { EventSubSubscriptionType.CharityDonation, typeof(CharityDonationNotification) },
        { EventSubSubscriptionType.CharityCampaignStart, typeof(CharityCampaignStartNotification) },
        { EventSubSubscriptionType.CharityCampaignProgress, typeof(CharityCampaignProgressNotification) },
        { EventSubSubscriptionType.CharityCampaignStop, typeof(CharityCampaignStopNotification) },
        { EventSubSubscriptionType.ConduitShardDisabled, typeof(ConduitShardDisabledNotification) },
        { EventSubSubscriptionType.DropEntitlementGrant, typeof(DropEntitlementGrantNotification) },
        { EventSubSubscriptionType.ExtensionBitsTransactionCreate, typeof(ExtensionBitsTransactionCreateNotification) },
        { EventSubSubscriptionType.GoalBegin, typeof(GoalBeginNotification) },
        { EventSubSubscriptionType.GoalProgress, typeof(GoalProgressNotification) },
        { EventSubSubscriptionType.GoalEnd, typeof(GoalEndNotification) },
        { EventSubSubscriptionType.HypeTrainBeginV2, typeof(HypeTrainBeginV2Notification) },
        { EventSubSubscriptionType.HypeTrainProgressV2, typeof(HypeTrainProgressV2Notification) },
        { EventSubSubscriptionType.HypeTrainEndV2, typeof(HypeTrainEndV2Notification) },
        { EventSubSubscriptionType.ShieldModeBegin, typeof(ShieldModeBeginNotification) },
        { EventSubSubscriptionType.ShieldModeEnd, typeof(ShieldModeEndNotification) },
        { EventSubSubscriptionType.ShoutoutCreate, typeof(ShoutoutCreateNotification) },
        { EventSubSubscriptionType.ShoutoutReceived, typeof(ShoutoutReceivedNotification) },
        { EventSubSubscriptionType.StreamOnline, typeof(StreamOnlineNotification) },
        { EventSubSubscriptionType.StreamOffline, typeof(StreamOfflineNotification) },
        { EventSubSubscriptionType.UserAuthorizationGrant, typeof(UserAuthorizationGrantNotification) },
        { EventSubSubscriptionType.UserAuthorizationRevoke, typeof(UserAuthorizationRevokeNotification) },
        { EventSubSubscriptionType.UserUpdate, typeof(UserUpdateNotification) },
        { EventSubSubscriptionType.WhisperReceived, typeof(WhisperReceivedNotification) }
    };

    private JsonSerializerOptions _serializerOptions = JsonConfig.ApiOptions;
    private IReadOnlyDictionary<EventSubSubscriptionType, Type> _notificationTypes = notificationTypes ?? DefaultNotificationTypes;

    /// <summary>
    /// <inheritdoc cref="Deserialize(JsonElement, EventSubSubscriptionType)"/>
    /// </summary>
    /// <param name="json"><inheritdoc cref="Deserialize(JsonDocument, EventSubSubscriptionType)" path="/param[@name='json']"/></param>
    /// <returns><inheritdoc cref="Deserialize(JsonDocument, EventSubSubscriptionType)"/></returns>
    /// <inheritdoc cref="Deserialize(JsonDocument, EventSubSubscriptionType)" path="/exception"/>
    public IEventSubNotification Deserialize(JsonDocument json)
        => Deserialize(json.RootElement);

    /// <summary>
    /// <inheritdoc cref="Deserialize(JsonElement, EventSubSubscriptionType)"/>
    /// </summary>
    /// <param name="json"><inheritdoc cref="Deserialize(JsonElement, EventSubSubscriptionType)" path="/param[@name='json']"/></param>
    /// <returns><inheritdoc cref="Deserialize(JsonElement, EventSubSubscriptionType)"/></returns>
    /// <inheritdoc cref="Deserialize(JsonElement, EventSubSubscriptionType)" path="/exception"/>
    public IEventSubNotification Deserialize(JsonElement json)
        => Deserialize(json, json.Deserialize<EventSubNotification>(_serializerOptions)?.Subscription ?? throw new ArgumentException("JSON cannot be null literal.", nameof(json)));

    /// <summary>
    /// <inheritdoc cref="Deserialize(JsonElement, EventSubSubscriptionType)"/>
    /// </summary>
    /// <param name="json"><inheritdoc cref="Deserialize(string, EventSubSubscriptionType)" path="/param[@name='json']"/></param>
    /// <returns><inheritdoc cref="Deserialize(string, EventSubSubscriptionType)"/></returns>
    /// <inheritdoc cref="Deserialize(string, EventSubSubscriptionType)" path="/exception"/>
    public IEventSubNotification Deserialize(string json)
        => Deserialize(json, JsonSerializer.Deserialize<EventSubNotification>(json, _serializerOptions)?.Subscription ?? throw new ArgumentException("JSON cannot be null literal.", nameof(json)));

    /// <summary>
    /// <inheritdoc cref="Deserialize(JsonElement, EventSubSubscriptionType)"/>
    /// </summary>
    /// <param name="json"><inheritdoc cref="Deserialize(JsonElement, EventSubSubscriptionType)" path="/param[@name='json']"/></param>
    /// <param name="subscriptionType"><inheritdoc cref="Deserialize(JsonElement, EventSubSubscriptionType)" path="/param[@name='subscriptionType']"/></param>
    /// <returns>
    /// <inheritdoc cref="Deserialize(JsonElement, EventSubSubscriptionType)"/>
    /// </returns>
    /// <exception cref="ArgumentException"><inheritdoc cref="Deserialize(JsonElement, EventSubSubscriptionType)"/></exception>
    /// <exception cref="InvalidCastException"><inheritdoc cref="Deserialize(JsonElement, EventSubSubscriptionType)"/></exception>
    /// <exception cref="JsonException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    public IEventSubNotification Deserialize(JsonDocument json, EventSubSubscriptionType subscriptionType)
        => Deserialize(json.RootElement, subscriptionType);

    /// <summary>
    /// Deserializes a JSON document into a type implementing <see cref="IEventSubNotification"/> using the class' notification type map.
    /// </summary>
    /// <param name="json"><inheritdoc cref="INotificationConverter.Deserialize(JsonDocument, EventSubSubscriptionType)" path="/param[@name='json']"/></param>
    /// <param name="subscriptionType"><inheritdoc cref="INotificationConverter.Deserialize(JsonDocument, EventSubSubscriptionType)" path="/param[@name='subscriptionType']"/></param>
    /// <returns>
    /// <inheritdoc cref="INotificationConverter.Deserialize(JsonDocument, EventSubSubscriptionType)"/>
    /// You can use a switch expression to pattern match this value into any number of distinct instance types.
    /// </returns>
    /// <exception cref="ArgumentException">The <paramref name="json"/> was a null literal value.</exception>
    /// <exception cref="InvalidCastException">The value of the <paramref name="subscriptionType"/> key in this instance's notification type map is not a type that implements <see cref="IEventSubNotification"/>.</exception>
    /// <exception cref="JsonException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    public IEventSubNotification Deserialize(JsonElement json, EventSubSubscriptionType subscriptionType)
        => (IEventSubNotification?)json.Deserialize(_notificationTypes[subscriptionType], _serializerOptions) ?? throw new ArgumentException("JSON cannot be null literal.", nameof(json));

    /// <summary>
    /// <inheritdoc cref="Deserialize(JsonElement, EventSubSubscriptionType)"/>
    /// </summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <param name="subscriptionType"><inheritdoc cref="Deserialize(JsonElement, EventSubSubscriptionType)" path="/param[@name='subscriptionType']"/></param>
    /// <returns><inheritdoc cref="Deserialize(JsonElement, EventSubSubscriptionType)"/></returns>
    /// <exception cref="ArgumentException"><inheritdoc cref="Deserialize(JsonElement, EventSubSubscriptionType)"/></exception>
    /// <exception cref="InvalidCastException"><inheritdoc cref="Deserialize(JsonElement, EventSubSubscriptionType)"/></exception>
    /// <exception cref="JsonException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    public IEventSubNotification Deserialize(string json, EventSubSubscriptionType subscriptionType)
         => (IEventSubNotification?)JsonSerializer.Deserialize(json, _notificationTypes[subscriptionType], _serializerOptions) ?? throw new ArgumentException("JSON cannot be null literal.", nameof(json));
}
