using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models.Helix.EventSub.Interfaces;
using TwitchySharp.Api.Models.Helix.EventSub.Models;
using TwitchySharp.Shared.EventSub.Constants;

namespace TwitchySharp.Api.Models.Helix.EventSub.Models.Types.Channel.GuestStar;
/// <summary>
/// A guest or a slot is updated in an active Guest Star session.
/// </summary>
/// <remarks>
/// Requires a user access token that includes (one of) <see cref="Scope.ChannelReadGuestStar"/>, <see cref="Scope.ChannelManageGuestStar"/>, <see cref="Scope.ModeratorReadGuestStar"/>, or <see cref="Scope.ModeratorManageGuestStar"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) hosting the Guest Star Session.</param>
/// <param name="ModeratorUserId">The user id of the broadcaster or a moderator of the specified broadcaster.</param>
public sealed record ChannelGuestStarGuestUpdate(string BroadcasterUserId, string ModeratorUserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.CHANNEL_GUEST_STAR_GUEST_UPDATE;
    public string Version => EventSubSubscriptionTypeVersions.BETA;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId)
            .Set("moderator_user_id", ModeratorUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
