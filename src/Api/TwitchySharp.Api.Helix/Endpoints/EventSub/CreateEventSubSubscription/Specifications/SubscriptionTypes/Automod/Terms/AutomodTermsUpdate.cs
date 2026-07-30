using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A notification is sent when a broadcaster's automod terms are updated. Changes to private terms are not sent.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageAutomod"/>.
/// The user who created the access token must be the same user as the <paramref name="ModeratorUserId"/>.
/// </remarks>
/// <param name="BroadcasterUserId">User id of the broadcaster (channel).</param>
/// <param name="ModeratorUserId">User id of a moderator in the broadcaster's chat. This can also be the broadcaster.</param>
public sealed record AutomodTermsUpdate(UserId BroadcasterUserId, UserId ModeratorUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<AutomodTermsUpdate>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.AutomodTermsUpdate;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.AutomodTermsUpdate;
    public override IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ModeratorManageAutomod);
    public override TwitchIdentity Identity { get; } = new TwitchIdentity.User(ModeratorUserId);

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId)
            .Set(new ConditionKey("moderator_user_id"), ModeratorUserId);
    public static Validation<AutomodTermsUpdate> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .GetRequiredValue(new("moderator_user_id"), out UserId ModeratorUserId, value => new(value))
            .Map(_ => new AutomodTermsUpdate(BroadcasterUserId, ModeratorUserId));
}
