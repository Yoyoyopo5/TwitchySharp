using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub.Channel;
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
    : IUserAuthorizedSubscriptionTypeSpecification
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelPredictionProgress;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new ConditionKey("broadcaster_user_id");
    public IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ChannelReadPredictions, Scope.ChannelManagePredictions);
    public UserId AuthorizingUser => BroadcasterUserId;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
