namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// Sends an event notification when progress is made towards the campaign's goal or when the broadcaster changes the fundraising goal.
/// </summary>
/// <param name="BroadcasterUserId">The ID of the broadcaster that you want to receive notifications about when their campaign makes progress or is updated.</param>
public sealed record CharityCampaignProgress(UserId BroadcasterUserId)
    : IEventSubSubscriptionTypeSpecification
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.CharityCampaignProgress;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
