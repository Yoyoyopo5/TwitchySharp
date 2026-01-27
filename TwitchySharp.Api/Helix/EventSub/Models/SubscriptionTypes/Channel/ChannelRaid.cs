using System.Collections.Generic;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A broadcaster raids another broadcaster's channel.
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
public sealed record ChannelRaid(UserId? ToBroadcasterUserId, UserId? FromBroadcasterUserId = null) // May need to remove this primary constuctor IF setting both conditions is not allowed.
    : IEventSubSubscriptionType
{
    /// <summary>
    /// Subscribe to raids from a specific channel.
    /// Use this factory method if you want to know when a specific broadcaster raids another broadcaster.
    /// </summary>
    /// <param name="fromBroadcasterUserId">The broadcaster user ID that created the channel raid you want to get notifications for.</param>
    /// <returns></returns>
    public static ChannelRaid From(UserId fromBroadcasterUserId)
        => new(null, fromBroadcasterUserId);

    /// <summary>
    /// Subscribe to raids to a specific channel.
    /// Use this factory method if you want to know when a specific broadcaster is raided by another broadcaster.
    /// </summary>
    /// <param name="toBroadcasterUserId">The broadcaster user ID that received the channel raid you want to get notifications for.</param>
    /// <returns></returns>
    public static ChannelRaid To(UserId toBroadcasterUserId)
        => new(toBroadcasterUserId, null);

    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelRaid;

    private readonly EventSubSubscriptionCondition _condition =
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
    public static ChannelRaid From(this ChannelRaid sub, UserId fromBroadcasterUserId)
        => sub with { FromBroadcasterUserId = fromBroadcasterUserId };

    /// <summary>
    /// Subscribe to raids to a specific channel.
    /// Use this extension method if you want to know when a specific broadcaster is raided by another specific broadcaster.
    /// </summary>
    /// <param name="toBroadcasterUserId">The broadcaster user ID that received the channel raid you want to get notifications for.</param>
    /// <returns></returns>
    public static ChannelRaid To(this ChannelRaid sub, UserId toBroadcasterUserId)
        => sub with { ToBroadcasterUserId = toBroadcasterUserId };
}
