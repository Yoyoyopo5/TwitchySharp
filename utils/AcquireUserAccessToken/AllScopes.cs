using TwitchySharp.Api;

namespace AcquireUserAccessToken;

internal static class AllScopes
{
    /// <summary>
    /// Set of all scopes.
    /// </summary>
    /// <remarks>
    /// Avoid using this outside of testing environments. 
    /// Use only the scopes that you need for your application.
    /// Doing otherwise may break Twitch Developer rules.
    /// </remarks>
    public static IReadOnlySet<Scope> All { get; } = new HashSet<Scope>()
    {
        Scope.OpenId,
        Scope.AnalyticsReadExtensions,
        Scope.AnalyticsReadGames,
        Scope.BitsRead,
        Scope.ChannelBot,
        Scope.ChannelModerate,
        Scope.ChannelManageAds,
        Scope.ChannelReadAds,
        Scope.ChannelManageBroadcast,
        Scope.ChannelReadCharity,
        Scope.ChannelManageClips,
        Scope.ChannelEditCommercial,
        Scope.ChannelReadEditors,
        Scope.ChannelManageExtensions,
        Scope.ChannelReadGoals,
        Scope.ChannelReadGuestStar,
        Scope.ChannelManageGuestStar,
        Scope.ChannelReadHypeTrain,
        Scope.ChannelManageModerators,
        Scope.ChannelReadPolls,
        Scope.ChannelManagePolls,
        Scope.ChannelReadPredictions,
        Scope.ChannelManagePredictions,
        Scope.ChannelManageRaids,
        Scope.ChannelReadRedemptions,
        Scope.ChannelManageRedemptions,
        Scope.ChannelManageSchedule,
        Scope.ChannelReadStreamKey,
        Scope.ChannelReadSubscriptions,
        Scope.ChannelManageVideos,
        Scope.ChannelReadVips,
        Scope.ChannelManageVips,
        Scope.ClipsEdit,
        Scope.EditorManageClips,
        Scope.ModerationRead,
        Scope.ModeratorManageAnnouncements,
        Scope.ModeratorManageAutomod,
        Scope.ModeratorReadAutomodSettings,
        Scope.ModeratorManageAutomodSettings,
        Scope.ModeratorReadBannedUsers,
        Scope.ModeratorManageBannedUsers,
        Scope.ModeratorReadBlockedTerms,
        Scope.ModeratorReadChatMessages,
        Scope.ModeratorManageBlockedTerms,
        Scope.ModeratorManageChatMessages,
        Scope.ModeratorReadChatSettings,
        Scope.ModeratorManageChatSettings,
        Scope.ModeratorReadChatters,
        Scope.ModeratorReadFollowers,
        Scope.ModeratorReadGuestStar,
        Scope.ModeratorManageGuestStar,
        Scope.ModeratorReadModerators,
        Scope.ModeratorReadShieldMode,
        Scope.ModeratorManageShieldMode,
        Scope.ModeratorReadShoutouts,
        Scope.ModeratorManageShoutouts,
        Scope.ModeratorReadSuspiciousUsers,
        Scope.ModeratorManageSuspiciousUsers,
        Scope.ModeratorReadUnbanRequests,
        Scope.ModeratorManageUnbanRequests,
        Scope.ModeratorReadVips,
        Scope.ModeratorReadWarnings,
        Scope.ModeratorManageWarnings,
        Scope.UserBot,
        Scope.UserEdit,
        Scope.UserEditBroadcast,
        Scope.UserReadBlockedUsers,
        Scope.UserManageBlockedUsers,
        Scope.UserReadBroadcast,
        Scope.UserReadChat,
        Scope.UserManageChatColor,
        Scope.UserReadEmail,
        Scope.UserReadEmotes,
        Scope.UserReadFollows,
        Scope.UserReadModeratedChannels,
        Scope.UserReadSubscriptions,
        Scope.UserReadWhispers,
        Scope.UserManageWhispers,
        Scope.UserWriteChat,
        Scope.ChatEdit,
        Scope.ChatRead
    };
}
