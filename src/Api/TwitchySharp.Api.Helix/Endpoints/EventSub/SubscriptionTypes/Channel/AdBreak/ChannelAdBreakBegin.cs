using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub.Channel;
/// <summary>
/// A midroll commercial break has started running.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadAds"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster that you want to get Channel Ad Break begin notifications for.</param>
public sealed record ChannelAdBreakBegin(UserId BroadcasterUserId)
    : IUserAuthorizedSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelAdBreakBegin;
    public ConditionKey AuthorizingUserConditionKey => new("broadcaster_user_id");
    public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ChannelReadAds);

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new("broadcaster_user_id"), BroadcasterUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
