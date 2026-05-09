using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub.Channel;
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
    : IUserAuthorizedSubscriptionTypeSpecification
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelVIPAdd;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new ConditionKey("broadcaster_user_id");
    public IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ChannelReadVips, Scope.ChannelManageVips);
    public UserId AuthorizingUser => BroadcasterUserId;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
