using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub.Channel;
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
    : IUserAuthorizedSubscriptionTypeSpecification
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelFollow;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new("moderator_user_id");
    public IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ModeratorReadFollowers);
    public UserId AuthorizingUser => ModeratorUserId;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new("broadcaster_user_id"), BroadcasterUserId)
            .Set(new("moderator_user_id"), ModeratorUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
