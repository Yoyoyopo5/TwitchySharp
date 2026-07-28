using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A poll ended on a specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadPolls"/> or <see cref="Scope.ChannelManagePolls"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster for which poll end notifications will be received.
/// This user must have created a user access token including <see cref="Scope.ChannelReadPolls"/> or <see cref="Scope.ChannelManagePolls"/> for this application.
/// </param>
public sealed record ChannelPollEnd(UserId BroadcasterUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelPollEnd>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelPollEnd;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelPollEnd;
    public override IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ChannelReadPolls, Scope.ChannelManagePolls);
    public override TwitchIdentity Identity { get; } = new TwitchIdentity.User(BroadcasterUserId);

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new("broadcaster_user_id"), BroadcasterUserId);
    public static Validation<ChannelPollEnd> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new ChannelPollEnd(BroadcasterUserId));
}
