namespace TwitchySharp.EventSub.Notifications;

/// <inheritdoc cref="EventSubSubscriptionType.ChannelCustomPowerUpRedemptionAdd"/>
public record ChannelCustomPowerUpRedemptionAddNotification
    : EventSubNotification<ChannelCustomPowerUpRedemptionAddEvent, ChannelCustomPowerUpRedemptionAddCondition>;
