using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A user cheers on the specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.BitsRead"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The broadcaster user ID for the channel you want to get cheer notifications for.
/// This user must have created a user access token including <see cref="Scope.BitsRead"/> for this application.
/// </param>
public sealed record ChannelCheer(UserId BroadcasterUserId)
    : IUserAuthorizedSubscriptionTypeSpecification
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelCheer;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new("broadcaster_user_id");
    public IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.BitsRead);
    public UserId AuthorizingUser => BroadcasterUserId;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new("broadcaster_user_id"), BroadcasterUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
