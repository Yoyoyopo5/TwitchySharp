using System.Collections.Generic;
using System.Collections.Immutable;
using TwitchySharp.Shared.EventSub;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
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
    : IUserAuthorizedSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelModeratorAdd;
    public ConditionKey AuthorizingUserConditionKey => new ConditionKey("broadcaster_user_id");
    public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ModerationRead);

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
