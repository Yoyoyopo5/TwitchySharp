using System.Collections;
using System.Collections.Immutable;
using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.Api.Helix.EventSub.Subscriptions;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.EventSub;

public static class EventSubTestRegistry
{
    private readonly static Dictionary<EventSubSubscriptionType, EventSubTest> _registry
        = new Dictionary<EventSubSubscriptionType, EventSubTest>()
        .Register<AutomodMessageHold, UserConfiguration>(userConfig => new AutomodMessageHold(userConfig.UserId, userConfig.UserId))
        .Register<AutomodMessageHoldV2, UserConfiguration>(userConfig => new AutomodMessageHoldV2(userConfig.UserId, userConfig.UserId))
        .Register<AutomodMessageUpdate, UserConfiguration>(userConfig => new AutomodMessageUpdate(userConfig.UserId, userConfig.UserId))
        .Register<AutomodMessageUpdateV2, UserConfiguration>(userConfig => new AutomodMessageUpdateV2(userConfig.UserId, userConfig.UserId))
        .Register<AutomodSettingsUpdate, UserConfiguration>(userConfig => new AutomodSettingsUpdate(userConfig.UserId, userConfig.UserId))
        .Register<AutomodTermsUpdate, UserConfiguration>(userConfig => new AutomodTermsUpdate(userConfig.UserId, userConfig.UserId))
        .Register<ChannelBitsUse, UserConfiguration>(userConfig => new ChannelBitsUse(userConfig.UserId))
        .Register<ChannelUpdate, UserConfiguration>(userConfig => new ChannelUpdate(userConfig.UserId))
        .Register<ChannelFollow, UserConfiguration>(userConfig => new ChannelFollow(userConfig.UserId, userConfig.UserId))
        .Register<ChannelAdBreakBegin, UserConfiguration>(userConfig => new ChannelAdBreakBegin(userConfig.UserId))
        .Register<ChannelChatClear, UserConfiguration>(userConfig => new ChannelChatClear(userConfig.UserId, userConfig.UserId))
        .Register<ChannelChatClearUserMessages, UserConfiguration>(userConfig => new ChannelChatClearUserMessages(userConfig.UserId, userConfig.UserId))
        .Register<ChannelChatMessage, UserConfiguration>(userConfig => new ChannelChatMessage(userConfig.UserId, userConfig.UserId))
        .Register<ChannelChatMessageDelete, UserConfiguration>(userConfig => new ChannelChatMessageDelete(userConfig.UserId, userConfig.UserId))
        .Register<ChannelChatNotification, UserConfiguration>(userConfig => new ChannelChatNotification(userConfig.UserId, userConfig.UserId))
        .Register<ChannelChatSettingsUpdate, UserConfiguration>(userConfig => new ChannelChatSettingsUpdate(userConfig.UserId, userConfig.UserId))
        .Register<ChannelChatUserMessageHold, UserConfiguration>(userConfig => new ChannelChatUserMessageHold(userConfig.UserId, userConfig.UserId))
        .Register<ChannelChatUserMessageUpdate, UserConfiguration>(userConfig => new ChannelChatUserMessageUpdate(userConfig.UserId, userConfig.UserId))
        .Register<ChannelSharedChatSessionBegin, UserConfiguration>(userConfig => new ChannelSharedChatSessionBegin(userConfig.UserId))
        .Register<ChannelSharedChatSessionUpdate, UserConfiguration>(userConfig => new ChannelSharedChatSessionUpdate(userConfig.UserId))
        .Register<ChannelSharedChatSessionEnd, UserConfiguration>(userConfig => new ChannelSharedChatSessionEnd(userConfig.UserId))
        .Register<ChannelSubscribe, UserConfiguration>(userConfig => new ChannelSubscribe(userConfig.UserId))
        .Register<ChannelSubscriptionEnd, UserConfiguration>(userConfig => new ChannelSubscriptionEnd(userConfig.UserId))
        .Register<ChannelSubscriptionGift, UserConfiguration>(userConfig => new ChannelSubscriptionGift(userConfig.UserId))
        .Register<ChannelSubscriptionMessage, UserConfiguration>(userConfig => new ChannelSubscriptionMessage(userConfig.UserId))
        .Register<ChannelCheer, UserConfiguration>(userConfig => new ChannelCheer(userConfig.UserId))
        .Register<ChannelRaid, UserConfiguration>(userConfig => new ChannelRaid(userConfig.UserId))
        .Register<ChannelBan, UserConfiguration>(userConfig => new ChannelBan(userConfig.UserId))
        .Register<ChannelUnban, UserConfiguration>(userConfig => new ChannelUnban(userConfig.UserId))
        .Register<ChannelUnbanRequestCreate, UserConfiguration>(userConfig => new ChannelUnbanRequestCreate(userConfig.UserId, userConfig.UserId))
        .Register<ChannelUnbanRequestResolve, UserConfiguration>(userConfig => new ChannelUnbanRequestResolve(userConfig.UserId, userConfig.UserId))
        .Register<ChannelModerate, UserConfiguration>(userConfig => new ChannelModerate(userConfig.UserId, userConfig.UserId))
        .Register<ChannelModerateV2, UserConfiguration>(userConfig => new ChannelModerateV2(userConfig.UserId, userConfig.UserId))
        .Register<ChannelModeratorAdd, UserConfiguration>(userConfig => new ChannelModeratorAdd(userConfig.UserId))
        .Register<ChannelModeratorRemove, UserConfiguration>(userConfig => new ChannelModeratorRemove(userConfig.UserId))
        .Register<ChannelGuestStarSessionBegin, UserConfiguration>(userConfig => new ChannelGuestStarSessionBegin(userConfig.UserId, userConfig.UserId))
        .Register<ChannelGuestStarSessionEnd, UserConfiguration>(userConfig => new ChannelGuestStarSessionEnd(userConfig.UserId, userConfig.UserId))
        .Register<ChannelGuestStarGuestUpdate, UserConfiguration>(userConfig => new ChannelGuestStarGuestUpdate(userConfig.UserId, userConfig.UserId))
        .Register<ChannelGuestStarSettingsUpdate, UserConfiguration>(userConfig => new ChannelGuestStarSettingsUpdate(userConfig.UserId, userConfig.UserId))
        .Register<ChannelPointsAutomaticRewardRedemptionAdd, UserConfiguration>(userConfig => new ChannelPointsAutomaticRewardRedemptionAdd(userConfig.UserId))
        .Register<ChannelPointsAutomaticRewardRedemptionAddV2, UserConfiguration>(userConfig => new ChannelPointsAutomaticRewardRedemptionAddV2(userConfig.UserId))
        .Register<ChannelPointsCustomRewardAdd, UserConfiguration>(userConfig => new ChannelPointsCustomRewardAdd(userConfig.UserId))
        .Register<ChannelPointsCustomRewardUpdate, UserConfiguration>(userConfig => new ChannelPointsCustomRewardUpdate(userConfig.UserId))
        .Register<ChannelPointsCustomRewardRemove, UserConfiguration>(userConfig => new ChannelPointsCustomRewardRemove(userConfig.UserId))
        .Register<ChannelPointsCustomRewardRedemptionAdd, UserConfiguration>(userConfig => new ChannelPointsCustomRewardRedemptionAdd(userConfig.UserId))
        .Register<ChannelPointsCustomRewardRedemptionUpdate, UserConfiguration>(userConfig => new ChannelPointsCustomRewardRedemptionUpdate(userConfig.UserId))
        .Register<ChannelPollBegin, UserConfiguration>(userConfig => new ChannelPollBegin(userConfig.UserId))
        .Register<ChannelPollProgress, UserConfiguration>(userConfig => new ChannelPollProgress(userConfig.UserId))
        .Register<ChannelPollEnd, UserConfiguration>(userConfig => new ChannelPollEnd(userConfig.UserId))
        .Register<ChannelPredictionBegin, UserConfiguration>(userConfig => new ChannelPredictionBegin(userConfig.UserId))
        .Register<ChannelPredictionProgress, UserConfiguration>(userConfig => new ChannelPredictionProgress(userConfig.UserId))
        .Register<ChannelPredictionLock, UserConfiguration>(userConfig => new ChannelPredictionLock(userConfig.UserId))
        .Register<ChannelPredictionEnd, UserConfiguration>(userConfig => new ChannelPredictionEnd(userConfig.UserId))
        .Register<ChannelSuspiciousUserMessage, UserConfiguration>(userConfig => new ChannelSuspiciousUserMessage(userConfig.UserId, userConfig.UserId))
        .Register<ChannelSuspiciousUserUpdate, UserConfiguration>(userConfig => new ChannelSuspiciousUserUpdate(userConfig.UserId, userConfig.UserId))
        .Register<ChannelVipAdd, UserConfiguration>(userConfig => new ChannelVipAdd(userConfig.UserId))
        .Register<ChannelVipRemove, UserConfiguration>(userConfig => new ChannelVipRemove(userConfig.UserId))
        .Register<ChannelWarningAcknowledgement, UserConfiguration>(userConfig => new ChannelWarningAcknowledgement(userConfig.UserId, userConfig.UserId))
        .Register<ChannelWarningSend, UserConfiguration>(userConfig => new ChannelWarningSend(userConfig.UserId, userConfig.UserId))
        .Register<CharityDonation, UserConfiguration>(userConfig => new CharityDonation(userConfig.UserId))
        .Register<CharityCampaignStart, UserConfiguration>(userConfig => new CharityCampaignStart(userConfig.UserId))
        .Register<CharityCampaignProgress, UserConfiguration>(userConfig => new CharityCampaignProgress(userConfig.UserId))
        .Register<CharityCampaignStop, UserConfiguration>(userConfig => new CharityCampaignStop(userConfig.UserId))
        .Register<ConduitShardDisabled, ClientConfiguration>(clientConfig => new ConduitShardDisabled(clientConfig.ClientId))
        .Register<DropEntitlementGrant, OrganizationConfiguration>(organizationConfig => new DropEntitlementGrant(organizationConfig.OrganizationId))
        .Register<ExtensionBitsTransactionCreate, ExtensionConfiguration>(extensionConfig => new ExtensionBitsTransactionCreate(extensionConfig.ExtensionId))
        .Register<GoalBegin, UserConfiguration>(userConfig => new GoalBegin(userConfig.UserId))
        .Register<GoalProgress, UserConfiguration>(userConfig => new GoalProgress(userConfig.UserId))
        .Register<GoalEnd, UserConfiguration>(userConfig => new GoalEnd(userConfig.UserId))
        .Register<HypeTrainBegin, UserConfiguration>(userConfig => new HypeTrainBegin(userConfig.UserId))
        .Register<HypeTrainProgress, UserConfiguration>(userConfig => new HypeTrainProgress(userConfig.UserId))
        .Register<HypeTrainEnd, UserConfiguration>(userConfig => new HypeTrainEnd(userConfig.UserId))
        .Register<ShieldModeBegin, UserConfiguration>(userConfig => new ShieldModeBegin(userConfig.UserId, userConfig.UserId))
        .Register<ShieldModeEnd, UserConfiguration>(userConfig => new ShieldModeEnd(userConfig.UserId, userConfig.UserId))
        .Register<ShoutoutCreate, UserConfiguration>(userConfig => new ShoutoutCreate(userConfig.UserId, userConfig.UserId))
        .Register<ShoutoutReceived, UserConfiguration>(userConfig => new ShoutoutReceived(userConfig.UserId, userConfig.UserId))
        .Register<StreamOnline, UserConfiguration>(userConfig => new StreamOnline(userConfig.UserId))
        .Register<StreamOffline, UserConfiguration>(userConfig => new StreamOffline(userConfig.UserId))
        .Register<UserAuthorizationGrant, ClientConfiguration>(clientConfig => new UserAuthorizationGrant(clientConfig.ClientId))
        .Register<UserAuthorizationRevoke, ClientConfiguration>(clientConfig => new UserAuthorizationRevoke(clientConfig.ClientId))
        .Register<UserUpdate, UserConfiguration>(userConfig => new UserUpdate(userConfig.UserId))
        .Register<WhisperReceived, UserConfiguration>(userConfig => new WhisperReceived(userConfig.UserId));

    private static Dictionary<EventSubSubscriptionType, EventSubTest> Register<TSpecification, TRequiredIdentity>(
        this Dictionary<EventSubSubscriptionType, EventSubTest> registry,
        Func<TRequiredIdentity, TSpecification> create
        )
        where TSpecification : EventSubSubscriptionTypeSpecification, IConditionConstructable<TSpecification>
        where TRequiredIdentity : ITestIdentity
    {
        registry.Add(TSpecification.SubscriptionType, new EventSubTest<TRequiredIdentity, TSpecification>()
        {
            CreateSpecification = create
        });
        return registry;
    }

    public static EventSubTest Get(EventSubSubscriptionType subscriptionType)
        => _registry[subscriptionType];

    public static EventSubTest Get<TSpecification>()
        where TSpecification : EventSubSubscriptionTypeSpecification, IConditionConstructable<TSpecification>
        => _registry[TSpecification.SubscriptionType];
}

public class EventSubTestProvider : IEnumerable<TheoryDataRow<EventSubTestRow>>
{
    public readonly static IEnumerable<TheoryDataRow<EventSubTestRow>> Data = [
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = AutomodMessageHold.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = AutomodMessageHoldV2.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = AutomodMessageUpdate.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = AutomodMessageUpdateV2.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = AutomodSettingsUpdate.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = AutomodTermsUpdate.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelBitsUse.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelUpdate.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelFollow.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelAdBreakBegin.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelChatClear.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelChatClearUserMessages.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelChatMessage.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelChatMessageDelete.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelChatNotification.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelChatSettingsUpdate.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelChatUserMessageHold.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelChatUserMessageUpdate.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelSharedChatSessionBegin.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelSharedChatSessionUpdate.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelSharedChatSessionEnd.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelSubscribe.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelSubscriptionEnd.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelSubscriptionGift.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelSubscriptionMessage.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelCheer.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelRaid.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelBan.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelUnban.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelUnbanRequestCreate.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelUnbanRequestResolve.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelModerate.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelModerateV2.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelModeratorAdd.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelModeratorRemove.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelGuestStarSessionBegin.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelGuestStarSessionEnd.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelGuestStarGuestUpdate.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelGuestStarSettingsUpdate.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelPointsAutomaticRewardRedemptionAdd.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelPointsAutomaticRewardRedemptionAddV2.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelPointsCustomRewardAdd.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelPointsCustomRewardUpdate.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelPointsCustomRewardRemove.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelPointsCustomRewardRedemptionAdd.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelPointsCustomRewardRedemptionUpdate.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelPollBegin.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelPollProgress.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelPollEnd.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelPredictionBegin.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelPredictionProgress.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelPredictionLock.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelPredictionEnd.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelSuspiciousUserMessage.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelSuspiciousUserUpdate.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelVipAdd.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelVipRemove.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelWarningAcknowledgement.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ChannelWarningSend.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = CharityDonation.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = CharityCampaignStart.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = CharityCampaignProgress.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = CharityCampaignStop.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ConduitShardDisabled.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = DropEntitlementGrant.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ExtensionBitsTransactionCreate.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = GoalBegin.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = GoalProgress.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = GoalEnd.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = HypeTrainBegin.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = HypeTrainProgress.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = HypeTrainEnd.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ShieldModeBegin.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ShieldModeEnd.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ShoutoutCreate.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = ShoutoutReceived.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = StreamOnline.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = StreamOffline.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = UserAuthorizationGrant.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = UserAuthorizationRevoke.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = UserUpdate.SubscriptionType
        }),
        new TheoryDataRow<EventSubTestRow>(new()
        {
            SubscriptionType = WhisperReceived.SubscriptionType
        })
        ];

    public IEnumerator<TheoryDataRow<EventSubTestRow>> GetEnumerator() => Data.AsEnumerable().GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
