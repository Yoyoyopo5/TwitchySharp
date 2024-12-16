using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.Channel;
/// <summary>
/// A user cheers on the specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.BitsRead"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The broadcaster user ID for the channel you want to get cheer notifications for.
/// This user must have created a user access token including <see cref="Scope.BitsRead"/> for this application.
/// </param>
public sealed record ChannelCheer(string BroadcasterUserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.CHANNEL_CHEER;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
