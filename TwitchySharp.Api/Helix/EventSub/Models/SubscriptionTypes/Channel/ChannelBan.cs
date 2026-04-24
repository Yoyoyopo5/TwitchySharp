using System.Collections.Generic;
using System.Collections.Immutable;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;
using TwitchySharp.Shared.EventSub;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A viewer is banned from the specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelModerate"/>
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster (channel) you want to get ban notifications for.
/// This must have created a user access token including <see cref="Scope.ChannelModerate"/> for this application.
/// </param>
public sealed record ChannelBan(UserId BroadcasterUserId)
    : IUserAuthorizedSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelBan;
    public ConditionKey AuthorizingUserConditionKey => new ConditionKey("broadcaster_user_id");
    public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ChannelModerate);

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
