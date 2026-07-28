using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A midroll commercial break has started running.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadAds"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster that you want to get Channel Ad Break begin notifications for.</param>
public sealed record ChannelAdBreakBegin(UserId BroadcasterUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelAdBreakBegin>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelAdBreakBegin;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelAdBreakBegin;
    public override IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ChannelReadAds);
    public override TwitchIdentity Identity { get; } = new TwitchIdentity.User(BroadcasterUserId);

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public static Validation<ChannelAdBreakBegin> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new ChannelAdBreakBegin(BroadcasterUserId));
}
