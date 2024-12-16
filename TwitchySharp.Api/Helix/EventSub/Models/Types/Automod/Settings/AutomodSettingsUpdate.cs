using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.Automod.Settings;
/// <summary>
/// A notification is sent when a broadcaster’s automod settings are updated.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadAutomodSettings"/>.
/// The user who created the access token must be the same user as the <paramref name="ModeratorUserId"/>.
/// </remarks>
/// <param name="BroadcasterUserId">User id of the broadcaster (channel).</param>
/// <param name="ModeratorUserId">User id of a moderator in the broadcaster's chat. This can also be the broadcaster.</param>
public sealed record AutomodSettingsUpdate(string BroadcasterUserId, string ModeratorUserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.AUTOMOD_SETTINGS_UPDATE;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId)
            .Set("moderator_user_id", ModeratorUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
