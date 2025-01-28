using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Automod.Message;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Automod.Settings;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Automod.Terms;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.AdBreak;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.ChannelPoints;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.CharityCampaign;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.Chat;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.ChatSettings;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.Goals;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.GuestStar;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.HypeTrain;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.Moderator;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.Polls;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.Predictions;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.SharedChat;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.ShieldMode;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.Shoutout;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.Subscription;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.SuspiciousUser;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.UnbanRequest;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.Vips;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.Warnings;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Conduit;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Drops;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Extension;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Stream;
using TwitchySharp.Api.Helix.EventSub.Models.Types.User;
using TwitchySharp.Api.Helix.EventSub.Models.Types.User.Authorization;
using TwitchySharp.Api.Helix.EventSub.Models.Types.User.Whisper;

namespace TwitchySharp.Api.Tests.Integration.Helix.EventSub;
/// <summary>
/// Handles one-time setup of subscription types.
/// </summary>
public class EventSubFixture : HelixFixture, IAsyncLifetime
{
    private IReadOnlyDictionary<string, IEventSubSubscriptionType> _subscriptionTypes = ImmutableDictionary<string, IEventSubSubscriptionType>.Empty;

    public async Task InitializeAsync()
        => _subscriptionTypes = await GenerateTypeMapAsync();

    public Task DisposeAsync()
        => Task.CompletedTask;

    private async ValueTask<IReadOnlyDictionary<string, IEventSubSubscriptionType>> GenerateTypeMapAsync()
    {
        string broadcasterId = await GetUserIdFromAccessTokenAsync();

        return new Dictionary<string, IEventSubSubscriptionType>
        {
            { nameof(AutomodMessageHoldV2), new AutomodMessageHoldV2(broadcasterId, broadcasterId) },
            { nameof(AutomodMessageUpdateV2), new AutomodMessageUpdateV2(broadcasterId, broadcasterId) },
            { nameof(AutomodSettingsUpdate), new AutomodSettingsUpdate(broadcasterId, broadcasterId) },
            { nameof(AutomodTermsUpdate), new AutomodTermsUpdate(broadcasterId, broadcasterId) },
            { nameof(ChannelAdBreakBegin), new ChannelAdBreakBegin(broadcasterId) },
            { nameof(ChannelPointsAutomaticRewardRedemption), new ChannelPointsAutomaticRewardRedemption(broadcasterId) },
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
            { nameof(ChannelChatSettingsUpdate), new ChannelChatSettingsUpdate(broadcasterId, broadcasterId) },
            { nameof(GoalBegin), new GoalBegin(broadcasterId) },
            { nameof(GoalEnd), new GoalEnd(broadcasterId) },
            { nameof(GoalProgress), new GoalProgress(broadcasterId) },
            { nameof(ChannelGuestStarGuestUpdate), new ChannelGuestStarGuestUpdate(broadcasterId, broadcasterId) },
            { nameof(ChannelGuestStarSessionBegin), new ChannelGuestStarSessionBegin(broadcasterId, broadcasterId) },
            { nameof(ChannelGuestStarSessionEnd), new ChannelGuestStarSessionEnd(broadcasterId, broadcasterId) },
            { nameof(ChannelGuestStarSettingsUpdate), new ChannelGuestStarSettingsUpdate(broadcasterId, broadcasterId) },
            { nameof(HypeTrainBegin), new HypeTrainBegin(broadcasterId) },
            { nameof(HypeTrainEnd), new HypeTrainEnd(broadcasterId) },
            { nameof(HypeTrainProgress), new HypeTrainProgress(broadcasterId) },
            { nameof(ChannelModeratorAdd), new ChannelModeratorAdd(broadcasterId) },
            { nameof(ChannelModeratorRemove), new ChannelModeratorRemove(broadcasterId) },
            { nameof(ChannelPollBegin), new ChannelPollEnd(broadcasterId) },
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
            { nameof(ConduitShardDisabled), new ConduitShardDisabled(broadcasterId) },
            { nameof(DropEntitlementGrant), new DropEntitlementGrant(broadcasterId) },
            { nameof(ExtensionBitsTransactionCreate), new ExtensionBitsTransactionCreate(broadcasterId) },
            { nameof(StreamOffline), new StreamOffline(broadcasterId) },
            { nameof(StreamOnline), new StreamOnline(broadcasterId) },
            { nameof(UserAuthorizationGrant), new UserAuthorizationGrant(broadcasterId) },
            { nameof(UserAuthorizationRevoke), new UserAuthorizationRevoke(broadcasterId) },
            { nameof(WhisperReceived), new WhisperReceived(broadcasterId) },
            { nameof(UserUpdate), new UserUpdate(broadcasterId) },
        };
    }

    public IEventSubSubscriptionType GetSubscriptionType(string subscriptionTypeName)
        => _subscriptionTypes[subscriptionTypeName];
}
