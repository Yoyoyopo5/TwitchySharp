using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// Users respond to a poll on a specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadPolls"/> or <see cref="Scope.ChannelManagePolls"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster for which poll progress notifications will be received.
/// This user must have created a user access token including <see cref="Scope.ChannelReadPolls"/> or <see cref="Scope.ChannelManagePolls"/> for this application.
/// </param>
public sealed record ChannelPollProgress(UserId BroadcasterUserId)
    : IUserAuthorizedSubscriptionTypeSpecification
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelPollProgress;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new ConditionKey("broadcaster_user_id");
    public IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ChannelReadPolls, Scope.ChannelManagePolls);
    public UserId AuthorizingUser => BroadcasterUserId;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
