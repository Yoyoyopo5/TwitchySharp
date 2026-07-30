using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// Moderator privileges were added to a user on a specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token with <see cref="Scope.ModerationRead"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster for the channel you want to get moderator removal notifications for.
/// This user must have created a user access token that includes <see cref="Scope.ModerationRead"/> for your application.
/// </param>
public sealed record ChannelModeratorAdd(UserId BroadcasterUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelModeratorAdd>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelModeratorAdd;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelModeratorAdd;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new ConditionKey("broadcaster_user_id");
    public override IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ModerationRead);
    public override TwitchIdentity Identity { get; } = new TwitchIdentity.User(BroadcasterUserId);

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public static Validation<ChannelModeratorAdd> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new ChannelModeratorAdd(BroadcasterUserId));
}
