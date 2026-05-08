using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub.Channel;
/// <summary>
/// A moderator or bot has cleared all messages from a specific user.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserReadChat"/>, or
/// an app access token where the <paramref name="UserId"/> has created a user access token with <see cref="Scope.UserBot"/> and <see cref="Scope.ChannelBot"/> (from broadcaster or moderator status).
/// The user who created the access token must be the same user as the <paramref name="UserId"/>.
/// </remarks>
/// <param name="BroadcasterUserId">User ID of the channel to receive chat clear user messages events for.</param>
/// <param name="UserId">The user ID to read chat as.</param>
public sealed record ChannelChatClearUserMessages(UserId BroadcasterUserId, UserId UserId)
    : IUserAuthorizedSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelChatClearUserMessages;
    public ConditionKey AuthorizingUserConditionKey => new("user_id");
    public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.UserReadChat);

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new("broadcaster_user_id"), BroadcasterUserId)
            .Set(new("user_id"), UserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
