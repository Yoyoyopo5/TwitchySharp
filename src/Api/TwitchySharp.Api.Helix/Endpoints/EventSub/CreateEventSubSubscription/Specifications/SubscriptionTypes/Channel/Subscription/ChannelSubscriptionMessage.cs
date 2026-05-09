using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub.Channel;
/// <summary>
/// A notification when a user sends a resubscription chat message in a specific channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadSubscriptions"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster (channel) you want to get resubscription chat notifications for.
/// This user must have created a user access token that includes <see cref="Scope.ChannelReadSubscriptions"/> for this application.
/// </param>
public sealed record ChannelSubscriptionMessage(UserId BroadcasterUserId)
    : IUserAuthorizedSubscriptionTypeSpecification
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelSubscriptionMessage;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new ConditionKey("broadcaster_user_id");
    public IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ChannelReadSubscriptions);
    public UserId AuthorizingUser => BroadcasterUserId;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
