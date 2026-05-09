using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub.Channel;
/// <summary>
/// An unban request has been resolved.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadUnbanRequests"/> or <see cref="Scope.ModeratorManageUnbanRequests"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) you want to get chat unban request resolution notifications for.</param>
/// <param name="ModeratorUserId">
/// The user id of the broadcaster or a moderator in the broadcaster's chat.
/// This user must have created a user access token including <see cref="Scope.ModeratorReadUnbanRequests"/> or <see cref="Scope.ModeratorManageUnbanRequests"/> for this application.
/// </param>
public sealed record ChannelUnbanRequestResolve(UserId ModeratorUserId, UserId BroadcasterUserId)
    : IUserAuthorizedSubscriptionTypeSpecification
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelUnbanRequestResolve;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new ConditionKey("moderator_user_id");
    public IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ModeratorReadUnbanRequests, Scope.ModeratorManageUnbanRequests);
    public UserId AuthorizingUser => ModeratorUserId;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("moderator_user_id"), ModeratorUserId)
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
