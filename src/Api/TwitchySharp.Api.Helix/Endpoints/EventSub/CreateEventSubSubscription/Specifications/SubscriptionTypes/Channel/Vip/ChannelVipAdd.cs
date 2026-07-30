using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A VIP is added to the channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadVips"/> or <see cref="Scope.ChannelManageVips"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster (channel) that you want to get VIP add notifications for.
/// This user must have created a user access token that includes <see cref="Scope.ChannelReadVips"/> or <see cref="Scope.ChannelManageVips"/> for this application.
/// </param>
public sealed record ChannelVipAdd(UserId BroadcasterUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelVipAdd>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelVIPAdd;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelVIPAdd;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new ConditionKey("broadcaster_user_id");
    public override IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ChannelReadVips, Scope.ChannelManageVips);
    public override TwitchIdentity Identity { get; } = new TwitchIdentity.User(BroadcasterUserId);

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public static Validation<ChannelVipAdd> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new ChannelVipAdd(BroadcasterUserId));
}
