using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// Sends an event notification when the broadcaster stops a charity campaign.
/// </summary>
/// <param name="BroadcasterUserId">The ID of the broadcaster that you want to receive notifications about when they stop a charity campaign.</param>
public sealed record CharityCampaignStop(UserId BroadcasterUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<CharityCampaignStop>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.CharityCampaignStop;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.CharityCampaignStop;

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public static Validation<CharityCampaignStop> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new CharityCampaignStop(BroadcasterUserId));
}
