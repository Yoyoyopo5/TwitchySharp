using System.Collections.Generic;
using System.Collections.Immutable;
using TwitchySharp.Shared.EventSub;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A moderator or bot has cleared all messages from the chat room.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserReadChat"/>, or
/// an app access token where the <paramref name="UserId"/> has created a user access token with <see cref="Scope.UserBot"/> and <see cref="Scope.ChannelBot"/> (from broadcaster or moderator status).
/// The user who created the access token must be the same user as the <paramref name="UserId"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) to receive chat clear events for.</param>
/// <param name="UserId">The user id of the user to read chat as.</param>
public sealed record ChannelChatClear(UserId BroadcasterUserId, UserId UserId)
    : IUserAuthorizedSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelChatClear;
    public ConditionKey AuthorizingUserConditionKey => new ConditionKey("user_id");
    public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.UserReadChat);

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId)
            .Set(new ConditionKey("user_id"), UserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
