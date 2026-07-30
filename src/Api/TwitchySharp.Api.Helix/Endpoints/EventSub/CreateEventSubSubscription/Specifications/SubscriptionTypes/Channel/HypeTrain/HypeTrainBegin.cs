using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;

/// <summary>
/// A Hype Train begins on the specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadHypeTrain"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster (channel) that you want to get Hype Train begin notifications for.
/// This user must have also created a user access token including <see cref="Scope.ChannelReadHypeTrain"/> for your application.
/// </param>
public sealed record HypeTrainBegin(UserId BroadcasterUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<HypeTrainBegin>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.HypeTrainBegin;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.HypeTrainBegin;
    public override IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ChannelReadHypeTrain);
    public override TwitchIdentity Identity { get; } = new TwitchIdentity.User(BroadcasterUserId);

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new("broadcaster_user_id"), BroadcasterUserId);
    public static Validation<HypeTrainBegin> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new HypeTrainBegin(BroadcasterUserId));
}
