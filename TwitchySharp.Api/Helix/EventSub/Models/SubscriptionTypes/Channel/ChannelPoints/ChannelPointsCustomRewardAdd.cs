using System.Collections.Generic;
using TwitchySharp.Shared.EventSub.Constants;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A custom channel points reward has been created for the specified channel.
/// </summary>
/// <param name="BroadcasterUserId">The broadcaster user ID for the channel you want to receive channel points custom reward add notifications for.</param>
public sealed record ChannelPointsCustomRewardAdd(string BroadcasterUserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.CHANNEL_POINTS_CUSTOM_REWARD_ADD;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
