using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A notification for when a broadcaster's chat settings are updated.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserReadChat"/>, or
/// an app access token where the <paramref name="UserId"/> has created a user access token with <see cref="Scope.UserBot"/> and <see cref="Scope.ChannelBot"/> (from broadcaster or moderator status).
/// The user who created the access token must be the same user as the <paramref name="UserId"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) to receive chat settings update events for.</param>
/// <param name="UserId">The user id of the user to read chat as.</param>
public sealed record ChannelChatSettingsUpdate(UserId BroadcasterUserId, UserId UserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelChatSettingsUpdate>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelChatSettingsUpdate;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelChatSettingsUpdate;
    public override EventSubSubscriptionAuthenticationContext.UserAuthorized AuthenticationContext
        => new()
        {
            Identity = new TwitchIdentity.User(UserId),
            ValidScopes = ImmutableHashSet.Create(Scope.UserReadChat)
        };

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new("broadcaster_user_id"), BroadcasterUserId)
            .Set(new("user_id"), UserId);
    public static Validation<ChannelChatSettingsUpdate> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .GetRequiredValue(new("user_id"), out UserId UserId, value => new(value))
            .Map(_ => new ChannelChatSettingsUpdate(BroadcasterUserId, UserId));
}
