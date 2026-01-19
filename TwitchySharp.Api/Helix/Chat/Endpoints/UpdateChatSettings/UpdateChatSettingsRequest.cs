using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Helpers.JsonConverters;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Updates the broadcaster’s chat settings.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageChatSettings"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-chat-settings">Update Chat Settings</see> for more information.
/// </remarks>
public record UpdateChatSettingsRequest
    : TwitchHelixRequest<UpdateChatSettingsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageChatSettings"/>.</param>
    /// <param name="broadcasterId">The user id of the broadcaster whose chat settings you want to update.</param>
    /// <param name="moderatorId">
    /// The user id of the broadcaster or a moderator of the broadcaster's channel. 
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    /// <param name="newSettings">The settings that you want to change.</param>
    public UpdateChatSettingsRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string moderatorId,
        UpdateChatSettingsRequestData newSettings
        )
        : base(
            "/chat/settings",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("moderator_id", moderatorId)
            )
    {
        Method = HttpMethod.Patch;
        ContentObject = newSettings;
    }
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
    /// The length of time that users must follow the broadcaster before being able to participate in the chat room. 
    /// Set only if <see cref="FollowerMode"/> is <see langword="true"/>. Possible values range from 0 (no restriction) to 3 months. The default is 0.
    /// </summary>
    [JsonConverter(typeof(MinutesTimeSpanJsonConverter))]
    public TimeSpan? FollowerModeDuration { get; init; }
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
    /// <item><c>2</c> — 2 second delay (recommended)</item>
    /// <item><c>4</c> — 4 second delay</item>
    /// <item><c>6</c> — 6 second delay</item>
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
    /// The amount of time, that users must wait between sending messages. Set only if <see cref="SlowMode"/> is <see langword="true"/>.
    /// Possible values range from 3 to 120 seconds. The default is 30 seconds.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public TimeSpan? SlowModeWaitTime { get; init; }
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
