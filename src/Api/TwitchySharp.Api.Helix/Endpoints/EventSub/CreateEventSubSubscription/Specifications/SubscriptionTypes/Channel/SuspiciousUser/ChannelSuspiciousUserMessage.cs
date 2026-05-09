using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub.Channel;
/// <summary>
/// A chat message has been sent by a suspicious user.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadSuspiciousUsers"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) you want to get suspicious user message notifications for.</param>
/// <param name="ModeratorUserId">
/// The user id of the broadcaster or a moderator in the broadcaster's channel.
/// This user must have created a user access token that includes <see cref="Scope.ModeratorReadSuspiciousUsers"/> for this application.
/// </param>
public sealed record ChannelSuspiciousUserMessage(UserId BroadcasterUserId, UserId ModeratorUserId)
    : IUserAuthorizedSubscriptionTypeSpecification
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelSuspiciousUserMessage;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new ConditionKey("moderator_user_id");
    public IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ModeratorReadSuspiciousUsers);
    public UserId AuthorizingUser => ModeratorUserId;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("moderator_user_id"), ModeratorUserId)
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
