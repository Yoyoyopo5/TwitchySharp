using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.Channel;
/// <summary>
/// A broadcaster raids another broadcaster’s channel.
/// </summary>
/// <remarks>
/// No authorization required.
/// You can use the built-in static methods <see cref="From(string)"/> and <see cref="To(string)"/> as well as provided extension methods of the same name to help create this subscription.
/// </remarks>
/// <param name="FromBroadcasterUserId">
/// The broadcaster user ID that created the channel raid you want to get notifications for. 
/// Use this parameter if you want to know when a specific broadcaster raids another broadcaster. 
/// The channel raid condition must include either <paramref name="FromBroadcasterUserId"/> or <paramref name="ToBroadcasterUserId"/>.
/// </param>
/// <param name="ToBroadcasterUserId">
/// The broadcaster user ID that received the channel raid you want to get notifications for. 
/// Use this parameter if you want to know when a specific broadcaster is raided by another broadcaster. 
/// The channel raid condition must include either <paramref name="FromBroadcasterUserId"/> or <paramref name="ToBroadcasterUserId"/>.
/// </param>
public sealed record ChannelRaid(string? ToBroadcasterUserId, string? FromBroadcasterUserId = null) // May need to remove this primary constuctor IF setting both conditions is not allowed.
    : IEventSubSubscriptionType
{
    /// <summary>
    /// Subscribe to raids from a specific channel.
    /// Use this factory method if you want to know when a specific broadcaster raids another broadcaster.
    /// </summary>
    /// <param name="fromBroadcasterUserId">The broadcaster user ID that created the channel raid you want to get notifications for.</param>
    /// <returns></returns>
    public static ChannelRaid From(string fromBroadcasterUserId)
        => new ChannelRaid(null, fromBroadcasterUserId);

    /// <summary>
    /// Subscribe to raids to a specific channel.
    /// Use this factory method if you want to know when a specific broadcaster is raided by another broadcaster.
    /// </summary>
    /// <param name="toBroadcasterUserId">The broadcaster user ID that received the channel raid you want to get notifications for.</param>
    /// <returns></returns>
    public static ChannelRaid To(string toBroadcasterUserId)
        => new ChannelRaid(toBroadcasterUserId, null);

    public string Type => EventSubSubscriptionTypeNames.CHANNEL_RAID;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("from_broadcaster_user_id", FromBroadcasterUserId)
            .Set("to_broadcaster_user_id", ToBroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}

public static class ChannelRaidFluentExtensions
{
    // Not sure if we can include BOTH condition parameters in the request.
    // I'm adding these here to test that case, but they can be removed if only one can be provided.
    // We may also need to update the ChannelRaid class to prevent setting both parameters if that is the case.

    /// <summary>
    /// Subscribe to raids from a specific channel.
    /// Use this extension method if you want to know when a specific broadcaster raids another specific broadcaster.
    /// </summary>
    /// <param name="fromBroadcasterUserId">The broadcaster user ID that created the channel raid you want to get notifications for.</param>
    /// <returns></returns>
    public static ChannelRaid From(this ChannelRaid sub, string fromBroadcasterUserId)
        => sub with { FromBroadcasterUserId = fromBroadcasterUserId };

    /// <summary>
    /// Subscribe to raids to a specific channel.
    /// Use this extension method if you want to know when a specific broadcaster is raided by another specific broadcaster.
    /// </summary>
    /// <param name="toBroadcasterUserId">The broadcaster user ID that received the channel raid you want to get notifications for.</param>
    /// <returns></returns>
    public static ChannelRaid To(this ChannelRaid sub, string toBroadcasterUserId)
        => sub with { ToBroadcasterUserId = toBroadcasterUserId };
}
