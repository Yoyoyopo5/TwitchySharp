using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A notification when the active shared chat session the channel is in changes.
/// </summary>
/// <remarks>
/// No authorization required.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) to receive shared chat session begin events for.</param>
public sealed record ChannelSharedChatSessionUpdate(UserId BroadcasterUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelSharedChatSessionUpdate>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelSharedChatSessionUpdate;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelSharedChatSessionUpdate;

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public static Validation<ChannelSharedChatSessionUpdate> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new ChannelSharedChatSessionUpdate(BroadcasterUserId));
}
