using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub.Channel;
/// <summary>
/// A user acknowledges a warning. Broadcasters and moderators can see the warning's details.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadWarnings"/> or <see cref="Scope.ModeratorManageWarnings"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) to receive warning acknowledgement notifications for.</param>
/// <param name="ModeratorUserId">
/// The user id of the broadcaster or a moderator in a broadcaster's chat.
/// This user must have created a user access token that includes <see cref="Scope.ModeratorReadWarnings"/> or <see cref="Scope.ModeratorManageWarnings"/> for this application.
/// </param>
public sealed record ChannelWarningAcknowledgement(UserId BroadcasterUserId, UserId ModeratorUserId)
    : IUserAuthorizedSubscriptionTypeSpecification
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelWarningAcknowledgement;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new ConditionKey("moderator_user_id");
    public IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ModeratorReadWarnings, Scope.ModeratorManageWarnings);
    public UserId AuthorizingUser => ModeratorUserId;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId)
            .Set(new ConditionKey("moderator_user_id"), ModeratorUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
