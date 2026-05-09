namespace TwitchySharp.Api.Helix.EventSub.Stream;
/// <summary>
/// The specified broadcaster stops a stream.
/// </summary>
/// <remarks>
/// No authorization required.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) you want to get stream offline notifications for.</param>
public sealed record StreamOffline(UserId BroadcasterUserId)
    : IEventSubSubscriptionTypeSpecification
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.StreamOffline;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new("broadcaster_user_id"), BroadcasterUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
