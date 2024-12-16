namespace TwitchySharp.Api.Helix.EventSub;

// Note that this type may need to be moved into a higher scope or dependency as we develop the EventSub branch.

/// <summary>
/// Contains static definitions of Twitch <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types">EventSub subscription types</see>.
/// You can also create your own subscription type using the public constructor (this may be useful if Twitch adds a new type that isn't available as a static definition yet).
/// </summary>
/// <param name="type">The name of the type.</param>
/// <param name="version">The version of the type.</param>
public record EventSubSubscriptionType(string Type, string Version)
{
    private const string VERSION_1 = "1";
    private const string VERSION_2 = "2";
    private const string VERSION_BETA = "BETA";

    /// <summary>
    /// A user is notified if a message is caught by automod for review.
    /// </summary>
    public static EventSubSubscriptionType AutomodMessageHold { get; } = new("automod.message.hold", VERSION_1);

    /// <summary>
    /// A user is notified if a message is caught by automod for review. 
    /// Only public blocked terms trigger notifications, not private ones.
    /// </summary>
    public static EventSubSubscriptionType AutomodMessageHoldV2 { get; } = new("automod.message.hold", VERSION_2);

    /// <summary>
    /// A message in the automod queue had its status changed.
    /// </summary>
    public static EventSubSubscriptionType AutomodMessageUpdate { get; } = new("automod.message.update", VERSION_1);

    /// <summary>
    /// A message in the automod queue had its status changed. Only public blocked terms trigger notifications, not private ones.
    /// </summary>
    public static EventSubSubscriptionType AutomodMessageUpdateV2 { get; } = new("automod.message.update", VERSION_2);

    /// <summary>
    /// A notification is sent when a broadcaster’s automod settings are updated.
    /// </summary>
    public static EventSubSubscriptionType AutomodSettingsUpdate { get; } = new("automod.settings.update", VERSION_1);

    /// <summary>
    /// A notification is sent when a broadcaster’s automod terms are updated. Changes to private terms are not sent.
    /// </summary>
    public static EventSubSubscriptionType AutomodTermsUpdate { get; } = new("automod.terms.update", VERSION_1);

    /// <summary>
    /// A broadcaster updates their channel properties e.g., category, title, content classification labels, broadcast, or language.
    /// </summary>
    public static EventSubSubscriptionType ChannelUpdate { get; } = new("channel.update", VERSION_2);

    /// <summary>
    /// A specified channel receives a follow.
    /// </summary>
    public static EventSubSubscriptionType ChannelFollow { get; } = new("channel.follow", VERSION_2);

    /// <summary>
    /// A midroll commercial break has started running.
    /// </summary>
    public static EventSubSubscriptionType ChannelAdBreakBegin { get; } = new("channel.ad_break.begin", VERSION_1);

    /// <summary>
    /// A moderator or bot has cleared all messages from the chat room.
    /// </summary>
    public static EventSubSubscriptionType ChannelChatClear { get; } = new("channel.chat.clear", VERSION_1);

    /// <summary>
    /// A moderator or bot has cleared all messages from a specific user.
    /// </summary>
    public static EventSubSubscriptionType ChannelChatClearUserMessages { get; } = new("channel.chat.clear_user_messages", VERSION_1);

    /// <summary>
    /// Any user sends a message to a specific chat room.
    /// </summary>
    public static EventSubSubscriptionType ChannelChatMessage { get; } = new("channel.chat.message", VERSION_1);

    /// <summary>
    /// A moderator has removed a specific message.
    /// </summary>
    public static EventSubSubscriptionType ChannelChatMessageDelete { get; } = new("channel.chat.message_delete", VERSION_1);

    /// <summary>
    /// A notification for when an event that appears in chat has occurred.
    /// </summary>
    public static EventSubSubscriptionType ChannelChatNotification { get; } = new("channel.chat.notification", VERSION_1);

    /// <summary>
    /// A notification for when a broadcaster’s chat settings are updated.
    /// </summary>
    public static EventSubSubscriptionType ChannelChatSettingsUpdate { get; } = new("channel.chat_settings.update", VERSION_1);

    /// <summary>
    /// A user is notified if their message is caught by automod.
    /// </summary>
    public static EventSubSubscriptionType ChannelChatUserMessageHold { get; } = new("channel.chat.user_message_hold", VERSION_1);

    /// <summary>
    /// A user is notified if their message’s automod status is updated.
    /// </summary>
    public static EventSubSubscriptionType ChannelChatUserMessageUpdate { get; } = new("channel.chat.user_message_update", VERSION_1);

    /// <summary>
    /// A notification when a channel becomes active in an active shared chat session.
    /// </summary>
    public static EventSubSubscriptionType ChannelSharedChatSessionBegin { get; } = new("channel.shared_chat.begin", VERSION_1);

    /// <summary>
    /// A notification when the active shared chat session the channel is in changes.
    /// </summary>
    public static EventSubSubscriptionType ChannelSharedChatSessionUpdate { get; } = new("channel.shared_chat.update", VERSION_1);

    /// <summary>
    /// A notification when a channel leaves a shared chat session or the session ends.
    /// </summary>
    public static EventSubSubscriptionType ChannelSharedChatSessionEnd { get; } = new("channel.shared_chat.end", VERSION_1);

    /// <summary>
    /// A notification is sent when a specified channel receives a subscriber. This does not include resubscribes.
    /// </summary>
    public static EventSubSubscriptionType ChannelSubscribe { get; } = new("channel.subscribe", VERSION_1);

    /// <summary>
    /// A notification when a subscription to the specified channel ends.
    /// </summary>
    public static EventSubSubscriptionType ChannelSubscriptionEnd { get; } = new("channel.subscription.end", VERSION_1);

    /// <summary>
    /// A notification when a viewer gives a gift subscription to one or more users in the specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelSubscriptionGift { get; } = new("channel.subscription.gift", VERSION_1);

    /// <summary>
    /// A notification when a user sends a resubscription chat message in a specific channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelSubscriptionMessage { get; } = new("channel.subscription.message", VERSION_1);

    /// <summary>
    /// A user cheers on the specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelCheer { get; } = new("channel.cheer", VERSION_1);

    /// <summary>
    /// A broadcaster raids another broadcaster’s channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelRaid { get; } = new("channel.raid", VERSION_1);

    /// <summary>
    /// A viewer is banned from the specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelBan { get; } = new("channel.ban", VERSION_1);

    /// <summary>
    /// A viewer is unbanned from the specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelUnban { get; } = new("channel.unban", VERSION_1);

    /// <summary>
    /// A user creates an unban request.
    /// </summary>
    public static EventSubSubscriptionType ChannelUnbanRequestCreate { get; } = new("channel.unban_request.create", VERSION_1);

    /// <summary>
    /// An unban request has been resolved.
    /// </summary>
    public static EventSubSubscriptionType ChannelUnbanRequestResolve { get; } = new("channel.unban_request.resolve", VERSION_1);

    /// <summary>
    /// A moderator performs a moderation action in a channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelModerate { get; } = new("channel.moderate", VERSION_1);

    /// <summary>
    /// A moderator performs a moderation action in a channel. Includes warnings.
    /// </summary>
    public static EventSubSubscriptionType ChannelModerateV2 { get; } = new("channel.moderate", VERSION_2);

    /// <summary>
    /// Moderator privileges were added to a user on a specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelModeratorAdd { get; } = new("channel.moderator.add", VERSION_1);

    /// <summary>
    /// Moderator privileges were removed from a user on a specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelModeratorRemove { get; } = new("channel.moderator.remove", VERSION_1);

    /// <summary>
    /// The host began a new Guest Star session.
    /// </summary>
    public static EventSubSubscriptionType ChannelGuestStarSessionBegin { get; } = new("channel.guest_star_session.begin", VERSION_BETA);

    /// <summary>
    /// A running Guest Star session has ended.
    /// </summary>
    public static EventSubSubscriptionType ChannelGuestStarSessionEnd { get; } = new("channel.guest_star_session.end", VERSION_BETA);

    /// <summary>
    /// A guest or a slot is updated in an active Guest Star session.
    /// </summary>
    public static EventSubSubscriptionType ChannelGuestStarGuestUpdate { get; } = new("channel.guest_star_guest.update", VERSION_BETA);

    /// <summary>
    /// The host preferences for Guest Star have been updated.
    /// </summary>
    public static EventSubSubscriptionType ChannelGuestStarSettingsUpdate { get; } = new("channel.guest_star_settings.update", VERSION_BETA);

    /// <summary>
    /// A viewer has redeemed an automatic channel points reward on the specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPointsAutomaticRewardRedemption { get; } = new("channel.channel_points_automatic_reward_redemption.add", VERSION_1);

    /// <summary>
    /// A custom channel points reward has been created for the specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPointsCustomRewardAdd { get; } = new("channel.channel_points_custom_reward.add", VERSION_1);

    /// <summary>
    /// A custom channel points reward has been updated for the specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPointsCustomRewardUpdate { get; } = new("channel.channel_points_custom_reward.update", VERSION_1);

    /// <summary>
    /// A custom channel points reward has been removed from the specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPointsCustomRewardRemove { get; } = new("channel.channel_points_custom_reward.remove", VERSION_1);

    /// <summary>
    /// A viewer has redeemed a custom channel points reward on the specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPointsCustomRewardRedemptionAdd { get; } = new("channel.channel_points_custom_reward_redemption.add", VERSION_1);

    /// <summary>
    /// A redemption of a channel points custom reward has been updated for the specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPointsCustomRewardRedemptionUpdate { get; } = new("channel.channel_points_custom_reward_redemption.update", VERSION_1);

    /// <summary>
    /// A poll started on a specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPollBegin { get; } = new("channel.poll.begin", VERSION_1);

    /// <summary>
    /// Users respond to a poll on a specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPollProgress { get; } = new("channel.poll.progress", VERSION_1);

    /// <summary>
    /// A poll ended on a specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPollEnd { get; } = new("channel.poll.end", VERSION_1);

    /// <summary>
    /// A Prediction started on a specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPredictionBegin { get; } = new("channel.prediction.begin", VERSION_1);

    /// <summary>
    /// Users participated in a Prediction on a specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPredictionProgress { get; } = new("channel.prediction.progress", VERSION_1);

    /// <summary>
    /// A Prediction was locked on a specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPredictionLock { get; } = new("channel.prediction.lock", VERSION_1);

    /// <summary>
    /// A Prediction ended on a specified channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelPredictionEnd { get; } = new("channel.prediction.end", VERSION_1);

    /// <summary>
    /// A chat message has been sent by a suspicious user.
    /// </summary>
    public static EventSubSubscriptionType ChannelSuspiciousUserMessage { get; } = new("channel.suspicious_user.message", VERSION_1);

    /// <summary>
    /// A suspicious user has been updated.
    /// </summary>
    public static EventSubSubscriptionType ChannelSuspiciousUserUpdate { get; } = new("channel.suspicious_user.update", VERSION_1);

    /// <summary>
    /// A VIP is added to the channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelVIPAdd { get; } = new("channel.vip.add", VERSION_1);

    /// <summary>
    /// A VIP is removed from the channel.
    /// </summary>
    public static EventSubSubscriptionType ChannelVIPRemove { get; } = new("channel.vip.remove", VERSION_1);

    /// <summary>
    /// A user acknowledges a warning. Broadcasters and moderators can see the warning’s details.
    /// </summary>
    public static EventSubSubscriptionType ChannelWarningAcknowledgement { get; } = new("channel.warning.acknowledge", VERSION_1);

    /// <summary>
    /// A user is sent a warning. Broadcasters and moderators can see the warning’s details.
    /// </summary>
    public static EventSubSubscriptionType ChannelWarningSend { get; } = new("channel.warning.send", VERSION_1);

    /// <summary>
    /// Sends an event notification when a user donates to the broadcaster’s charity campaign.
    /// </summary>
    public static EventSubSubscriptionType CharityDonation { get; } = new("channel.charity_campaign.donate", VERSION_1);

    /// <summary>
    /// Sends an event notification when the broadcaster starts a charity campaign.
    /// </summary>
    public static EventSubSubscriptionType CharityCampaignStart { get; } = new("channel.charity_campaign.start", VERSION_1);

    /// <summary>
    /// Sends an event notification when progress is made towards the campaign’s goal or when the broadcaster changes the fundraising goal.
    /// </summary>
    public static EventSubSubscriptionType CharityCampaignProgress { get; } = new("channel.charity_campaign.progress", VERSION_1);

    /// <summary>
    /// Sends an event notification when the broadcaster stops a charity campaign.
    /// </summary>
    public static EventSubSubscriptionType CharityCampaignStop { get; } = new("channel.charity_campaign.stop", VERSION_1);

    /// <summary>
    /// Sends a notification when EventSub disables a shard due to the status of the underlying transport changing.
    /// </summary>
    public static EventSubSubscriptionType ConduitShardDisabled { get; } = new("conduit.shard.disabled", VERSION_1);

    /// <summary>
    /// An entitlement for a Drop is granted to a user.
    /// </summary>
    public static EventSubSubscriptionType DropEntitlementGrant { get; } = new("drop.entitlement.grant", VERSION_1);

    /// <summary>
    /// A Bits transaction occurred for a specified Twitch Extension.
    /// </summary>
    public static EventSubSubscriptionType ExtensionBitsTransactionCreate { get; } = new("extension.bits_transaction.create", VERSION_1);

    /// <summary>
    /// Get notified when a broadcaster begins a goal.
    /// </summary>
    public static EventSubSubscriptionType GoalBegin { get; } = new("channel.goal.begin", VERSION_1);

    /// <summary>
    /// Get notified when progress (either positive or negative) is made towards a broadcaster’s goal.
    /// </summary>
    public static EventSubSubscriptionType GoalProgress { get; } = new("channel.goal.progress", VERSION_1);

    /// <summary>
    /// Get notified when a broadcaster ends a goal.
    /// </summary>
    public static EventSubSubscriptionType GoalEnd { get; } = new("channel.goal.end", VERSION_1);

    /// <summary>
    /// A Hype Train begins on the specified channel.
    /// </summary>
    public static EventSubSubscriptionType HypeTrainBegin { get; } = new("channel.hype_train.begin", VERSION_1);

    /// <summary>
    /// A Hype Train makes progress on the specified channel.
    /// </summary>
    public static EventSubSubscriptionType HypeTrainProgress { get; } = new("channel.hype_train.progress", VERSION_1);

    /// <summary>
    /// A Hype Train ends on the specified channel.
    /// </summary>
    public static EventSubSubscriptionType HypeTrainEnd { get; } = new("channel.hype_train.end", VERSION_1);

    /// <summary>
    /// Sends a notification when the broadcaster activates Shield Mode.
    /// </summary>
    public static EventSubSubscriptionType ShieldModeBegin { get; } = new("channel.shield_mode.begin", VERSION_1);

    /// <summary>
    /// Sends a notification when the broadcaster deactivates Shield Mode.
    /// </summary>
    public static EventSubSubscriptionType ShieldModeEnd { get; } = new("channel.shield_mode.end", VERSION_1);

    /// <summary>
    /// Sends a notification when the specified broadcaster sends a Shoutout.
    /// </summary>
    public static EventSubSubscriptionType ShoutoutCreate { get; } = new("channel.shoutout.create", VERSION_1);

    /// <summary>
    /// Sends a notification when the specified broadcaster receives a Shoutout.
    /// </summary>
    public static EventSubSubscriptionType ShoutoutReceived { get; } = new("channel.shoutout.receive", VERSION_1);

    /// <summary>
    /// The specified broadcaster starts a stream.
    /// </summary>
    public static EventSubSubscriptionType StreamOnline { get; } = new("stream.online", VERSION_1);

    /// <summary>
    /// The specified broadcaster stops a stream.
    /// </summary>
    public static EventSubSubscriptionType StreamOffline { get; } = new("stream.offline", VERSION_1);

    /// <summary>
    /// A user’s authorization has been granted to your client id.
    /// </summary>
    public static EventSubSubscriptionType UserAuthorizationGrant { get; } = new("user.authorization.grant", VERSION_1);

    /// <summary>
    /// A user’s authorization has been revoked for your client id.
    /// </summary>
    public static EventSubSubscriptionType UserAuthorizationRevoke { get; } = new("user.authorization.revoke", VERSION_1);

    /// <summary>
    /// A user has updated their account.
    /// </summary>
    public static EventSubSubscriptionType UserUpdate { get; } = new("user.update", VERSION_1);

    /// <summary>
    /// A user receives a whisper.
    /// </summary>
    public static EventSubSubscriptionType WhisperReceived { get; } = new("user.whisper.message", VERSION_1);
}
