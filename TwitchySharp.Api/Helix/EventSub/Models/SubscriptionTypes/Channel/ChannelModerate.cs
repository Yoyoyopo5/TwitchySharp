using System.Collections.Generic;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;
using TwitchySharp.Shared.EventSub;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A moderator performs a moderation action in a channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes all of the following:
/// <br/>
/// One of <see cref="Scope.ModeratorReadBlockedTerms"/> or <see cref="Scope.ModeratorManageBlockedTerms"/>,
/// <br/>
/// One of <see cref="Scope.ModeratorReadChatSettings"/> or <see cref="Scope.ModeratorManageChatSettings"/>,
/// <br/>
/// One of <see cref="Scope.ModeratorReadUnbanRequests"/> or <see cref="Scope.ModeratorManageUnbanRequests"/>,
/// <br/>
/// One of <see cref="Scope.ModeratorReadBannedUsers"/> or <see cref="Scope.ModeratorManageBannedUsers"/>,
/// <br/>
/// One of <see cref="Scope.ModeratorReadChatMessages"/> or <see cref="Scope.ModeratorManageChatMessages"/>,
/// <br/>
/// Plus <see cref="Scope.ModeratorReadModerators"/> and <see cref="Scope.ModeratorReadVips"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) to get moderation notifications for.</param>
/// <param name="ModeratorUserId">
/// The user id of the broadcaster or a moderator in the broadcaster's chat.
/// This user must have created a user access token for this application with the required scopes.
/// </param>
public sealed record ChannelModerate(UserId BroadcasterUserId, UserId ModeratorUserId)
    : IUserAuthorizedSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelModerate;
    public ConditionKey AuthorizingUserConditionKey => new ConditionKey("moderator_user_id");
    public IEnumerable<Scope> ValidScopes => [ Scope.ModeratorReadBlockedTerms, Scope.ModeratorReadChatSettings, Scope.ModeratorReadUnbanRequests, Scope.ModeratorReadBannedUsers, Scope.ModeratorReadChatMessages, Scope.ModeratorReadModerators, Scope.ModeratorReadVips ];

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId)
            .Set(new ConditionKey("moderator_user_id"), ModeratorUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
