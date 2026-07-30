using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// Sends a notification when the specified broadcaster receives a Shoutout.
/// <b>Note: </b> Sent only if Twitch posts the Shoutout to the broadcaster's activity feed.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadShoutouts"/> or <see cref="Scope.ModeratorManageShoutouts"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) that you want to receive notifications about when they receive a Shoutout.</param>
/// <param name="ModeratorUserId">
/// The user id of the broadcaster or one of the broadcaster's moderators.
/// This user must have created a user access token for this application that includes <see cref="Scope.ModeratorReadShoutouts"/> or <see cref="Scope.ModeratorManageShoutouts"/>.
/// </param>
public sealed record ShoutoutReceived(UserId BroadcasterUserId, UserId ModeratorUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ShoutoutReceived>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ShoutoutReceived;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ShoutoutReceived;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new ConditionKey("moderator_user_id");
    public override IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ModeratorReadShoutouts, Scope.ModeratorManageShoutouts);
    public override TwitchIdentity Identity { get; } = new TwitchIdentity.User(ModeratorUserId);

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId)
            .Set(new ConditionKey("moderator_user_id"), ModeratorUserId);
    public static Validation<ShoutoutReceived> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .GetRequiredValue(new("moderator_user_id"), out UserId ModeratorUserId, value => new(value))
            .Map(_ => new ShoutoutReceived(BroadcasterUserId, ModeratorUserId));
}
