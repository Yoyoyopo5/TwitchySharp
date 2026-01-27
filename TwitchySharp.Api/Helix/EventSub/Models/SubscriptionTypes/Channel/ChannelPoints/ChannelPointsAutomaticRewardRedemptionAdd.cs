using System.Collections.Generic;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A viewer has redeemed an automatic channel points reward on the specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadRedemptions"/> or <see cref="Scope.ChannelManageRedemptions"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The broadcaster user ID for the channel you want to receive channel points reward add notifications for.</param>
public sealed record ChannelPointsAutomaticRewardRedemptionAdd(UserId BroadcasterUserId)
    : IEventSubSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAdd;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
