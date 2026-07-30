namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ConduitShardDisabled"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#conduitsharddisabled">Conduit Shard Disabled</see> for more information.
/// </remarks>
public record ConduitShardDisabledNotification : EventSubNotification<ConduitShardDisabledEvent, ConduitShardDisabledCondition>;
