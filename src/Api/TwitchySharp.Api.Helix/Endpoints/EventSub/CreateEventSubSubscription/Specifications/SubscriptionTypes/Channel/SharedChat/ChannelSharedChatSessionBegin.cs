using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A notification when a channel becomes active in an active shared chat session.
/// </summary>
/// <remarks>
/// No authorization required.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) to receive shared chat session begin events for.</param>
public sealed record ChannelSharedChatSessionBegin(UserId BroadcasterUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelSharedChatSessionBegin>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelSharedChatSessionBegin;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelSharedChatSessionBegin;

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);

    public override EventSubSubscriptionAuthenticationContext.None AuthenticationContext
        => EventSubSubscriptionAuthenticationContext.None.Instance;

    public static Validation<ChannelSharedChatSessionBegin> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new ChannelSharedChatSessionBegin(BroadcasterUserId));
}
