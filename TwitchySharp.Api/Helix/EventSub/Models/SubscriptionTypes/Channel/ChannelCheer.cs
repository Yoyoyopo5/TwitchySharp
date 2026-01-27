using System.Collections.Generic;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;
using TwitchySharp.Shared.EventSub;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A user cheers on the specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.BitsRead"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The broadcaster user ID for the channel you want to get cheer notifications for.
/// This user must have created a user access token including <see cref="Scope.BitsRead"/> for this application.
/// </param>
public sealed record ChannelCheer(UserId BroadcasterUserId)
    : IEventSubSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelCheer;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
