using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.Warnings;
/// <summary>
/// A user is sent a warning. Broadcasters and moderators can see the warning’s details.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadWarnings"/> or <see cref="Scope.ModeratorManageWarnings"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) to receive warning sent notifications for.</param>
/// <param name="ModeratorUserId">
/// The user id of the broadcaster or a moderator in a broadcaster's chat.
/// This user must have created a user access token that includes <see cref="Scope.ModeratorReadWarnings"/> or <see cref="Scope.ModeratorManageWarnings"/> for this application.
/// </param>
public sealed record ChannelWarningSend(string BroadcasterUserId, string ModeratorUserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.CHANNEL_WARNING_SEND;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId)
            .Set("moderator_user_id", ModeratorUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
