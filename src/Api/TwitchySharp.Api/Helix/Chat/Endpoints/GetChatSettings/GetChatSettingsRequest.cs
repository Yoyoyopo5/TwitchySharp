using System.Net.Http;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Gets the broadcaster's chat settings.
/// </summary>
/// <remarks>
/// For an overview of chat settings, see <see href="https://help.twitch.tv/s/article/chat-commands#AllMods">Chat Commands for Broadcasters and Moderators</see> and <see href="https://help.twitch.tv/s/article/setting-up-moderation-for-your-twitch-channel#modpreferences">Moderator Preferences</see>.
/// <br/>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-chat-settings">Get Chat Settings</see> for more information.
/// </remarks>
public record GetChatSettingsRequest
    : TwitchHelixRequest<GetChatSettingsResponse>
{
    protected override string Path => "/chat/settings";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId);

    /// <summary>
    /// The user id of the broadcaster whose chat settings you want to get.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The user id of the broadcaster or one of the broadcaster's moderators.
    /// </summary>
    /// <remarks>
    /// This parameter is only required if you want to include the <see cref="ChatSettings.NonModeratorChatDelay"/> and <see cref="ChatSettings.NonModeratorChatDelayDuration"/> in the response.
    /// If specified, this must be the same user that created the access token used in the request.
    /// </remarks>
    public UserId? ModeratorId { get; init; }
}
