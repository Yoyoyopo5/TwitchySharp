using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// Sends an event notification when progress is made towards the campaign's goal or when the broadcaster changes the fundraising goal.
/// </summary>
/// <param name="BroadcasterUserId">The ID of the broadcaster that you want to receive notifications about when their campaign makes progress or is updated.</param>
public sealed record CharityCampaignProgress(UserId BroadcasterUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<CharityCampaignProgress>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.CharityCampaignProgress;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.CharityCampaignProgress;

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public static Validation<CharityCampaignProgress> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new CharityCampaignProgress(BroadcasterUserId));
}
