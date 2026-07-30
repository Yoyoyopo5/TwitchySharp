namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ConduitShardDisabled"/>.
/// </summary>
public record ConduitShardDisabledCondition : ClientCondition
{
    /// <summary>
    /// The id of the conduit the notification is for.
    /// If <see langword="null"/>, events for all of the client's conduits are sent.
    /// </summary>
    public ConduitId? ConduitId { get; init; }
}
