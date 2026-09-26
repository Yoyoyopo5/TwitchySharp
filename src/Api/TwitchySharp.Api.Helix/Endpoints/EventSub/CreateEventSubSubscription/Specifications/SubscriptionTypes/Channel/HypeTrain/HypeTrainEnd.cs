using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;

/// <summary>
/// A Hype Train ends on the specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadHypeTrain"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster (channel) that you want to get Hype Train end notifications for.
/// This user must have also created a user access token including <see cref="Scope.ChannelReadHypeTrain"/> for your application.
/// </param>
public sealed record HypeTrainEnd(UserId BroadcasterUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<HypeTrainEnd>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.HypeTrainEnd;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.HypeTrainEnd;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new ConditionKey("broadcaster_user_id");
    public override EventSubSubscriptionAuthenticationContext.UserAuthorized AuthenticationContext
        => new()
        {
            Identity = new TwitchIdentity.User(BroadcasterUserId),
            ValidScopes = ImmutableHashSet.Create(Scope.ChannelReadHypeTrain)
        };

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public static Validation<HypeTrainEnd> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new HypeTrainEnd(BroadcasterUserId));
}
