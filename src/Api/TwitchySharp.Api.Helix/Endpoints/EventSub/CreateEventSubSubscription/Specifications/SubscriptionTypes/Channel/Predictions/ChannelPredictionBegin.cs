using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A Prediction started on a specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token with <see cref="Scope.ChannelReadPredictions"/> or <see cref="Scope.ChannelManagePredictions"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster for which prediction begin events will be received.
/// This user must have created a user access token including <see cref="Scope.ChannelReadPredictions"/> or <see cref="Scope.ChannelManagePredictions"/> for this application.
/// </param>
public sealed record ChannelPredictionBegin(UserId BroadcasterUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelPredictionBegin>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelPredictionBegin;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelPredictionBegin;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new ConditionKey("broadcaster_user_id");
    public override EventSubSubscriptionAuthenticationContext.UserAuthorized AuthenticationContext
        => new()
        {
            Identity = new TwitchIdentity.User(BroadcasterUserId),
            ValidScopes = ImmutableHashSet.Create(Scope.ChannelReadPredictions, Scope.ChannelManagePredictions)
        };

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public static Validation<ChannelPredictionBegin> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new ChannelPredictionBegin(BroadcasterUserId));
}
