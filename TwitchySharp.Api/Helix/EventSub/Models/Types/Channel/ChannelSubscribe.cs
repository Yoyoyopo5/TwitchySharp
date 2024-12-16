using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.Channel;
/// <summary>
/// A notification is sent when a specified channel receives a subscriber. This does not include resubscribes.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadSubscriptions"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster (channel) you want to get subscribe notifications for.
/// This user must have created a user access token that includes <see cref="Scope.ChannelReadSubscriptions"/> for this application.
/// </param>
public sealed record ChannelSubscribe(string BroadcasterUserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.CHANNEL_SUBSCRIBE;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
