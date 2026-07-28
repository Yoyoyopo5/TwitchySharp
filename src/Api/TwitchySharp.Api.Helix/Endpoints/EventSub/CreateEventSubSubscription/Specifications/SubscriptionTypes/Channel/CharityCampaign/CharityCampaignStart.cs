using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// Sends an event notification when the broadcaster starts a charity campaign.
/// </summary>
/// <param name="BroadcasterUserId">The ID of the broadcaster that you want to receive notifications about when they start a charity campaign.</param>
public sealed record CharityCampaignStart(UserId BroadcasterUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<CharityCampaignStart>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.CharityCampaignStart;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.CharityCampaignStart;

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public static Validation<CharityCampaignStart> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new CharityCampaignStart(BroadcasterUserId));
}
