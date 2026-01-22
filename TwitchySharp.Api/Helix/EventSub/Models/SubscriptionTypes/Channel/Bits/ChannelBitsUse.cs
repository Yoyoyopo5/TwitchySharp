using System.Collections.Generic;
using TwitchySharp.Shared.EventSub.Constants;
using TwitchySharp.Shared.Models;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
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
    : IEventSubSubscriptionType
{
    public EventSubSubscriptionTypeName Name { get; } = new(EventSubSubscriptionTypeNames.CHANNEL_BITS_USE);
    public EventSubSubscriptionTypeVersion Version { get; } = new(EventSubSubscriptionTypeVersions.V1);

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
