using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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
public class EventSubTestTypes : TheoryData<string>
{
    /// <summary>
    /// Types available to the testing framework. 
    /// Be sure to include the corresponding type instance in <see cref="EventSubFixture.GenerateTypeMapAsync"/>.
    /// </summary>
    private IEnumerable<string> _subscriptionTypeNames = new string[]
    {
        nameof(AutomodMessageHoldV2),
        nameof(AutomodMessageUpdateV2),
        nameof(AutomodSettingsUpdate),
        nameof(AutomodTermsUpdate),
        nameof(ChannelAdBreakBegin),
        nameof(ChannelPointsAutomaticRewardRedemption),
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
        nameof(HypeTrainBegin),
        nameof(HypeTrainEnd),
        nameof(HypeTrainProgress),
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
    };

    public EventSubTestTypes()
    {
        foreach (string subscriptionType in _subscriptionTypeNames)
            Add(subscriptionType);
    }
}
