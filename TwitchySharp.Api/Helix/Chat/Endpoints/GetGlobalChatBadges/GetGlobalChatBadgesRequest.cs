using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.Models;

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
    : TwitchHelixRequest<GetGlobalChatBadgesResponse>
{
    protected override string Path => "/chat/badges/global";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchApiIdentity DefaultIdentity => TwitchApiIdentity.Default;
    public override IEnumerable<Scope> ValidScopes => [];
}
