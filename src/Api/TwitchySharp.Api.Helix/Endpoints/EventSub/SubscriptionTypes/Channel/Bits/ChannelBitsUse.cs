using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub.Channel;
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
    : IUserAuthorizedSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelBitsUse;
    public ConditionKey AuthorizingUserConditionKey => new("broadcaster_user_id");
    public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.BitsRead);

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new("broadcaster_user_id"), BroadcasterUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
