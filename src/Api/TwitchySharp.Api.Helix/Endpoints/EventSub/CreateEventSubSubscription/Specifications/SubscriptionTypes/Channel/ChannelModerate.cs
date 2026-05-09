using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub.Channel;
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
    : IUserAuthorizedSubscriptionTypeSpecification
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelModerate;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new ConditionKey("moderator_user_id");
    public IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ModeratorReadBlockedTerms, Scope.ModeratorReadChatSettings, Scope.ModeratorReadUnbanRequests, Scope.ModeratorReadBannedUsers, Scope.ModeratorReadChatMessages, Scope.ModeratorReadModerators, Scope.ModeratorReadVips);
    public UserId AuthorizingUser => ModeratorUserId;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId)
            .Set(new ConditionKey("moderator_user_id"), ModeratorUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
