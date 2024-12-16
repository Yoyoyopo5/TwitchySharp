using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.UnbanRequest;
/// <summary>
/// A user creates an unban request.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadUnbanRequests"/> or <see cref="Scope.ModeratorManageUnbanRequests"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) you want to get chat unban request notifications for.</param>
/// <param name="ModeratorUserId">
/// The user id of the broadcaster or a moderator in the broadcaster's chat.
/// This user must have created a user access token including <see cref="Scope.ModeratorReadUnbanRequests"/> or <see cref="Scope.ModeratorManageUnbanRequests"/> for this application.
/// </param>
public sealed record ChannelUnbanRequestCreate(string BroadcasterUserId, string ModeratorUserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.CHANNEL_UNBAN_REQUEST_CREATE;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("moderator_user_id", ModeratorUserId)
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
