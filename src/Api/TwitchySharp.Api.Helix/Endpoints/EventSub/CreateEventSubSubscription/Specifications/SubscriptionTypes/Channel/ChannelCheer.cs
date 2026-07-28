using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A user cheers on the specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.BitsRead"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The broadcaster user ID for the channel you want to get cheer notifications for.
/// This user must have created a user access token including <see cref="Scope.BitsRead"/> for this application.
/// </param>
public sealed record ChannelCheer(UserId BroadcasterUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelCheer>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelCheer;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelCheer;
    public override IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.BitsRead);
    public override TwitchIdentity Identity { get; } = new TwitchIdentity.User(BroadcasterUserId);

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new("broadcaster_user_id"), BroadcasterUserId);
    public static Validation<ChannelCheer> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new ChannelCheer(BroadcasterUserId));
}
