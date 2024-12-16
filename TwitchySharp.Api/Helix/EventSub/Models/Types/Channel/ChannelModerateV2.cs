using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.Channel;
/// <summary>
/// A moderator performs a moderation action in a channel. Includes warnings.
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
/// One of <see cref="Scope.ModeratorReadWarnings"/> or <see cref="Scope.ModeratorManageWarnings"/>,
/// <br/>
/// Plus <see cref="Scope.ModeratorReadModerators"/> and <see cref="Scope.ModeratorReadVips"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) to get moderation notifications for.</param>
/// <param name="ModeratorUserId">
/// The user id of the broadcaster or a moderator in the broadcaster's chat.
/// This user must have created a user access token for this application with the required scopes.
/// </param>
public sealed record ChannelModerateV2(string BroadcasterUserId, string ModeratorUserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.CHANNEL_MODERATE;
    public string Version => EventSubSubscriptionTypeVersions.V2;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId)
            .Set("moderator_user_id", ModeratorUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
