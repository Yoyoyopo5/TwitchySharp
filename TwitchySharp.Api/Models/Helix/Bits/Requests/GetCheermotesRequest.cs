using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.Bits.Enums;
using TwitchySharp.Api.Models.Helix.Bits.Models;
using TwitchySharp.Api.Models.Helix.Bits.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Bits.Requests;
/// <summary>
/// Gets a list of Cheermotes that users can use to cheer Bits in any Bits-enabled channel’s chat room. 
/// </summary>
/// <remarks>
/// Cheermotes are animated emotes that viewers can assign Bits to.
/// <br/>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-cheermotes">Get Cheermotes</see> for more information.
/// </remarks>
public record GetCheermotesRequest
    : TwitchHelixRequest<GetCheermotesResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token. Does not require any specific <see cref="Scope"/>.</param>
    /// <param name="broadcasterId">
    /// The user ID of the broadcaster whose custom Cheermotes you want to get. 
    /// Specify this if you want to include the broadcaster’s Cheermotes in the response (not all broadcasters upload Cheermotes). 
    /// If not specified, the response contains only global Cheermotes.
    /// If the broadcaster uploaded Cheermotes, the <see cref="Cheermote.Type"/> in the response is set to <see cref="CheermoteType.ChannelCustom"/>.
    /// </param>
    public GetCheermotesRequest(
        string clientId,
        string accessToken,
        string? broadcasterId = null
        )
        : base(
            "/bits/cheermotes",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
            )
    {
        Method = HttpMethod.Get;
    }
}
