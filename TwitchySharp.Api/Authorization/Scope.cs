using System;
using System.Collections.Generic;
using System.Text;
using TwitchySharp.Api.Authorization.Requests;

namespace TwitchySharp.Api.Authorization;
public record Scope
{
    /// <summary>
    /// Obtain OpenID claims of the authorizing user.
    /// Required for: 
    /// <see cref="UserInfoRequest"/>
    /// </summary>
    public static Scope OpenId { get; } = new("openid");
    /// <summary>
    /// View analytics data for the Twitch Extensions owned by the authenticated account.
    /// Required for: 
    /// </summary>
    public static Scope AnalyticsReadExtensions { get; } = new("analytics:read:extensions");
    /// <summary>
    /// View analytics data for the games owned by the authenticated account.
    /// Required for: 
    /// </summary>
    public static Scope AnalyticsReadGames { get; } = new("analytics:read:games");
    /// <summary>
    /// View Bits information for a channel.
    /// Required for: 
    /// </summary>
    public static Scope BitsRead { get; } = new("bits:read");
    /// <summary>
    /// Joins your channel’s chatroom as a bot user, and perform chat-related actions as that user.
    /// Required for:
    /// </summary>
    public static Scope ChannelBot { get; } = new("channel:bot");
    /// <summary>
    /// Manage ads schedule on a channel.
    /// Required for: 
    /// </summary>
    public static Scope ChannelManageAds { get; } = new("channel:manage:ads");
    /// <summary>
    /// Read the ads schedule and details on a channel.
    /// Required for:
    /// </summary>
    public static Scope ChannelReadAds { get; } = new("channel:read:ads");
    /// <summary>
    /// Manage a channel’s broadcast configuration, including updating channel configuration and managing stream markers and stream tags.
    /// Required for:
    /// </summary>
    public static Scope ChannelManageBroadcast { get; } = new("channel:manage:broadcast");
    /// <summary>
    /// Read charity campaign details and user donations on your channel.
    /// Required for:
    /// </summary>
    public static Scope ChannelReadCharity { get; } = new("channel:read:charity");
    /// <summary>
    /// Run commercials on a channel.
    /// Required for: 
    /// </summary>
    public static Scope ChannelEditCommercial { get; } = new("channel:edit:commercial");
    /// <summary>
    /// View a list of users with the editor role for a channel.
    /// Required for:
    /// </summary>
    public static Scope ChannelReadEditors { get; } = new("channel:read:editors");
    /// <summary>
    /// Manage a channel’s Extension configuration, including activating Extensions.
    /// Required for:
    /// </summary>
    public static Scope ChannelManageExtensions { get; } = new("channel:manage:extensions");
    /// <summary>
    /// View Creator Goals for a channel.
    /// Required for:
    /// </summary>
    public static Scope ChannelReadGoals { get; } = new("channel:read:goals");
    /// <summary>
    /// Read Guest Star details for your channel.
    /// Required for:
    /// </summary>
    public static Scope ChannelReadGuestStar { get; } = new("channel:read:guest_star");
    /// <summary>
    /// Manage Guest Star for your channel.
    /// Required for:
    /// </summary>
    public static Scope ChannelManageGuestStar { get; } = new("channel:manage:guest_star");
    /// <summary>
    /// View Hype Train information for a channel.
    /// Required for:
    /// </summary>
    public static Scope ChannelReadHypeTrain { get; } = new("channel:read:hype_train");
    /// <summary>
    /// Add or remove the moderator role from users in your channel.
    /// Required for:
    /// </summary>
    public static Scope ChannelManageModerators { get; } = new("channel:manage:moderators");
    /// <summary>
    /// View a channel’s polls.
    /// Required for:
    /// </summary>
    public static Scope ChannelReadPolls { get; } = new("channel:read:polls");
    /// <summary>
    /// Manage a channel’s polls.
    /// Required for:
    /// </summary>
    public static Scope ChannelManagePolls { get; } = new("channel:manage:polls");
    /// <summary>
    /// View a channel’s Channel Points Predictions.
    /// Required for:
    /// </summary>
    public static Scope ChannelReadPredictions { get; } = new("channel:read:predictions");
    /// <summary>
    /// Manage of channel’s Channel Points Predictions.
    /// Required for:
    /// </summary>
    public static Scope ChannelManagePredictions { get; } = new("channel:manage:predictions");
    /// <summary>
    /// Manage a channel raiding another channel.
    /// Required for:
    /// </summary>
    public static Scope ChannelManageRaids { get; } = new("channel:manage:raids");
    /// <summary>
    /// View Channel Points custom rewards and their redemptions on a channel.
    /// Required for:
    /// </summary>
    public static Scope ChannelReadRedemptions { get; } = new("channel:read:redemptions");
    /// <summary>
    /// Manage Channel Points custom rewards and their redemptions on a channel.
    /// Required for:
    /// </summary>
    public static Scope ChannelManageRedemptions { get; } = new("channel:manage:redemptions");
    /// <summary>
    /// Manage a channel’s stream schedule.
    /// Required for:
    /// </summary>
    public static Scope ChannelManageSchedule { get; } = new("channel:manage:schedule");
    /// <summary>
    /// View an authorized user’s stream key.
    /// Required for:
    /// </summary>
    public static Scope ChannelReadStreamKey { get; } = new("channel:read:stream_key");
    /// <summary>
    /// View a list of all subscribers to a channel and check if a user is subscribed to a channel.
    /// Required for:
    /// </summary>
    public static Scope ChannelReadSubscriptions { get; } = new("channel:read:subscriptions");
    /// <summary>
    /// Manage a channel’s videos, including deleting videos.
    /// Required for:
    /// </summary>
    public static Scope ChannelManageVideos { get; } = new("channel:manage:videos");
    /// <summary>
    /// Read the list of VIPs in a channel.
    /// Required for:
    /// </summary>
    public static Scope ChannelReadVips { get; } = new("channel:read:vips");
    /// <summary>
    /// Add or remove the VIP role from users in your channel.
    /// Required for:
    /// </summary>
    public static Scope ChannelManageVips { get; } = new("channel:manage:vips");
    /// <summary>
    /// Manage Clips for a channel.
    /// Required for:
    /// </summary>
    public static Scope ClipsEdit { get; } = new("clips:edit");
    /// <summary>
    /// View a channel’s moderation data including Moderators, Bans, Timeouts, and Automod settings.
    /// Required for:
    /// </summary>
    public static Scope ModerationRead { get; } = new("moderation:read");
    /// <summary>
    /// Send announcements in channels where you have the moderator role.
    /// Required for:
    /// </summary>
    public static Scope ModeratorManageAnnouncements { get; } = new("moderator:manage:announcements");
    /// <summary>
    /// Manage messages held for review by AutoMod in channels where you are a moderator.
    /// Required for:
    /// </summary>
    public static Scope ModeratorManageAutomod { get; } = new("moderator:manage:automod");
    /// <summary>
    /// View a broadcaster’s AutoMod settings.
    /// Required for:
    /// </summary>
    public static Scope ModeratorReadAutomodSettings { get; } = new("moderator:read:automod_settings");
    /// <summary>
    /// Manage a broadcaster’s AutoMod settings.
    /// Required for:
    /// </summary>
    public static Scope ModeratorManageAutomodSettings { get; } = new("moderator:manage:automod_settings");
    /// <summary>
    /// Read the list of bans or unbans in channels where you have the moderator role.
    /// Required for:
    /// </summary>
    public static Scope ModeratorReadBannedUsers { get; } = new("moderator:read:banned_users");
    /// <summary>
    /// Ban and unban users.
    /// Required for:
    /// </summary>
    public static Scope ModeratorManageBannedUsers { get; } = new("moderator:manage:banned_users");
    /// <summary>
    /// View a broadcaster’s list of blocked terms.
    /// Required for:
    /// </summary>
    public static Scope ModeratorReadBlockedTerms { get; } = new("moderator:read:blocked_terms");
    /// <summary>
    /// Read deleted chat messages in channels where you have the moderator role.
    /// Required for:
    /// </summary>
    public static Scope ModeratorReadChatMessages { get; } = new("moderator:read:chat_messages");
    /// <summary>
    /// Manage a broadcaster’s list of blocked terms.
    /// Required for:
    /// </summary>
    public static Scope ModeratorManageBlockedTerms { get; } = new("moderator:manage:blocked_terms");
    /// <summary>
    /// Delete chat messages in channels where you have the moderator role.
    /// Required for:
    /// </summary>
    public static Scope ModeratorManageChatMessages { get; } = new("moderator:manage:chat_messages");
    /// <summary>
    /// View a broadcaster’s chat room settings.
    /// Required for:
    /// </summary>
    public static Scope ModeratorReadChatSettings { get; } = new("moderator:read:chat_settings");
    /// <summary>
    /// Manage a broadcaster’s chat room settings.
    /// Required for:
    /// </summary>
    public static Scope ModeratorManageChatSettings { get; } = new("moderator:manage:chat_settings");
    /// <summary>
    /// View the chatters in a broadcaster’s chat room.
    /// Required for:
    /// </summary>
    public static Scope ModeratorReadChatters { get; } = new("moderator:read:chatters");
    /// <summary>
    /// Read the followers of a broadcaster.
    /// Required for:
    /// </summary>
    public static Scope ModeratorReadFollowers { get; } = new("moderator:read:followers");
    /// <summary>
    /// Read Guest Star details for channels where you are a Guest Star moderator.
    /// Required for:
    /// </summary>
    public static Scope ModeratorReadGuestStar { get; } = new("moderator:read:guest_star");
    /// <summary>
    /// Manage Guest Star for channels where you are a Guest Star moderator.
    /// Required for:
    /// </summary>
    public static Scope ModeratorManageGuestStar { get; } = new("moderator:manage:guest_star");
    /// <summary>
    /// Read the list of moderators in channels where you have the moderator role.
    /// Required for:
    /// </summary>
    public static Scope ModeratorReadModerators { get; } = new("moderator:read:moderators");
    /// <summary>
    /// View a broadcaster’s Shield Mode status.
    /// Required for:
    /// </summary>
    public static Scope ModeratorReadShieldMode { get; } = new("moderator:read:shield_mode");
    /// <summary>
    /// Manage a broadcaster’s Shield Mode status.
    /// Required for:
    /// </summary>
    public static Scope ModeratorManageShieldMode { get; } = new("moderator:manage:shield_mode");
    /// <summary>
    /// View a broadcaster’s shoutouts.
    /// Required for:
    /// </summary>
    public static Scope ModeratorReadShoutouts { get; } = new("moderator:read:shoutouts");
    /// <summary>
    /// Manage a broadcaster’s shoutouts.
    /// Required for:
    /// </summary>
    public static Scope ModeratorManageShoutouts { get; } = new("moderator:manage:shoutouts");
    /// <summary>
    /// Read chat messages from suspicious users and see users flagged as suspicious in channels where you have the moderator role.
    /// Required for:
    /// </summary>
    public static Scope ModeratorReadSuspiciousUsers { get; } = new("moderator:read:suspicious_users");
    /// <summary>
    /// View a broadcaster’s unban requests.
    /// Required for:
    /// </summary>
    public static Scope ModeratorReadUnbanRequests { get; } = new("moderator:read:unban_requests");
    /// <summary>
    /// Manage a broadcaster’s unban requests.
    /// Required for:
    /// </summary>
    public static Scope ModeratorManageUnbanRequests { get; } = new("moderator:manage:unban_requests");
    /// <summary>
    /// Read the list of VIPs in channels where you have the moderator role.
    /// Required for:
    /// </summary>
    public static Scope ModeratorReadVips { get; } = new("moderator:read:vips");
    /// <summary>
    /// Read warnings in channels where you have the moderator role.
    /// Required for:
    /// </summary>
    public static Scope ModeratorReadWarnings { get; } = new("moderator:read:warnings");
    /// <summary>
    /// Warn users in channels where you have the moderator role.
    /// Required for:
    /// </summary>
    public static Scope ModeratorManageWarnings { get; } = new("moderator:manage:warnings");
    /// <summary>
    /// Join a specified chat channel as your user and appear as a bot, and perform chat-related actions as your user.
    /// Required for:
    /// </summary>
    public static Scope UserBot { get; } = new("user:bot");
    /// <summary>
    /// Manage a user object.
    /// Required for:
    /// </summary>
    public static Scope UserEdit { get; } = new("user:edit");
    /// <summary>
    /// View and edit a user’s broadcasting configuration, including Extension configurations.
    /// Required for:
    /// </summary>
    public static Scope UserEditBroadcast { get; } = new("user:edit:broadcast");
    /// <summary>
    /// View the block list of a user.
    /// Required for:
    /// </summary>
    public static Scope UserReadBlockedUsers { get; } = new("user:read:blocked_users");
    /// <summary>
    /// Manage the block list of a user.
    /// Required for:
    /// </summary>
    public static Scope UserManageBlockedUsers { get; } = new("user:manage:blocked_users");
    /// <summary>
    /// View a user’s broadcasting configuration, including Extension configurations.
    /// Required for:
    /// </summary>
    public static Scope UserReadBroadcast { get; } = new("user:read:broadcast");
    /// <summary>
    /// Receive chatroom messages and informational notifications relating to a channel’s chatroom.
    /// Required for:
    /// </summary>
    public static Scope UserReadChat { get; } = new("user:read:chat");
    /// <summary>
    /// Update the color used for the user’s name in chat.
    /// Required for:
    /// </summary>
    public static Scope UserManageChatColor { get; } = new("user:manage:chat_color");
    /// <summary>
    /// View a user's email address.
    /// Required for:
    /// </summary>
    public static Scope UserReadEmail { get; } = new("user:read:email");
    /// <summary>
    /// View emotes available to a user.
    /// Required for:
    /// </summary>
    public static Scope UserReadEmotes { get; } = new("user:read:emotes");
    /// <summary>
    /// View the list of channels a user follows.
    /// Required for:
    /// </summary>
    public static Scope UserReadFollows { get; } = new("user:read:follows");
    /// <summary>
    /// Read the list of channels you have moderator privileges in.
    /// Required for:
    /// </summary>
    public static Scope UserReadModeratedChannels { get; } = new("user:read:moderated_channels");
    /// <summary>
    /// View if an authorized user is subscribed to specific channels.
    /// Required for:
    /// </summary>
    public static Scope UserReadSubscriptions { get; } = new("user:read:subscriptions");
    /// <summary>
    /// Receive whispers sent to your user.
    /// Required for:
    /// </summary>
    public static Scope UserReadWhispers { get; } = new("user:read:whispers");
    /// <summary>
    /// Receive whispers sent to your user, and send whispers on your user’s behalf.
    /// Required for:
    /// </summary>
    public static Scope UserManageWhispers { get; } = new("user:manage:whispers");
    /// <summary>
    /// Send chat messages to a chatroom.
    /// Required for:
    /// </summary>
    public static Scope UserWriteChat { get; } = new("user:write:chat");
    /// <summary>
    /// IRC Chat Scope
    /// Send chat messages to a chatroom using an IRC connection.
    /// </summary>
    public static Scope ChatEdit { get; } = new("chat:edit");
    /// <summary>
    /// IRC Chat Scope
    /// View chat messages sent in a chatroom using an IRC connection.
    /// </summary>
    public static Scope ChatRead { get; } = new("chat:read");
    /// <summary>
    /// PubSub-Specific Scope
    /// Receive whisper messages for your user using PubSub.
    /// </summary>
    public static Scope WhispersRead { get; } = new("whispers:read");

    private readonly string _scope;
    /// <summary>
    /// If possible, please use static <see cref="Scope"/> definitions provided by this class.
    /// You can use this constructor to create a <see cref="Scope"/> when a static definition is not provided.
    /// </summary>
    /// <param name="scope">The Twitch scope string (e.g. "bits:read")</param>
    public Scope(string scope) {  _scope = scope; }
    public override string ToString()
        => _scope;
}

internal static class ScopeExtensions
{
    public static string FormatScopes(this IEnumerable<Scope> scopes)
    {
        StringBuilder sb = new();
        foreach (Scope scope in scopes)
        {
            sb.Append(scope.ToString());
            sb.Append('+');
        }
        return sb.ToString().TrimEnd('+');
    }
}
