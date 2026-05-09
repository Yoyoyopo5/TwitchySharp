using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub.Channel;
/// <summary>
/// The host began a new Guest Star session.
/// </summary>
/// <remarks>
/// Requires a user access token that includes (one of) <see cref="Scope.ChannelReadGuestStar"/>, <see cref="Scope.ChannelManageGuestStar"/>, <see cref="Scope.ModeratorReadGuestStar"/>, or <see cref="Scope.ModeratorManageGuestStar"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) hosting the Guest Star Session.</param>
/// <param name="ModeratorUserId">The user id of the broadcaster or a moderator of the specified broadcaster.</param>
public sealed record ChannelGuestStarSessionBegin(UserId BroadcasterUserId, UserId ModeratorUserId)
    : IUserAuthorizedSubscriptionTypeSpecification
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelGuestStarSessionBegin;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new("moderator_user_id");
    public IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ChannelReadGuestStar, Scope.ChannelManageGuestStar, Scope.ModeratorReadGuestStar, Scope.ModeratorManageGuestStar);
    public UserId AuthorizingUser => ModeratorUserId;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new("broadcaster_user_id"), BroadcasterUserId)
            .Set(new("moderator_user_id"), ModeratorUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
