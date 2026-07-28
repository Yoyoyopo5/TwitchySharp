using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// The specified broadcaster stops a stream.
/// </summary>
/// <remarks>
/// No authorization required.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) you want to get stream offline notifications for.</param>
public sealed record StreamOffline(UserId BroadcasterUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<StreamOffline>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.StreamOffline;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.StreamOffline;

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new("broadcaster_user_id"), BroadcasterUserId);
    public static Validation<StreamOffline> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new StreamOffline(BroadcasterUserId));
}
