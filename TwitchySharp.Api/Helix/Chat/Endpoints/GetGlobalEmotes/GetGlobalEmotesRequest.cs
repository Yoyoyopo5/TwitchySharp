using System.Net.Http;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Gets the list of <see href="https://www.twitch.tv/creatorcamp/en/learn-the-basics/emotes/">Global Emotes</see>.
/// </summary>
/// <remarks>
/// Global emotes are Twitch-created emotes that users can use in any Twitch chat.
/// <br/>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference#get-global-emotes">Get Global Emotes</see> for more information.
/// </remarks>
public record GetGlobalEmotesRequest
    : TwitchHelixRequest<GetGlobalEmotesResponse>
{
    protected override string Path => "/chat/emotes/global";
    public override HttpMethod Method => HttpMethod.Get;
}
