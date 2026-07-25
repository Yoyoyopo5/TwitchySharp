using System.Collections;
using TwitchySharp.Api.Helix.EventSub.Subscriptions;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.EventSub;

public class EventSubTestProvider : IEnumerable<TheoryDataRow<EventSubTest>>
{
    private const string EVENT_SUB_TEST_PREFIX = "event-sub-";
    private static TestName CreateTestName(string name)
        => new(EVENT_SUB_TEST_PREFIX + name);

    public readonly static IEnumerable<TheoryDataRow<EventSubTest>> Data = [
        new TheoryDataRow<EventSubTest>(new EventSubTest<AutomodMessageHold, UserConfiguration>()
        {
            TestName = CreateTestName("automod-message-hold"),
            CreateSpecification = userConfig => new AutomodMessageHold(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<AutomodMessageHoldV2, UserConfiguration>()
        {
            TestName = CreateTestName("automod-message-hold-v2"),
            CreateSpecification = userConfig => new AutomodMessageHoldV2(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<AutomodMessageUpdate, UserConfiguration>()
        {
            TestName = CreateTestName("automod-message-update"),
            CreateSpecification = userConfig => new AutomodMessageUpdate(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<AutomodMessageUpdateV2, UserConfiguration>()
        {
            TestName = CreateTestName("automod-message-update-v2"),
            CreateSpecification = userConfig => new AutomodMessageUpdateV2(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<AutomodSettingsUpdate, UserConfiguration>()
        {
            TestName = CreateTestName("automod-settings-update"),
            CreateSpecification = userConfig => new AutomodSettingsUpdate(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<AutomodTermsUpdate, UserConfiguration>()
        {
            TestName = CreateTestName("automod-terms-update"),
            CreateSpecification = userConfig => new AutomodTermsUpdate(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelBitsUse, UserConfiguration>()
        {
            TestName = CreateTestName("channel-bits-use"),
            CreateSpecification = userConfig => new ChannelBitsUse(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelUpdate, UserConfiguration>()
        {
            TestName = CreateTestName("channel-update"),
            CreateSpecification = userConfig => new ChannelUpdate(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelFollow, UserConfiguration>()
        {
            TestName = CreateTestName("channel-follow"),
            CreateSpecification = userConfig => new ChannelFollow(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelAdBreakBegin, UserConfiguration>()
        {
            TestName = CreateTestName("channel-ad-break-begin"),
            CreateSpecification = userConfig => new ChannelAdBreakBegin(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelChatClear, UserConfiguration>()
        {
            TestName = CreateTestName("channel-chat-clear"),
            CreateSpecification = userConfig => new ChannelChatClear(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelChatClearUserMessages, UserConfiguration>()
        {
            TestName = CreateTestName("channel-chat-clear-user-messages"),
            CreateSpecification = userConfig => new ChannelChatClearUserMessages(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelChatMessage, UserConfiguration>()
        {
            TestName = CreateTestName("channel-chat-message"),
            CreateSpecification = userConfig => new ChannelChatMessage(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelChatMessageDelete, UserConfiguration>()
        {
            TestName = CreateTestName("channel-chat-message-delete"),
            CreateSpecification = userConfig => new ChannelChatMessageDelete(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelChatNotification, UserConfiguration>()
        {
            TestName = CreateTestName("channel-chat-notification"),
            CreateSpecification = userConfig => new ChannelChatNotification(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelChatSettingsUpdate, UserConfiguration>()
        {
            TestName = CreateTestName("channel-chat-settings-update"),
            CreateSpecification = userConfig => new ChannelChatSettingsUpdate(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelChatUserMessageHold, UserConfiguration>()
        {
            TestName = CreateTestName("channel-chat-user-message-hold"),
            CreateSpecification = userConfig => new ChannelChatUserMessageHold(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelChatUserMessageUpdate, UserConfiguration>()
        {
            TestName = CreateTestName("channel-chat-user-message-update"),
            CreateSpecification = userConfig => new ChannelChatUserMessageUpdate(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelSharedChatSessionBegin, UserConfiguration>()
        {
            TestName = CreateTestName("channel-shared-chat-session-begin"),
            CreateSpecification = userConfig => new ChannelSharedChatSessionBegin(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelSharedChatSessionUpdate, UserConfiguration>()
        {
            TestName = CreateTestName("channel-shared-chat-session-update"),
            CreateSpecification = userConfig => new ChannelSharedChatSessionUpdate(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelSharedChatSessionEnd, UserConfiguration>()
        {
            TestName = CreateTestName("channel-shared-chat-session-end"),
            CreateSpecification = userConfig => new ChannelSharedChatSessionEnd(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelSubscribe, UserConfiguration>()
        {
            TestName = CreateTestName("channel-subscription"),
            CreateSpecification = userConfig => new ChannelSubscribe(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelSubscriptionEnd, UserConfiguration>()
        {
            TestName = CreateTestName("channel-subscription-end"),
            CreateSpecification = userConfig => new ChannelSubscriptionEnd(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelSubscriptionGift, UserConfiguration>()
        {
            TestName = CreateTestName("channel-subscription-gift"),
            CreateSpecification = userConfig => new ChannelSubscriptionGift(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelSubscriptionMessage, UserConfiguration>()
        {
            TestName = CreateTestName("channel-subscription-message"),
            CreateSpecification = userConfig => new ChannelSubscriptionMessage(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelCheer, UserConfiguration>()
        {
            TestName = CreateTestName("channel-cheer"),
            CreateSpecification = userConfig => new ChannelCheer(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelRaid, UserConfiguration>()
        {
            TestName = CreateTestName("channel-raid"),
            CreateSpecification = userConfig => new ChannelRaid(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelBan, UserConfiguration>()
        {
            TestName = CreateTestName("channel-ban"),
            CreateSpecification = userConfig => new ChannelBan(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelUnban, UserConfiguration>()
        {
            TestName = CreateTestName("channel-unban"),
            CreateSpecification = userConfig => new ChannelUnban(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelUnbanRequestCreate, UserConfiguration>()
        {
            TestName = CreateTestName("channel-unban-request-create"),
            CreateSpecification = userConfig => new ChannelUnbanRequestCreate(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelUnbanRequestResolve, UserConfiguration>()
        {
            TestName = CreateTestName("channel-unban-request-resolve"),
            CreateSpecification = userConfig => new ChannelUnbanRequestResolve(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelModerate, UserConfiguration>()
        {
            TestName = CreateTestName("channel-moderate"),
            CreateSpecification = userConfig => new ChannelModerate(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelModerateV2, UserConfiguration>()
        {
            TestName = CreateTestName("channel-moderate-v2"),
            CreateSpecification = userConfig => new ChannelModerateV2(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelModeratorAdd, UserConfiguration>()
        {
            TestName = CreateTestName("channel-moderator-add"),
            CreateSpecification = userConfig => new ChannelModeratorAdd(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelModeratorRemove, UserConfiguration>()
        {
            TestName = CreateTestName("channel-moderator-remove"),
            CreateSpecification = userConfig => new ChannelModeratorRemove(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelGuestStarSessionBegin, UserConfiguration>()
        {
            TestName = CreateTestName("channel-guest-star-session-begin"),
            CreateSpecification = userConfig => new ChannelGuestStarSessionBegin(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelGuestStarSessionEnd, UserConfiguration>()
        {
            TestName = CreateTestName("channel-guest-star-session-end"),
            CreateSpecification = userConfig => new ChannelGuestStarSessionEnd(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelGuestStarGuestUpdate, UserConfiguration>()
        {
            TestName = CreateTestName("channel-guest-star-guest-update"),
            CreateSpecification = userConfig => new ChannelGuestStarGuestUpdate(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelGuestStarSettingsUpdate, UserConfiguration>()
        {
            TestName = CreateTestName("channel-guest-star-settings-update"),
            CreateSpecification = userConfig => new ChannelGuestStarSettingsUpdate(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelPointsAutomaticRewardRedemptionAdd, UserConfiguration>()
        {
            TestName = CreateTestName("channel-points-automatic-reward-redemption-add"),
            CreateSpecification = userConfig => new ChannelPointsAutomaticRewardRedemptionAdd(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelPointsAutomaticRewardRedemptionAddV2, UserConfiguration>()
        {
            TestName = CreateTestName("channel-points-automatic-reward-redemption-add-v2"),
            CreateSpecification = userConfig => new ChannelPointsAutomaticRewardRedemptionAddV2(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelPointsCustomRewardAdd, UserConfiguration>()
        {
            TestName = CreateTestName("channel-points-custom-reward-add"),
            CreateSpecification = userConfig => new ChannelPointsCustomRewardAdd(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelPointsCustomRewardUpdate, UserConfiguration>()
        {
            TestName = CreateTestName("channel-points-custom-reward-update"),
            CreateSpecification = userConfig => new ChannelPointsCustomRewardUpdate(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelPointsCustomRewardRemove, UserConfiguration>()
        {
            TestName = CreateTestName("channel-points-custom-reward-remove"),
            CreateSpecification = userConfig => new ChannelPointsCustomRewardRemove(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelPointsCustomRewardRedemptionAdd, UserConfiguration>()
        {
            TestName = CreateTestName("channel-points-custom-reward-redemption-add"),
            CreateSpecification = userConfig => new ChannelPointsCustomRewardRedemptionAdd(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelPointsCustomRewardRedemptionUpdate, UserConfiguration>()
        {
            TestName = CreateTestName("channel-points-custom-reward-redemption-update"),
            CreateSpecification = userConfig => new ChannelPointsCustomRewardRedemptionUpdate(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelPollBegin, UserConfiguration>()
        {
            TestName = CreateTestName("channel-poll-begin"),
            CreateSpecification = userConfig => new ChannelPollBegin(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelPollProgress, UserConfiguration>()
        {
            TestName = CreateTestName("channel-poll-progress"),
            CreateSpecification = userConfig => new ChannelPollProgress(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelPollEnd, UserConfiguration>()
        {
            TestName = CreateTestName("channel-poll-end"),
            CreateSpecification = userConfig => new ChannelPollEnd(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelPredictionBegin, UserConfiguration>()
        {
            TestName = CreateTestName("channel-prediction-begin"),
            CreateSpecification = userConfig => new ChannelPredictionBegin(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelPredictionProgress, UserConfiguration>()
        {
            TestName = CreateTestName("channel-prediction-progress"),
            CreateSpecification = userConfig => new ChannelPredictionProgress(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelPredictionLock, UserConfiguration>()
        {
            TestName = CreateTestName("channel-prediction-lock"),
            CreateSpecification = userConfig => new ChannelPredictionLock(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelPredictionEnd, UserConfiguration>()
        {
            TestName = CreateTestName("channel-prediction-end"),
            CreateSpecification = userConfig => new ChannelPredictionEnd(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelSuspiciousUserMessage, UserConfiguration>()
        {
            TestName = CreateTestName("channel-suspicious-user-message"),
            CreateSpecification = userConfig => new ChannelSuspiciousUserMessage(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelSuspiciousUserUpdate, UserConfiguration>()
        {
            TestName = CreateTestName("channel-suspicious-user-update"),
            CreateSpecification = userConfig => new ChannelSuspiciousUserUpdate(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelVipAdd, UserConfiguration>()
        {
            TestName = CreateTestName("channel-vip-add"),
            CreateSpecification = userConfig => new ChannelVipAdd(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelVipRemove, UserConfiguration>()
        {
            TestName = CreateTestName("channel-vip-remove"),
            CreateSpecification = userConfig => new ChannelVipRemove(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelWarningAcknowledgement, UserConfiguration>()
        {
            TestName = CreateTestName("channel-warning-acknowledgement"),
            CreateSpecification = userConfig => new ChannelWarningAcknowledgement(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ChannelWarningSend, UserConfiguration>()
        {
            TestName = CreateTestName("channel-warning-send"),
            CreateSpecification = userConfig => new ChannelWarningSend(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<CharityDonation, UserConfiguration>()
        {
            TestName = CreateTestName("charity-donation"),
            CreateSpecification = userConfig => new CharityDonation(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<CharityCampaignStart, UserConfiguration>()
        {
            TestName = CreateTestName("charity-campaign-start"),
            CreateSpecification = userConfig => new CharityCampaignStart(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<CharityCampaignProgress, UserConfiguration>()
        {
            TestName = CreateTestName("charity-campaign-progress"),
            CreateSpecification = userConfig => new CharityCampaignProgress(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<CharityCampaignStop, UserConfiguration>()
        {
            TestName = CreateTestName("charity-campaign-stop"),
            CreateSpecification = userConfig => new CharityCampaignStop(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ConduitShardDisabled, ClientConfiguration>()
        {
            TestName = CreateTestName("conduit-shard-disabled"),
            CreateSpecification = clientConfig => new ConduitShardDisabled(clientConfig.ClientId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<DropEntitlementGrant, OrganizationConfiguration>()
        {
            TestName = CreateTestName("drop-entitlement-grant"),
            CreateSpecification = organizationConfig => new DropEntitlementGrant(organizationConfig.OrganizationId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ExtensionBitsTransactionCreate, ExtensionConfiguration>()
        {
            TestName = CreateTestName("extension-bits-transaction-create"),
            CreateSpecification = extensionConfig => new ExtensionBitsTransactionCreate(extensionConfig.ExtensionId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<GoalBegin, UserConfiguration>()
        {
            TestName = CreateTestName("goal-begin"),
            CreateSpecification = userConfig => new GoalBegin(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<GoalProgress, UserConfiguration>()
        {
            TestName = CreateTestName("goal-progress"),
            CreateSpecification = userConfig => new GoalProgress(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<GoalEnd, UserConfiguration>()
        {
            TestName = CreateTestName("goal-end"),
            CreateSpecification = userConfig => new GoalEnd(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<HypeTrainBegin, UserConfiguration>()
        {
            TestName = CreateTestName("hype-train-begin"),
            CreateSpecification = userConfig => new HypeTrainBegin(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<HypeTrainProgress, UserConfiguration>()
        {
            TestName = CreateTestName("hype-train-progress"),
            CreateSpecification = userConfig => new HypeTrainProgress(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<HypeTrainEnd, UserConfiguration>()
        {
            TestName = CreateTestName("hype-train-end"),
            CreateSpecification = userConfig => new HypeTrainEnd(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ShieldModeBegin, UserConfiguration>()
        {
            TestName = CreateTestName("shield-mode-begin"),
            CreateSpecification = userConfig => new ShieldModeBegin(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ShieldModeEnd, UserConfiguration>()
        {
            TestName = CreateTestName("shield-mode-end"),
            CreateSpecification = userConfig => new ShieldModeEnd(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ShoutoutCreate, UserConfiguration>()
        {
            TestName = CreateTestName("shoutout-create"),
            CreateSpecification = userConfig => new ShoutoutCreate(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<ShoutoutReceived, UserConfiguration>()
        {
            TestName = CreateTestName("shoutout-received"),
            CreateSpecification = userConfig => new ShoutoutReceived(userConfig.UserId, userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<StreamOnline, UserConfiguration>()
        {
            TestName = CreateTestName("stream-online"),
            CreateSpecification = userConfig => new StreamOnline(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<StreamOffline, UserConfiguration>()
        {
            TestName = CreateTestName("stream-offline"),
            CreateSpecification = userConfig => new StreamOffline(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<UserAuthorizationGrant, ClientConfiguration>()
        {
            TestName = CreateTestName("user-authorization-grant"),
            CreateSpecification = clientConfig => new UserAuthorizationGrant(clientConfig.ClientId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<UserAuthorizationRevoke, ClientConfiguration>()
        {
            TestName = CreateTestName("user-authorization-revoke"),
            CreateSpecification = clientConfig => new UserAuthorizationRevoke(clientConfig.ClientId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<UserUpdate, UserConfiguration>()
        {
            TestName = CreateTestName("user-update"),
            CreateSpecification = userConfig => new UserUpdate(userConfig.UserId)
        }),
        new TheoryDataRow<EventSubTest>(new EventSubTest<WhisperReceived, UserConfiguration>()
        {
            TestName = CreateTestName("whisper-received"),
            CreateSpecification = userConfig => new WhisperReceived(userConfig.UserId)
        })
        ];

    public IEnumerator<TheoryDataRow<EventSubTest>> GetEnumerator() => Data.AsEnumerable().GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
