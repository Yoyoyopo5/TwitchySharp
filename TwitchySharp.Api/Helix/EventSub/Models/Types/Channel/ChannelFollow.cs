using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.Channel;
/// <summary>
/// A specified channel receives a follow.
/// </summary>
/// <remarks>
/// Requires a user access token with <see cref="Scope.ModeratorReadFollowers"/>.
/// The user who created the access token must be the same user as the <paramref name="ModeratorUserId"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster whose channel you want to get follow notifications for.</param>
/// <param name="ModeratorUserId">The ID of a moderator of the channel you want to get follow notifications for. If you have authorization from the broadcaster rather than a moderator, specify the broadcaster’s user ID here.</param>
public sealed record ChannelFollow(string BroadcasterUserId, string ModeratorUserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.CHANNEL_FOLLOW;
    public string Version => EventSubSubscriptionTypeVersions.V2;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId)
            .Set("moderator_user_id", ModeratorUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
