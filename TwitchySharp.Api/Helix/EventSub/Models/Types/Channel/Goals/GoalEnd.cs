using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.Goals;
/// <summary>
/// Get notified when a broadcaster ends a goal.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadGoals"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster to get notified about. 
/// This user must have created a user access token for this application that includes <see cref="Scope.ChannelReadGoals"/>.
/// </param>
public sealed record GoalEnd(string BroadcasterUserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.GOAL_END;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
