using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
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
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelChatMessage>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelChatMessage;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelChatMessage;
    public override IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.UserReadChat);
    public override TwitchIdentity Identity { get; } = new TwitchIdentity.User(UserId);

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId)
            .Set(new ConditionKey("user_id"), UserId);
    public static Validation<ChannelChatMessage> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .GetRequiredValue(new("user_id"), out UserId UserId, value => new(value))
            .Map(_ => new ChannelChatMessage(BroadcasterUserId, UserId));
}
