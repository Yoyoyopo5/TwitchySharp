using System.Collections.Generic;
using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// Any user sends a message to a specific chat room.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserReadChat"/>, or
/// an app access token where the <paramref name="UserId"/> has created a user access token with <see cref="Scope.UserBot"/> and <see cref="Scope.ChannelBot"/> (from broadcaster or moderator status).
/// The user who created the access token must be the same user as the <paramref name="UserId"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The User ID of the channel to receive chat message events for.</param>
/// <param name="UserId">The User ID to read chat as.</param>
public sealed record ChannelChatMessage(UserId BroadcasterUserId, UserId UserId)
    : IUserAuthorizedSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelChatMessage;
    public ConditionKey AuthorizingUserConditionKey => new ConditionKey("user_id");
    public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.UserReadChat);

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId)
            .Set(new ConditionKey("user_id"), UserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
