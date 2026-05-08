using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub.Channel;
/// <summary>
/// A running Guest Star session has ended.
/// This can mean it was ended by the host, or automatically by the system.
/// </summary>
/// <remarks>
/// Requires a user access token that includes (one of) <see cref="Scope.ChannelReadGuestStar"/>, <see cref="Scope.ChannelManageGuestStar"/>, <see cref="Scope.ModeratorReadGuestStar"/>, or <see cref="Scope.ModeratorManageGuestStar"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) hosting the Guest Star Session.</param>
/// <param name="ModeratorUserId">The user id of the broadcaster or a moderator of the specified broadcaster.</param>
public sealed record ChannelGuestStarSessionEnd(UserId BroadcasterUserId, UserId ModeratorUserId)
    : IUserAuthorizedSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelGuestStarSessionEnd;
    public ConditionKey AuthorizingUserConditionKey => new("moderator_user_id");
    public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ChannelReadGuestStar, Scope.ChannelManageGuestStar, Scope.ModeratorReadGuestStar, Scope.ModeratorManageGuestStar);

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new("broadcaster_user_id"), BroadcasterUserId)
            .Set(new("moderator_user_id"), ModeratorUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
