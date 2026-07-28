using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// Users participated in a Prediction on a specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token with <see cref="Scope.ChannelReadPredictions"/> or <see cref="Scope.ChannelManagePredictions"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster for which prediction progress events will be received.
/// This user must have created a user access token including <see cref="Scope.ChannelReadPredictions"/> or <see cref="Scope.ChannelManagePredictions"/> for this application.
/// </param>
public sealed record ChannelPredictionProgress(UserId BroadcasterUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelPredictionProgress>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelPredictionProgress;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelPredictionProgress;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new ConditionKey("broadcaster_user_id");
    public override IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ChannelReadPredictions, Scope.ChannelManagePredictions);
    public override TwitchIdentity Identity { get; } = new TwitchIdentity.User(BroadcasterUserId);

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public static Validation<ChannelPredictionProgress> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new ChannelPredictionProgress(BroadcasterUserId));
}
