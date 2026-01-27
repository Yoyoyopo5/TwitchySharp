using System.Collections.Generic;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.EventSub.Models.SubscriptionTypes.Channel.ChannelPoints;

/// <summary>
/// A viewer has redeemed an automatic channel points reward on the specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadRedemptions"/> or <see cref="Scope.ChannelManageRedemptions"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The broadcaster user ID for the channel you want to receive Channel Points Reward Add V2 notifications for.</param>
public sealed record ChannelPointsAutomaticRewardRedemptionAddV2(UserId BroadcasterUserId)
    : IEventSubSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAddV2;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
