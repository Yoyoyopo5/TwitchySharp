using System.Collections.Immutable;
using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.EventSub;
/// <summary>
/// Handles one-time setup of subscription types.
/// </summary>
public class EventSubFixture : TwitchClientFixture, IAsyncLifetime
{
    private IReadOnlyDictionary<string, IEventSubSubscriptionType> _subscriptionTypes = ImmutableDictionary<string, IEventSubSubscriptionType>.Empty;

    public async ValueTask InitializeAsync()
        => _subscriptionTypes = await GenerateTypeMapAsync();

    public ValueTask DisposeAsync()
        => ValueTask.CompletedTask;

    private async ValueTask<IReadOnlyDictionary<string, IEventSubSubscriptionType>> GenerateTypeMapAsync()
    {
        OrganizationId organizationId = new(string.Empty); // Don't have one of these to test with.
        UserId broadcasterId = UserIdentity.UserId;
        ClientId clientId = Client.Id;

        return new Dictionary<string, IEventSubSubscriptionType>
        {
            { nameof(AutomodMessageHoldV2), new AutomodMessageHoldV2(broadcasterId, broadcasterId) },
            { nameof(AutomodMessageUpdateV2), new AutomodMessageUpdateV2(broadcasterId, broadcasterId) },
            { nameof(AutomodSettingsUpdate), new AutomodSettingsUpdate(broadcasterId, broadcasterId) },
            { nameof(AutomodTermsUpdate), new AutomodTermsUpdate(broadcasterId, broadcasterId) },
            { nameof(ChannelAdBreakBegin), new ChannelAdBreakBegin(broadcasterId) },
            { nameof(ChannelPointsAutomaticRewardRedemptionAdd), new ChannelPointsAutomaticRewardRedemptionAdd(broadcasterId) },
            { nameof(ChannelPointsCustomRewardAdd), new ChannelPointsCustomRewardAdd(broadcasterId) },
            { nameof(ChannelPointsCustomRewardRedemptionAdd), new ChannelPointsCustomRewardRedemptionAdd(broadcasterId) },
            { nameof(ChannelPointsCustomRewardRedemptionUpdate), new ChannelPointsCustomRewardRedemptionUpdate(broadcasterId) },
            { nameof(ChannelPointsCustomRewardRemove), new ChannelPointsCustomRewardRemove(broadcasterId) },
            { nameof(ChannelPointsCustomRewardUpdate), new ChannelPointsCustomRewardUpdate(broadcasterId) },
            { nameof(CharityCampaignProgress), new CharityCampaignProgress(broadcasterId) },
            { nameof(CharityCampaignStart), new CharityCampaignStart(broadcasterId) },
            { nameof(CharityCampaignStop), new CharityCampaignStop(broadcasterId) },
            { nameof(CharityDonation), new CharityDonation(broadcasterId) },
            { nameof(ChannelChatClear), new ChannelChatClear(broadcasterId, broadcasterId) },
            { nameof(ChannelChatClearUserMessages), new ChannelChatClearUserMessages(broadcasterId, broadcasterId) },
            { nameof(ChannelChatMessage), new ChannelChatMessage(broadcasterId, broadcasterId) },
            { nameof(ChannelChatMessageDelete), new ChannelChatMessageDelete(broadcasterId, broadcasterId) },
            { nameof(ChannelChatNotification), new ChannelChatNotification(broadcasterId, broadcasterId) },
            { nameof(ChannelChatUserMessageHold), new ChannelChatUserMessageHold(broadcasterId, broadcasterId) },
            { nameof(ChannelChatUserMessageUpdate), new ChannelChatUserMessageUpdate(broadcasterId, broadcasterId) },
            { nameof(ChannelChatSettingsUpdate), new ChannelChatSettingsUpdate(broadcasterId, broadcasterId) },
            { nameof(GoalBegin), new GoalBegin(broadcasterId) },
            { nameof(GoalEnd), new GoalEnd(broadcasterId) },
            { nameof(GoalProgress), new GoalProgress(broadcasterId) },
            { nameof(ChannelGuestStarGuestUpdate), new ChannelGuestStarGuestUpdate(broadcasterId, broadcasterId) },
            { nameof(ChannelGuestStarSessionBegin), new ChannelGuestStarSessionBegin(broadcasterId, broadcasterId) },
            { nameof(ChannelGuestStarSessionEnd), new ChannelGuestStarSessionEnd(broadcasterId, broadcasterId) },
            { nameof(ChannelGuestStarSettingsUpdate), new ChannelGuestStarSettingsUpdate(broadcasterId, broadcasterId) },
            { nameof(HypeTrainBeginV2), new HypeTrainBeginV2(broadcasterId) },
            { nameof(HypeTrainEndV2), new HypeTrainEndV2(broadcasterId) },
            { nameof(HypeTrainProgressV2), new HypeTrainProgressV2(broadcasterId) },
            { nameof(ChannelModeratorAdd), new ChannelModeratorAdd(broadcasterId) },
            { nameof(ChannelModeratorRemove), new ChannelModeratorRemove(broadcasterId) },
            { nameof(ChannelPollBegin), new ChannelPollEnd(broadcasterId) },
            { nameof(ChannelPollEnd), new ChannelPollEnd(broadcasterId) },
            { nameof(ChannelPollProgress), new ChannelPollProgress(broadcasterId) },
            { nameof(ChannelPredictionBegin), new ChannelPredictionBegin(broadcasterId) },
            { nameof(ChannelPredictionEnd), new ChannelPredictionEnd(broadcasterId) },
            { nameof(ChannelPredictionLock), new ChannelPredictionLock(broadcasterId) },
            { nameof(ChannelPredictionProgress), new ChannelPredictionProgress(broadcasterId) },
            { nameof(ChannelSharedChatSessionBegin), new ChannelSharedChatSessionBegin(broadcasterId) },
            { nameof(ChannelSharedChatSessionEnd), new ChannelSharedChatSessionEnd(broadcasterId) },
            { nameof(ChannelSharedChatSessionUpdate), new ChannelSharedChatSessionUpdate(broadcasterId) },
            { nameof(ShieldModeBegin), new ShieldModeBegin(broadcasterId, broadcasterId) },
            { nameof(ShieldModeEnd), new ShieldModeEnd(broadcasterId, broadcasterId) },
            { nameof(ShoutoutCreate), new ShoutoutCreate(broadcasterId, broadcasterId) },
            { nameof(ShoutoutReceived), new ShoutoutReceived(broadcasterId, broadcasterId) },
            { nameof(ChannelSubscriptionEnd), new ChannelSubscriptionEnd(broadcasterId) },
            { nameof(ChannelSubscriptionGift), new ChannelSubscriptionGift(broadcasterId) },
            { nameof(ChannelSubscriptionMessage), new ChannelSubscriptionMessage(broadcasterId) },
            { nameof(ChannelSuspiciousUserMessage), new ChannelSuspiciousUserMessage(broadcasterId, broadcasterId) },
            { nameof(ChannelSuspiciousUserUpdate), new ChannelSuspiciousUserUpdate(broadcasterId, broadcasterId) },
            { nameof(ChannelUnbanRequestCreate), new ChannelUnbanRequestCreate(broadcasterId, broadcasterId) },
            { nameof(ChannelUnbanRequestResolve), new ChannelUnbanRequestResolve(broadcasterId, broadcasterId) },
            { nameof(ChannelVipAdd), new ChannelVipAdd(broadcasterId) },
            { nameof(ChannelVipRemove), new ChannelVipRemove(broadcasterId) },
            { nameof(ChannelWarningAcknowledgement), new ChannelWarningAcknowledgement(broadcasterId, broadcasterId) },
            { nameof(ChannelWarningSend), new ChannelWarningSend(broadcasterId, broadcasterId) },
            { nameof(ChannelBan), new ChannelBan(broadcasterId) },
            { nameof(ChannelCheer), new ChannelCheer(broadcasterId) },
            { nameof(ChannelFollow), new ChannelFollow(broadcasterId, broadcasterId) },
            { nameof(ChannelModerateV2), new ChannelModerateV2(broadcasterId, broadcasterId) },
            { nameof(ChannelRaid), new ChannelRaid(broadcasterId) },
            { nameof(ChannelSubscribe), new ChannelSubscribe(broadcasterId) },
            { nameof(ChannelUnban), new ChannelUnban(broadcasterId) },
            { nameof(ChannelUpdate), new ChannelUpdate(broadcasterId) },
            { nameof(ConduitShardDisabled), new ConduitShardDisabled(clientId) },
            { nameof(DropEntitlementGrant), new DropEntitlementGrant(organizationId) },
            { nameof(ExtensionBitsTransactionCreate), new ExtensionBitsTransactionCreate(clientId) },
            { nameof(StreamOffline), new StreamOffline(broadcasterId) },
            { nameof(StreamOnline), new StreamOnline(broadcasterId) },
            { nameof(UserAuthorizationGrant), new UserAuthorizationGrant(clientId) },
            { nameof(UserAuthorizationRevoke), new UserAuthorizationRevoke(clientId) },
            { nameof(WhisperReceived), new WhisperReceived(broadcasterId) },
            { nameof(UserUpdate), new UserUpdate(broadcasterId) },
        };
    }

    public IEventSubSubscriptionType GetSubscriptionType(string subscriptionTypeName)
        => _subscriptionTypes[subscriptionTypeName];
}
