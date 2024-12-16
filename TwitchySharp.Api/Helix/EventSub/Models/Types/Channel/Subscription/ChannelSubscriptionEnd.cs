using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.Subscription;
/// <summary>
/// A notification when a subscription to the specified channel ends (expires).
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadSubscriptions"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster (channel) you want to get subscription end notifications for.
/// This user must have created a user access token that includes <see cref="Scope.ChannelReadSubscriptions"/> for this application.
/// </param>
public sealed record ChannelSubscriptionEnd(string BroadcasterUserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.CHANNEL_SUBSCRIPTION_END;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
