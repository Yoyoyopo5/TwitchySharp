using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// Sends an event notification when a user donates to the broadcaster's charity campaign.
/// </summary>
/// <param name="BroadcasterUserId">The ID of the broadcaster that you want to receive notifications about when users donate to their campaign.</param>
public sealed record CharityDonation(UserId BroadcasterUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<CharityDonation>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.CharityDonation;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.CharityDonation;

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public static Validation<CharityDonation> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new CharityDonation(BroadcasterUserId));
}
