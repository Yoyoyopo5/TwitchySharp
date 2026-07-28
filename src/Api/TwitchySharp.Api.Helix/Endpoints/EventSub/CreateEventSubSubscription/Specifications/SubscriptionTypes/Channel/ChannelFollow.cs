using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A specified channel receives a follow.
/// </summary>
/// <remarks>
/// Requires a user access token with <see cref="Scope.ModeratorReadFollowers"/>.
/// The user who created the access token must be the same user as the <paramref name="ModeratorUserId"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster whose channel you want to get follow notifications for.</param>
/// <param name="ModeratorUserId">The ID of a moderator of the channel you want to get follow notifications for. If you have authorization from the broadcaster rather than a moderator, specify the broadcaster's user ID here.</param>
public sealed record ChannelFollow(UserId BroadcasterUserId, UserId ModeratorUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelFollow>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelFollow;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelFollow;
    public override IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ModeratorReadFollowers);
    public override TwitchIdentity Identity { get; } = new TwitchIdentity.User(ModeratorUserId);

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new("broadcaster_user_id"), BroadcasterUserId)
            .Set(new("moderator_user_id"), ModeratorUserId);
    public static Validation<ChannelFollow> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .GetRequiredValue(new("moderator_user_id"), out UserId ModeratorUserId, value => new(value))
            .Map(_ => new ChannelFollow(BroadcasterUserId, ModeratorUserId));
}
