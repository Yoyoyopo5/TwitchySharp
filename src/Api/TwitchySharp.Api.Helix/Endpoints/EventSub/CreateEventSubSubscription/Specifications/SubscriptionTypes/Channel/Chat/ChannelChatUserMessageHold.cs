using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A user is notified if their message is caught by automod.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserReadChat"/>, or
/// an app access token where the <paramref name="UserId"/> has created a user access token with <see cref="Scope.UserBot"/>.
/// The user who created the access token must be the same user as the <paramref name="UserId"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) to receive chat message events for.</param>
/// <param name="UserId">The user id of the user to read chat as.</param>
public sealed record ChannelChatUserMessageHold(UserId BroadcasterUserId, UserId UserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelChatUserMessageHold>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelChatUserMessageHold;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelChatUserMessageHold;
    public override IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.UserReadChat);
    public override TwitchIdentity Identity { get; } = new TwitchIdentity.User(UserId);

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new("broadcaster_user_id"), BroadcasterUserId)
            .Set(new("user_id"), UserId);

    public static Validation<ChannelChatUserMessageHold> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .GetRequiredValue(new("user_id"), out UserId UserId, value => new(value))
            .Map(_ => new ChannelChatUserMessageHold(BroadcasterUserId, UserId));
}
