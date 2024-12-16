using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.AdBreak;
/// <summary>
/// A midroll commercial break has started running.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadAds"/>.
/// </remarks>
/// <param name="BroadcasterId">The user id of the broadcaster that you want to get Channel Ad Break begin notifications for.</param>
public sealed record ChannelAdBreakBegin(string BroadcasterId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.CHANNEL_AD_BREAK_BEGIN;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_id", BroadcasterId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
