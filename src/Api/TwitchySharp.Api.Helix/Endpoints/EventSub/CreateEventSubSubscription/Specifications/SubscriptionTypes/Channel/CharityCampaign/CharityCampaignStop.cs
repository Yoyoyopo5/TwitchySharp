namespace TwitchySharp.Api.Helix.EventSub.Channel;
/// <summary>
/// Sends an event notification when the broadcaster stops a charity campaign.
/// </summary>
/// <param name="BroadcasterUserId">The ID of the broadcaster that you want to receive notifications about when they stop a charity campaign.</param>
public sealed record CharityCampaignStop(UserId BroadcasterUserId)
    : IEventSubSubscriptionTypeSpecification
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.CharityCampaignStop;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
