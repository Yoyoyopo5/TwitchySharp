using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;

/// <summary>
/// A viewer has redeemed a custom Power-up on the specified channel.
/// </summary>
/// <remarks>
/// <para>
/// Requires a user access token that includes <see cref="Scope.BitsRead"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelcustom_power_up_redemptionadd">channel.custom_power_up_redemption.add</see> for more information.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) to get notifications for.</param>
/// <param name="RewardId">
/// The id of the custom Bits power-up to get notifications for.
/// If <see langword="null"/>, redemptions for any custom power-up will be delivered.
/// </param>
public sealed record ChannelCustomPowerUpRedemptionAdd(UserId BroadcasterUserId, CustomBitsPowerUpId? RewardId = null)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelCustomPowerUpRedemptionAdd>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelCustomPowerUpRedemptionAdd;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelCustomPowerUpRedemptionAdd;
    public override EventSubSubscriptionAuthenticationContext.UserAuthorized AuthenticationContext { get; } = new()
    {
        Identity = new TwitchIdentity.User(BroadcasterUserId),
        ValidScopes = ImmutableHashSet.Create(Scope.BitsRead)
    };

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new("broadcaster_user_id"), BroadcasterUserId.Value)
            .Set(new("reward_id"), RewardId?.Value);

    public static Result<ChannelCustomPowerUpRedemptionAdd> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId broadcasterUserId, value => new(value))
            .GetValue(new("reward_id"), out CustomBitsPowerUpId? rewardId, value => new(value))
            .Map(_ => new ChannelCustomPowerUpRedemptionAdd(broadcasterUserId, rewardId));

}
