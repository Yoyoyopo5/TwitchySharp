using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Updates the broadcaster’s chat settings.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-chat-settings">update chat settings</see> for more information.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageChatSettings"/>.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageChatSettings"/>.</param>
/// <param name="broadcasterId">The user id of the broadcaster whose chat settings you want to update.</param>
/// <param name="moderatorId">The user id of the broadcaster or a moderator of the broadcaster's channel. This must be the same user that created the <paramref name="accessToken"/>.</param>
/// <param name="newSettings">The settings that you want to change.</param>
public class UpdateChatSettingsRequest(
    string clientId, 
    string accessToken, 
    string broadcasterId, 
    string moderatorId, 
    UpdateChatSettingsRequestData newSettings
    )
    : HelixApiRequest<UpdateChatSettingsResponse, UpdateChatSettingsRequestData>(
        "/chat/settings" + 
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId),
        clientId,
        accessToken,
        newSettings
        )
{
    public override HttpMethod Method => HttpMethod.Patch;
}

/// <summary>
/// Contains data used to update a broadcaster's chat settings.
/// All fields are optional. Specify only those fields that you want to update.
/// </summary>
public record UpdateChatSettingsRequestData
{
    /// <summary>
    /// Determines whether chat messages must contain only emotes.
    /// Set to <see langword="true"/> if only emotes are allowed; otherwise, <see langword="false"/>. The default is <see langword="false"/>.
    /// </summary>
    public bool? EmoteMode { get; init; }
    /// <summary>
    /// Determines whether the broadcaster restricts the chat room to followers only.
    /// Set to <see langword="true"/> if the broadcaster restricts the chat room to followers only; otherwise, <see langword="false"/>. The default is <see langword="true"/>.
    /// To specify how long users must follow the broadcaster before being able to participate in the chat room, see the <see cref="FollowerModeDuration"/> property.
    /// If you don't specify the <see cref="FollowerModeDuration"/> property, it is set to the default of 0.
    /// </summary>
    public bool? FollowerMode { get; init; }
    /// <summary>
    /// The length of time, in <b>minutes</b>, that users must follow the broadcaster before being able to participate in the chat room. 
    /// Set only if <see cref="FollowerMode"/> is <see langword="true"/>. Possible values are: 0 (no restriction) through 129600 (3 months). The default is 0.
    /// </summary>
    public int? FollowerModeDuration { get; init; }
    /// <summary>
    /// Determines whether the broadcaster adds a short delay before chat messages appear in the chat room. 
    /// This gives chat moderators and bots a chance to remove them before viewers can see the message.
    /// Set to <see langword="true"/> if the broadcaster applies a delay; otherwise, <see langword="false"/>. The default is <see langword="false"/>.
    /// To specify the length of the delay, see the <see cref="NonModeratorChatDelayDuration"/> property.
    /// </summary>
    public bool? NonModeratorChatDelay { get; init; }
    /// <summary>
    /// The amount of time, in seconds, that messages are delayed before appearing in chat. 
    /// Set only if <see cref="NonModeratorChatDelay"/> is <see langword="true"/>. Possible values are:
    /// <list type="bullet">
    /// <item>2 — 2 second delay (recommended)</item>
    /// <item>4 — 4 second delay</item>
    /// <item>6 — 6 second delay</item>
    /// </list>
    /// </summary>
    public int? NonModeratorChatDelayDuration { get; init; }
    /// <summary>
    /// Determines whether the broadcaster limits how often users in the chat room are allowed to send messages. 
    /// Set to <see langword="true"/> if the broadcaster applies a wait period between messages; otherwise, <see langword="false"/>. The default is <see langword="false"/>.
    /// To specify the delay, see the <see cref="SlowModeWaitTime"/> property.
    /// </summary>
    public bool? SlowMode { get; init; }
    /// <summary>
    /// The amount of time, in <b>seconds</b>, that users must wait between sending messages. Set only if <see cref="SlowMode"/> is <see langword="true"/>.
    /// Possible values are: 3 (3 second delay) through 120 (2 minute delay). The default is 30 seconds.
    /// </summary>
    public int? SlowModeWaitTime { get; init; }
    /// <summary>
    /// Determines whether only users that subscribe to the broadcaster’s channel may talk in the chat room.
    /// Set to <see langword="true"/> if the broadcaster restricts the chat room to subscribers only; otherwise, <see langword="false"/>. The default is <see langword="false"/>.
    /// </summary>
    public bool? SubscriberMode { get; init; }
    /// <summary>
    /// Determines whether the broadcaster requires users to post only unique messages in the chat room.
    /// Set to <see langword="true"/> if the broadcaster allows only unique messages; otherwise, <see langword="false"/>. The default is <see langword="false"/>.
    /// </summary>
    public bool? UniqueChatMode { get; init; }
}
