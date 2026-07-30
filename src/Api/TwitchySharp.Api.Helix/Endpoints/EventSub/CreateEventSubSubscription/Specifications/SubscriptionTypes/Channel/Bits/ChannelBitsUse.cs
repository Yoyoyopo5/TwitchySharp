using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// This event is designed to be an all-purpose event for when Bits are used in a channel and might be updated in the future as more Twitch features use Bits.
/// </summary>
/// <remarks>
/// Currently, this event will be sent when a user:
/// <list type="bullet">
/// <item>Cheers in a channel</item>
/// <item>Uses a Power-up (Will not emit when a streamer uses a Power-up for free in their own channel.)</item>
/// <item>Sends Combos</item>
/// </list>
/// Bits transactions via Twitch Extensions are not included in this subscription type.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.BitsRead"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) to get Bits Use notifications for.</param>
public sealed record ChannelBitsUse(UserId BroadcasterUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelBitsUse>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelBitsUse;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelBitsUse;
    public override IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.BitsRead);
    public override TwitchIdentity Identity { get; } = new TwitchIdentity.User(BroadcasterUserId);

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public static Validation<ChannelBitsUse> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new ChannelBitsUse(BroadcasterUserId));
}
