namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Gets Twitch's list of chat badges, which users may use in any channel's chat room.
/// </summary>
/// <remarks>
/// For information about chat badges, see <see href="https://help.twitch.tv/s/article/twitch-chat-badges-guide">Twitch Chat Badges Guide</see>.
/// <br/>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-global-chat-badges">Get Global Chat Badges</see> for more information.
/// </remarks>
public record GetGlobalChatBadgesRequest
    : TwitchHelixRequest<GetGlobalChatBadgesResponseContent>,
    IAuthenticatedTwitchRequest<ITwitchRequestAuthenticationContext<TwitchIdentity>>
{
    protected override string Path => "/chat/badges/global";
    public override HttpMethod Method => HttpMethod.Get;
    public ITwitchRequestAuthenticationContext<TwitchIdentity> AuthenticationContext { get; init; }
        = TwitchRequestAuthenticationContext.Default;
}
