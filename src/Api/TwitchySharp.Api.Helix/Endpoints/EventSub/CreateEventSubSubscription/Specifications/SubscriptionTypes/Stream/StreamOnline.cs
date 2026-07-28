using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// The specified broadcaster starts a stream.
/// </summary>
/// <remarks>
/// No authorization required.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) you want to get stream online notifications for.</param>
public sealed record StreamOnline(UserId BroadcasterUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<StreamOnline>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.StreamOnline;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.StreamOnline;

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new("broadcaster_user_id"), BroadcasterUserId);
    public static Validation<StreamOnline> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new StreamOnline(BroadcasterUserId));
}
