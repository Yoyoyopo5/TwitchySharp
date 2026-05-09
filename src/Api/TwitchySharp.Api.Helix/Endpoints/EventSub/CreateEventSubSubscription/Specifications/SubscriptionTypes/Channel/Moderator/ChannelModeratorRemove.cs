using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// Moderator privileges were removed from a user on a specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token with <see cref="Scope.ModerationRead"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster for the channel you want to get moderator removal notifications for.
/// This user must have created a user access token that includes <see cref="Scope.ModerationRead"/> for your application.
/// </param>
public sealed record ChannelModeratorRemove(UserId BroadcasterUserId)
    : IUserAuthorizedSubscriptionTypeSpecification
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelModeratorRemove;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new ConditionKey("broadcaster_user_id");
    public IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ModerationRead);
    public UserId AuthorizingUser => BroadcasterUserId;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
