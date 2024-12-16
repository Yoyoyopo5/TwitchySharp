using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.ChannelPoints;
/// <summary>
/// A custom channel points reward has been updated for the specified channel.
/// </summary>
/// <param name="BroadcasterUserId">The broadcaster user ID for the channel you want to receive channel points custom reward update notifications for.</param>
/// <param name="RewardId">Optional. Specify a reward id to only receive notifications for a specific reward.</param>
public sealed record ChannelPointsCustomRewardUpdate(string BroadcasterUserId, string? RewardId = null)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.CHANNEL_POINTS_CUSTOM_REWARD_UPDATE;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId)
            .Set("reward_id", RewardId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
