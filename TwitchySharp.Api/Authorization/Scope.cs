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
    public static Scope ReadExtensionsAnalytics { get; } = new("analytics:read:extensions");
    /// <summary>
    /// View analytics data for the games owned by the authenticated account.
    /// Required for: 
    /// </summary>
    public static Scope ReadGamesAnalytics { get; } = new("analytics:read:games");
    /// <summary>
    /// View Bits information for a channel.
    /// Required for: 
    /// </summary>
    public static Scope ReadBits { get; } = new("bits:read");
    /// <summary>
    /// Joins your channel’s chatroom as a bot user, and perform chat-related actions as that user.
    /// Required for:
    /// </summary>
    public static Scope ChannelBot { get; } = new("channel:bot");
    /// <summary>
    /// Manage ads schedule on a channel.
    /// Required for: 
    /// </summary>
    public static Scope ManageChannelAds { get; } = new("channel:manage:ads");
    /// <summary>
    /// Read the ads schedule and details on a channel.
    /// Required for:
    /// </summary>
    public static Scope ReadChannelAds { get; } = new("channel:read:ads");
    /// <summary>
    /// Manage a channel’s broadcast configuration, including updating channel configuration and managing stream markers and stream tags.
    /// Required for:
    /// </summary>
    public static Scope ManageChannelBroadcast { get; } = new("channel:manage:broadcast");
    /// <summary>
    /// Read charity campaign details and user donations on your channel.
    /// Required for:
    /// </summary>
    public static Scope ReadChannelCharity { get; } = new("channel:read:charity");
    /// <summary>
    /// Run commercials on a channel.
    /// Required for: 
    /// </summary>
    public static Scope EditChannelCommercial { get; } = new("channel:edit:commercial");
    /// <summary>
    /// View a list of users with the editor role for a channel.
    /// Required for:
    /// </summary>
    public static Scope ReadChannelEditors { get; } = new("channel:read:editors");
    /// <summary>
    /// Manage a channel’s Extension configuration, including activating Extensions.
    /// Required for:
    /// </summary>
    public static Scope ManageChannelExtensions { get; } = new("channel:manage:extensions");
    /// <summary>
    /// View Creator Goals for a channel.
    /// Required for:
    /// </summary>
    public static Scope ReadChannelGoals { get; } = new("channel:read:goals");
    /// <summary>
    /// Read Guest Star details for your channel.
    /// Required for:
    /// </summary>
    public static Scope ReadChannelGuestStar { get; } = new("channel:read:guest_star");
    /// <summary>
    /// Manage Guest Star for your channel.
    /// Required for:
    /// </summary>
    public static Scope ManageChannelGuestStar { get; } = new("channel:manage:guest_star");
    /// <summary>
    /// View Hype Train information for a channel.
    /// Required for:
    /// </summary>
    public static Scope ReadChannelHypeTrain { get; } = new("channel:read:hype_train");
    /// <summary>
    /// Add or remove the moderator role from users in your channel.
    /// Required for:
    /// </summary>
    public static Scope ManageChannelModerators { get; } = new("channel:manage:moderators");
    /// <summary>
    /// View a channel’s polls.
    /// Required for:
    /// </summary>
    public static Scope ReadChannelPolls { get; } = new("channel:read:polls");
    /// <summary>
    /// Manage a channel’s polls.
    /// Required for:
    /// </summary>
    public static Scope ManageChannelPolls { get; } = new("channel:manage:polls");
    /// <summary>
    /// View a channel’s Channel Points Predictions.
    /// Required for:
    /// </summary>
    public static Scope ReadChannelPredictions { get; } = new("channel:read:predictions");
    /// <summary>
    /// Read the email and email verification status for a user.
    /// Required for: 
    /// </summary>
    public static Scope ReadUserEmail { get; } = new("user:read:email");
    private readonly string _scope;
    private Scope(string scope) {  _scope = scope; }
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
