using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A broadcaster updates their channel properties e.g., category, title, content classification labels, broadcast, or language.
/// </summary>
/// <remarks>
/// No authorization required.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) you want to get updates for.</param>
public sealed record ChannelUpdate(UserId BroadcasterUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelUpdate>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelUpdate;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelUpdate;
    public override EventSubSubscriptionAuthenticationContext.ClientAuthorized AuthenticationContext { get; } = new();

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public static Validation<ChannelUpdate> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new ChannelUpdate(BroadcasterUserId));
}
