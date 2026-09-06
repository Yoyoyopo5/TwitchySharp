using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// Users respond to a poll on a specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadPolls"/> or <see cref="Scope.ChannelManagePolls"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster for which poll progress notifications will be received.
/// This user must have created a user access token including <see cref="Scope.ChannelReadPolls"/> or <see cref="Scope.ChannelManagePolls"/> for this application.
/// </param>
public sealed record ChannelPollProgress(UserId BroadcasterUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelPollProgress>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelPollProgress;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelPollProgress;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new ConditionKey("broadcaster_user_id");
    public override EventSubSubscriptionAuthenticationContext.UserAuthorized AuthenticationContext
        => new()
        {
            Identity = new TwitchIdentity.User(BroadcasterUserId),
            ValidScopes = ImmutableHashSet.Create(Scope.ChannelReadPolls, Scope.ChannelManagePolls)
        };

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public static Validation<ChannelPollProgress> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new ChannelPollProgress(BroadcasterUserId));
}
