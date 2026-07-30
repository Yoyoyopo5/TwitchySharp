using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A user acknowledges a warning. Broadcasters and moderators can see the warning's details.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadWarnings"/> or <see cref="Scope.ModeratorManageWarnings"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) to receive warning acknowledgement notifications for.</param>
/// <param name="ModeratorUserId">
/// The user id of the broadcaster or a moderator in a broadcaster's chat.
/// This user must have created a user access token that includes <see cref="Scope.ModeratorReadWarnings"/> or <see cref="Scope.ModeratorManageWarnings"/> for this application.
/// </param>
public sealed record ChannelWarningAcknowledgement(UserId BroadcasterUserId, UserId ModeratorUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelWarningAcknowledgement>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelWarningAcknowledgement;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelWarningAcknowledgement;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new ConditionKey("moderator_user_id");
    public override IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ModeratorReadWarnings, Scope.ModeratorManageWarnings);
    public override TwitchIdentity Identity { get; } = new TwitchIdentity.User(ModeratorUserId);

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId)
            .Set(new ConditionKey("moderator_user_id"), ModeratorUserId);
    public static Validation<ChannelWarningAcknowledgement> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .GetRequiredValue(new("moderator_user_id"), out UserId ModeratorUserId, value => new(value))
            .Map(_ => new ChannelWarningAcknowledgement(BroadcasterUserId, ModeratorUserId));
}
