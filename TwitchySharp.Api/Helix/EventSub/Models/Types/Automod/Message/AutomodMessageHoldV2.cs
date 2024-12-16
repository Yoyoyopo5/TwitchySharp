using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.Automod.Message;
/// <summary>
/// A user is notified if a message is caught by automod for review. 
/// Only public blocked terms trigger notifications, not private ones.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageAutomod"/>.
/// The user who created the access token must be the same user as the <paramref name="ModeratorUserId"/>.
/// </remarks>
/// <param name="BroadcasterUserId">User id of the broadcaster (channel).</param>
/// <param name="ModeratorUserId">User id of a moderator in the broadcaster's chat. This can also be the broadcaster.</param>
public sealed record AutomodMessageHoldV2(string BroadcasterUserId, string ModeratorUserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.AUTOMOD_MESSAGE_HOLD;
    public string Version => EventSubSubscriptionTypeVersions.V2;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId)
            .Set("moderator_user_id", ModeratorUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
