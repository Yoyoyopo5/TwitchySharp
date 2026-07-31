namespace TwitchySharp;
/// <summary>
/// Contains static definitions of Twitch <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types">EventSub subscription types</see>.
/// You can also create your own subscription type using the public constructor (this may be useful if Twitch adds a new type that isn't available as a static definition yet).
/// </summary>
/// <param name="type">The name of the type.</param>
/// <param name="version">The version of the type.</param>
public record EventSubSubscriptionType(
    EventSubSubscriptionTypeName Type,
    EventSubSubscriptionTypeVersion Version
    )
{
    /// <summary>
    /// A user is notified if a message is caught by automod for review.
    /// </summary>
    public static EventSubSubscriptionType AutomodMessageHold { get; } = new(EventSubSubscriptionTypeName.AutomodMessageHold, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A user is notified if a message is caught by automod for review. 
    /// Only public blocked terms trigger notifications, not private ones.
    /// </summary>
    public static EventSubSubscriptionType AutomodMessageHoldV2 { get; } = new(EventSubSubscriptionTypeName.AutomodMessageHold, EventSubSubscriptionTypeVersion.Version2);

    /// <summary>
    /// A message in the automod queue had its status changed.
    /// </summary>
    public static EventSubSubscriptionType AutomodMessageUpdate { get; } = new(EventSubSubscriptionTypeName.AutomodMessageUpdate, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A message in the automod queue had its status changed. Only public blocked terms trigger notifications, not private ones.
    /// </summary>
    public static EventSubSubscriptionType AutomodMessageUpdateV2 { get; } = new(EventSubSubscriptionTypeName.AutomodMessageUpdate, EventSubSubscriptionTypeVersion.Version2);

    /// <summary>
    /// A notification is sent when a broadcaster’s automod settings are updated.
    /// </summary>
    public static EventSubSubscriptionType AutomodSettingsUpdate { get; } = new(EventSubSubscriptionTypeName.AutomodSettingsUpdate, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A notification is sent when a broadcaster’s automod terms are updated. Changes to private terms are not sent.
    /// </summary>
    public static EventSubSubscriptionType AutomodTermsUpdate { get; } = new(EventSubSubscriptionTypeName.AutomodTermsUpdate, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A notification is sent whenever Bits are used on a channel.
    /// <br/>
    /// Currently, this event will be sent when a user:
    /// <list type="number">
    /// <item>
    /// Cheers in a channel.
    /// </item>
    /// <item>
    /// Uses a Power-Up (Will not emit when a streamer uses a Power-Up for free in their own channel).
    /// </item>
    /// <item>
    /// Sends Combos.
    /// </item>
    /// </list>
    /// <br/>
    /// Bits transactions via Twitch Extensions are not included in this subscription type.
    /// </summary>
    public static EventSubSubscriptionType ChannelBitsUse { get; } = new(EventSubSubscriptionTypeName.ChannelBitsUse, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A broadcaster updates their channel properties e.g., category, title, content classification labels, broadcast, or language.
    /// </summary>
    public static EventSubSubscriptionType ChannelUpdate { get; } = new(EventSubSubscriptionTypeName.ChannelUpdate, EventSubSubscriptionTypeVersion.Version2);

    /// <summary>
    /// A specified channel receives a follow.
    /// </summary>
    public static EventSubSubscriptionType ChannelFollow { get; } = new(EventSubSubscriptionTypeName.ChannelFollow, EventSubSubscriptionTypeVersion.Version2);

    /// <summary>
    /// A midroll commercial break has started running.
    /// </summary>
    public static EventSubSubscriptionType ChannelAdBreakBegin { get; } = new(EventSubSubscriptionTypeName.ChannelAdBreakBegin, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A moderator or bot has cleared all messages from the chat room.
    /// </summary>
    public static EventSubSubscriptionType ChannelChatClear { get; } = new(EventSubSubscriptionTypeName.ChannelChatClear, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A moderator or bot has cleared all messages from a specific user.
    /// </summary>
    /// <remarks>
    /// This is typically due to a ban or timeout action.
    /// </remarks>
    public static EventSubSubscriptionType ChannelChatClearUserMessages { get; } = new(EventSubSubscriptionTypeName.ChannelChatClearUserMessages, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// Any user sends a message to a specific chat room.
    /// </summary>
    public static EventSubSubscriptionType ChannelChatMessage { get; } = new(EventSubSubscriptionTypeName.ChannelChatMessage, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A moderator has removed a specific message.
    /// </summary>
    public static EventSubSubscriptionType ChannelChatMessageDelete { get; } = new(EventSubSubscriptionTypeName.ChannelChatMessageDelete, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A notification for when an event that appears in chat has occurred.
    /// </summary>
    public static EventSubSubscriptionType ChannelChatNotification { get; } = new(EventSubSubscriptionTypeName.ChannelChatNotification, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A notification for when a broadcaster’s chat settings are updated.
    /// </summary>
    public static EventSubSubscriptionType ChannelChatSettingsUpdate { get; } = new(EventSubSubscriptionTypeName.ChannelChatSettingsUpdate, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A user is notified if their message is caught by automod.
    /// </summary>
    public static EventSubSubscriptionType ChannelChatUserMessageHold { get; } = new(EventSubSubscriptionTypeName.ChannelChatUserMessageHold, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A user is notified if their message’s automod status is updated.
    /// </summary>
    public static EventSubSubscriptionType ChannelChatUserMessageUpdate { get; } = new(EventSubSubscriptionTypeName.ChannelChatUserMessageUpdate, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A notification when a channel becomes active in an active shared chat session.
    /// </summary>
    public static EventSubSubscriptionType ChannelSharedChatSessionBegin { get; } = new(EventSubSubscriptionTypeName.ChannelSharedChatSessionBegin, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A notification when the active shared chat session the channel is in changes.
    /// </summary>
    public static EventSubSubscriptionType ChannelSharedChatSessionUpdate { get; } = new(EventSubSubscriptionTypeName.ChannelSharedChatSessionUpdate, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A notification when a channel leaves a shared chat session or the session ends.
    /// </summary>
    public static EventSubSubscriptionType ChannelSharedChatSessionEnd { get; } = new(EventSubSubscriptionTypeName.ChannelSharedChatSessionEnd, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A notification is sent when a specified channel receives a subscriber. This does not include resubscribes.
    /// </summary>
    public static EventSubSubscriptionType ChannelSubscribe { get; } = new(EventSubSubscriptionTypeName.ChannelSubscribe, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A notification when a subscription to the specified channel ends.
    /// </summary>
    public static EventSubSubscriptionType ChannelSubscriptionEnd { get; } = new(EventSubSubscriptionTypeName.ChannelSubscriptionEnd, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A notification when a viewer gives a gift subscription to one or more users in the specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelSubscriptionGift { get; } = new(EventSubSubscriptionTypeName.ChannelSubscriptionGift, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A notification when a user sends a resubscription chat message in a specific channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelSubscriptionMessage { get; } = new(EventSubSubscriptionTypeName.ChannelSubscriptionMessage, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A user cheers on the specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelCheer { get; } = new(EventSubSubscriptionTypeName.ChannelCheer, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A broadcaster raids another broadcaster’s channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelRaid { get; } = new(EventSubSubscriptionTypeName.ChannelRaid, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A viewer is banned from the specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelBan { get; } = new(EventSubSubscriptionTypeName.ChannelBan, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A viewer is unbanned from the specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelUnban { get; } = new(EventSubSubscriptionTypeName.ChannelUnban, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A user creates an unban request.
    /// </summary>
    public static EventSubSubscriptionType ChannelUnbanRequestCreate { get; } = new(EventSubSubscriptionTypeName.ChannelUnbanRequestCreate, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// An unban request has been resolved.
    /// </summary>
    public static EventSubSubscriptionType ChannelUnbanRequestResolve { get; } = new(EventSubSubscriptionTypeName.ChannelUnbanRequestResolve, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A moderator performs a moderation action in a channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelModerate { get; } = new(EventSubSubscriptionTypeName.ChannelModerate, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A moderator performs a moderation action in a channel. Includes warnings.
    /// </summary>
    public static EventSubSubscriptionType ChannelModerateV2 { get; } = new(EventSubSubscriptionTypeName.ChannelModerate, EventSubSubscriptionTypeVersion.Version2);

    /// <summary>
    /// Moderator privileges were added to a user on a specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelModeratorAdd { get; } = new(EventSubSubscriptionTypeName.ChannelModeratorAdd, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// Moderator privileges were removed from a user on a specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelModeratorRemove { get; } = new(EventSubSubscriptionTypeName.ChannelModeratorRemove, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// The host began a new Guest Star session.
    /// </summary>
    public static EventSubSubscriptionType ChannelGuestStarSessionBegin { get; } = new(EventSubSubscriptionTypeName.ChannelGuestStarSessionBegin, EventSubSubscriptionTypeVersion.Beta);

    /// <summary>
    /// A running Guest Star session has ended.
    /// </summary>
    public static EventSubSubscriptionType ChannelGuestStarSessionEnd { get; } = new(EventSubSubscriptionTypeName.ChannelGuestStarSessionEnd, EventSubSubscriptionTypeVersion.Beta);

    /// <summary>
    /// A guest or a slot is updated in an active Guest Star session.
    /// </summary>
    public static EventSubSubscriptionType ChannelGuestStarGuestUpdate { get; } = new(EventSubSubscriptionTypeName.ChannelGuestStarGuestUpdate, EventSubSubscriptionTypeVersion.Beta);

    /// <summary>
    /// The host preferences for Guest Star have been updated.
    /// </summary>
    public static EventSubSubscriptionType ChannelGuestStarSettingsUpdate { get; } = new(EventSubSubscriptionTypeName.ChannelGuestStarSettingsUpdate, EventSubSubscriptionTypeVersion.Beta);

    /// <summary>
    /// A viewer has redeemed an automatic channel points reward on the specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPointsAutomaticRewardRedemptionAdd { get; } = new(EventSubSubscriptionTypeName.ChannelPointsAutomaticRewardRedemptionAdd, EventSubSubscriptionTypeVersion.Version1);
    /// <summary>
    /// A viewer has redeemed an automatic channel points reward on the specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPointsAutomaticRewardRedemptionAddV2 { get; } = new(EventSubSubscriptionTypeName.ChannelPointsAutomaticRewardRedemptionAdd, EventSubSubscriptionTypeVersion.Version2);

    /// <summary>
    /// A custom channel points reward has been created for the specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPointsCustomRewardAdd { get; } = new(EventSubSubscriptionTypeName.ChannelPointsCustomRewardAdd, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A custom channel points reward has been updated for the specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPointsCustomRewardUpdate { get; } = new(EventSubSubscriptionTypeName.ChannelPointsCustomRewardUpdate, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A custom channel points reward has been removed from the specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPointsCustomRewardRemove { get; } = new(EventSubSubscriptionTypeName.ChannelPointsCustomRewardRemove, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A viewer has redeemed a custom channel points reward on the specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPointsCustomRewardRedemptionAdd { get; } = new(EventSubSubscriptionTypeName.ChannelPointsCustomRewardRedemptionAdd, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A redemption of a channel points custom reward has been updated for the specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPointsCustomRewardRedemptionUpdate { get; } = new(EventSubSubscriptionTypeName.ChannelPointsCustomRewardRedemptionUpdate, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A poll started on a specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPollBegin { get; } = new(EventSubSubscriptionTypeName.ChannelPollBegin, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// Users respond to a poll on a specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPollProgress { get; } = new(EventSubSubscriptionTypeName.ChannelPollProgress, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A poll ended on a specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPollEnd { get; } = new(EventSubSubscriptionTypeName.ChannelPollEnd, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A Prediction started on a specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPredictionBegin { get; } = new(EventSubSubscriptionTypeName.ChannelPredictionBegin, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// Users participated in a Prediction on a specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPredictionProgress { get; } = new(EventSubSubscriptionTypeName.ChannelPredictionProgress, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A Prediction was locked on a specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPredictionLock { get; } = new(EventSubSubscriptionTypeName.ChannelPredictionLock, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A Prediction ended on a specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPredictionEnd { get; } = new(EventSubSubscriptionTypeName.ChannelPredictionEnd, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A chat message has been sent by a suspicious user.
    /// </summary>
    public static EventSubSubscriptionType ChannelSuspiciousUserMessage { get; } = new(EventSubSubscriptionTypeName.ChannelSuspiciousUserMessage, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A suspicious user has been updated.
    /// </summary>
    public static EventSubSubscriptionType ChannelSuspiciousUserUpdate { get; } = new(EventSubSubscriptionTypeName.ChannelSuspiciousUserUpdate, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A VIP is added to the channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelVIPAdd { get; } = new(EventSubSubscriptionTypeName.ChannelVipAdd, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A VIP is removed from the channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelVIPRemove { get; } = new(EventSubSubscriptionTypeName.ChannelVipRemove, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A user acknowledges a warning. Broadcasters and moderators can see the warning’s details.
    /// </summary>
    public static EventSubSubscriptionType ChannelWarningAcknowledgement { get; } = new(EventSubSubscriptionTypeName.ChannelWarningAcknowledgement, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A user is sent a warning. Broadcasters and moderators can see the warning’s details.
    /// </summary>
    public static EventSubSubscriptionType ChannelWarningSend { get; } = new(EventSubSubscriptionTypeName.ChannelWarningSend, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// Sends an event notification when a user donates to the broadcaster’s charity campaign.
    /// </summary>
    public static EventSubSubscriptionType CharityDonation { get; } = new(EventSubSubscriptionTypeName.CharityDonation, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// Sends an event notification when the broadcaster starts a charity campaign.
    /// </summary>
    public static EventSubSubscriptionType CharityCampaignStart { get; } = new(EventSubSubscriptionTypeName.CharityCampaignStart, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// Sends an event notification when progress is made towards the campaign’s goal or when the broadcaster changes the fundraising goal.
    /// </summary>
    public static EventSubSubscriptionType CharityCampaignProgress { get; } = new(EventSubSubscriptionTypeName.CharityCampaignProgress, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// Sends an event notification when the broadcaster stops a charity campaign.
    /// </summary>
    public static EventSubSubscriptionType CharityCampaignStop { get; } = new(EventSubSubscriptionTypeName.CharityCampaignStop, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// Sends a notification when EventSub disables a shard due to the status of the underlying transport changing.
    /// </summary>
    public static EventSubSubscriptionType ConduitShardDisabled { get; } = new(EventSubSubscriptionTypeName.ConduitShardDisabled, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// An entitlement for a Drop is granted to a user.
    /// </summary>
    public static EventSubSubscriptionType DropEntitlementGrant { get; } = new(EventSubSubscriptionTypeName.DropEntitlementGrant, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A Bits transaction occurred for a specified Twitch Extension.
    /// </summary>
    public static EventSubSubscriptionType ExtensionBitsTransactionCreate { get; } = new(EventSubSubscriptionTypeName.ExtensionBitsTransactionCreate, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// Get notified when a broadcaster begins a goal.
    /// </summary>
    public static EventSubSubscriptionType GoalBegin { get; } = new(EventSubSubscriptionTypeName.GoalBegin, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// Get notified when progress (either positive or negative) is made towards a broadcaster’s goal.
    /// </summary>
    public static EventSubSubscriptionType GoalProgress { get; } = new(EventSubSubscriptionTypeName.GoalProgress, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// Get notified when a broadcaster ends a goal.
    /// </summary>
    public static EventSubSubscriptionType GoalEnd { get; } = new(EventSubSubscriptionTypeName.GoalEnd, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A Hype Train begins on the specified channel.
    /// </summary>
    public static EventSubSubscriptionType HypeTrainBegin { get; } = new(EventSubSubscriptionTypeName.HypeTrainBegin, EventSubSubscriptionTypeVersion.Version2);

    /// <summary>
    /// A Hype Train makes progress on the specified channel.
    /// </summary>
    public static EventSubSubscriptionType HypeTrainProgress { get; } = new(EventSubSubscriptionTypeName.HypeTrainProgress, EventSubSubscriptionTypeVersion.Version2);

    /// <summary>
    /// A Hype Train ends on the specified channel.
    /// </summary>
    public static EventSubSubscriptionType HypeTrainEnd { get; } = new(EventSubSubscriptionTypeName.HypeTrainEnd, EventSubSubscriptionTypeVersion.Version2);

    /// <summary>
    /// Sends a notification when the broadcaster activates Shield Mode.
    /// </summary>
    public static EventSubSubscriptionType ShieldModeBegin { get; } = new(EventSubSubscriptionTypeName.ShieldModeBegin, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// Sends a notification when the broadcaster deactivates Shield Mode.
    /// </summary>
    public static EventSubSubscriptionType ShieldModeEnd { get; } = new(EventSubSubscriptionTypeName.ShieldModeEnd, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// Sends a notification when the specified broadcaster sends a Shoutout.
    /// </summary>
    public static EventSubSubscriptionType ShoutoutCreate { get; } = new(EventSubSubscriptionTypeName.ShoutoutCreate, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// Sends a notification when the specified broadcaster receives a Shoutout.
    /// </summary>
    public static EventSubSubscriptionType ShoutoutReceived { get; } = new(EventSubSubscriptionTypeName.ShoutoutReceived, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// The specified broadcaster starts a stream.
    /// </summary>
    public static EventSubSubscriptionType StreamOnline { get; } = new(EventSubSubscriptionTypeName.StreamOnline, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// The specified broadcaster stops a stream.
    /// </summary>
    public static EventSubSubscriptionType StreamOffline { get; } = new(EventSubSubscriptionTypeName.StreamOffline, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A user’s authorization has been granted to your client id.
    /// </summary>
    public static EventSubSubscriptionType UserAuthorizationGrant { get; } = new(EventSubSubscriptionTypeName.UserAuthorizationGrant, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A user’s authorization has been revoked for your client id.
    /// </summary>
    public static EventSubSubscriptionType UserAuthorizationRevoke { get; } = new(EventSubSubscriptionTypeName.UserAuthorizationRevoke, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A user has updated their account.
    /// </summary>
    public static EventSubSubscriptionType UserUpdate { get; } = new(EventSubSubscriptionTypeName.UserUpdate, EventSubSubscriptionTypeVersion.Version1);

    /// <summary>
    /// A user receives a whisper.
    /// </summary>
    public static EventSubSubscriptionType WhisperReceived { get; } = new(EventSubSubscriptionTypeName.WhisperReceived, EventSubSubscriptionTypeVersion.Version1);
}
