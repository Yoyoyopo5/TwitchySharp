using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A notification for when an event that appears in chat has occurred, such as someone subscribing to the channel or a subscription is gifted.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserReadChat"/>, or
/// an app access token where the <paramref name="UserId"/> has created a user access token with <see cref="Scope.UserBot"/> and <see cref="Scope.ChannelBot"/> (from broadcaster or moderator status).
/// The user who created the access token must be the same user as the <paramref name="UserId"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) to receive chat notification events for.</param>
/// <param name="UserId">The user id of the user to read chat as.</param>
public sealed record ChannelChatNotification(UserId BroadcasterUserId, UserId UserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelChatNotification>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelChatNotification;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelChatNotification;
    public override EventSubSubscriptionAuthenticationContext.UserAuthorized AuthenticationContext
        => new()
        {
            Identity = new TwitchIdentity.User(UserId),
            ValidScopes = ImmutableHashSet.Create(Scope.UserReadChat)
        };

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId)
            .Set(new ConditionKey("user_id"), UserId);
    public static Validation<ChannelChatNotification> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .GetRequiredValue(new("user_id"), out UserId UserId, value => new(value))
            .Map(_ => new ChannelChatNotification(BroadcasterUserId, UserId));
}
