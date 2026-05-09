using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub.Channel;
/// <summary>
/// A moderator has removed a specific message.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserReadChat"/>, or
/// an app access token where the <paramref name="UserId"/> has created a user access token with <see cref="Scope.UserBot"/> and <see cref="Scope.ChannelBot"/> (from broadcaster or moderator status).
/// The user who created the access token must be the same user as the <paramref name="UserId"/>.
/// </remarks>
/// <param name="BroadcasterUserId">User id of the broadcaster (channel) to receive chat message delete events for.</param>
/// <param name="UserId">The user id of the user to read chat as.</param>
public sealed record ChannelChatMessageDelete(UserId BroadcasterUserId, UserId UserId)
    : IUserAuthorizedSubscriptionTypeSpecification
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelChatMessageDelete;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new("user_id");
    public IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.UserReadChat);
    public UserId AuthorizingUser => UserId;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId)
            .Set(new ConditionKey("user_id"), UserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
