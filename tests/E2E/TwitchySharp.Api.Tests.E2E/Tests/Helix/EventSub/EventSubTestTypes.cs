using TwitchySharp.Api.Helix.EventSub.Subscriptions;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.EventSub;

public class EventSubTestTypes : TheoryData<string, EventSubTransportMethod>
{
    /// <summary>
    /// Types available to the testing framework. 
    /// Be sure to include the corresponding type instance in <see cref="EventSubFixture.GenerateTypeMapAsync"/>.
    /// </summary>
    private readonly IEnumerable<string> _subscriptionTypeNames =
    [
        nameof(AutomodMessageHoldV2),
        nameof(AutomodMessageUpdateV2),
        nameof(AutomodSettingsUpdate),
        nameof(AutomodTermsUpdate),
        nameof(ChannelAdBreakBegin),
        nameof(ChannelPointsAutomaticRewardRedemptionAdd),
        nameof(ChannelPointsCustomRewardAdd),
        nameof(ChannelPointsCustomRewardRemove),
        nameof(ChannelPointsCustomRewardUpdate),
        nameof(ChannelPointsCustomRewardRedemptionAdd),
        nameof(ChannelPointsCustomRewardRedemptionUpdate),
        nameof(CharityCampaignProgress),
        nameof(CharityCampaignStart),
        nameof(CharityCampaignStop),
        nameof(CharityDonation),
        nameof(ChannelChatClear),
        nameof(ChannelChatClearUserMessages),
        nameof(ChannelChatMessage),
        nameof(ChannelChatMessageDelete),
        nameof(ChannelChatNotification),
        nameof(ChannelChatUserMessageHold),
        nameof(ChannelChatUserMessageUpdate),
        nameof(ChannelChatSettingsUpdate),
        nameof(GoalBegin),
        nameof(GoalEnd),
        nameof(GoalProgress),
        nameof(ChannelGuestStarGuestUpdate),
        nameof(ChannelGuestStarSessionBegin),
        nameof(ChannelGuestStarSettingsUpdate),
        nameof(HypeTrainBeginV2),
        nameof(HypeTrainEndV2),
        nameof(HypeTrainProgressV2),
        nameof(ChannelModeratorAdd),
        nameof(ChannelModeratorRemove),
        nameof(ChannelPollBegin),
        nameof(ChannelPollEnd),
        nameof(ChannelPollProgress),
        nameof(ChannelPredictionBegin),
        nameof(ChannelPredictionEnd),
        nameof(ChannelPredictionLock),
        nameof(ChannelPredictionProgress),
        nameof(ChannelSharedChatSessionBegin),
        nameof(ChannelSharedChatSessionEnd),
        nameof(ChannelSharedChatSessionUpdate),
        nameof(ShieldModeBegin),
        nameof(ShieldModeEnd),
        nameof(ShoutoutCreate),
        nameof(ShoutoutReceived),
        nameof(ChannelSubscriptionEnd),
        nameof(ChannelSubscriptionGift),
        nameof(ChannelSubscriptionMessage),
        nameof(ChannelSuspiciousUserMessage),
        nameof(ChannelSuspiciousUserUpdate),
        nameof(ChannelUnbanRequestCreate),
        nameof(ChannelUnbanRequestResolve),
        nameof(ChannelVipAdd),
        nameof(ChannelVipRemove),
        nameof(ChannelWarningAcknowledgement),
        nameof(ChannelWarningSend),
        nameof(ChannelBan),
        nameof(ChannelCheer),
        nameof(ChannelFollow),
        nameof(ChannelModerateV2),
        nameof(ChannelRaid),
        nameof(ChannelSubscribe),
        nameof(ChannelUnban),
        nameof(ChannelUpdate),
        nameof(ConduitShardDisabled),
        nameof(DropEntitlementGrant),
        nameof(ExtensionBitsTransactionCreate),
        nameof(StreamOffline),
        nameof(StreamOnline),
        nameof(UserAuthorizationGrant),
        nameof(UserAuthorizationRevoke),
        nameof(WhisperReceived),
        nameof(UserUpdate)
    ];

    public EventSubTestTypes()
    {
        foreach (string subscriptionType in _subscriptionTypeNames)
        {
            Add(subscriptionType, EventSubTransportMethod.Webhook);
            Add(subscriptionType, EventSubTransportMethod.Websocket);
        }
    }
}
