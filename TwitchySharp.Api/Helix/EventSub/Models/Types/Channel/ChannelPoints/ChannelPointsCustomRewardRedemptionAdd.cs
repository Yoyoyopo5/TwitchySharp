using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.ChannelPoints;
/// <summary>
/// A viewer has redeemed a custom channel points reward on the specified channel.
/// </summary>
/// <param name="BroadcasterUserId">The broadcaster user ID for the channel you want to receive channel points custom reward redemption add notifications for.</param>
/// <param name="RewardId">Optional. Specify a reward id to only receive notifications for a specific reward.</param>
public sealed record ChannelPointsCustomRewardRedemptionAdd(string BroadcasterUserId, string? RewardId = null)
    : IEventSubSubscriptionType
{
    public string Type => "channel.channel_points_custom_reward_redemption.add";
    public string Version => "1";

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId)
            .Set("reward_id", RewardId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
