using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// Moderator privileges were removed from a user on a specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token with <see cref="Scope.ModerationRead"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster for the channel you want to get moderator removal notifications for.
/// This user must have created a user access token that includes <see cref="Scope.ModerationRead"/> for your application.
/// </param>
public sealed record ChannelModeratorRemove(UserId BroadcasterUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelModeratorRemove>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelModeratorRemove;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelModeratorRemove;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new ConditionKey("broadcaster_user_id");
    public override EventSubSubscriptionAuthenticationContext.UserAuthorized AuthenticationContext
        => new()
        {
            Identity = new TwitchIdentity.User(BroadcasterUserId),
            ValidScopes = ImmutableHashSet.Create(Scope.ModerationRead)
        };

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public static Validation<ChannelModeratorRemove> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new ChannelModeratorRemove(BroadcasterUserId));
}
