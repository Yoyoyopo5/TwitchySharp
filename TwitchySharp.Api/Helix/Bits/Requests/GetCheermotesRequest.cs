using System;
using System.Collections.Generic;
using System.Text;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Bits;
/// <summary>
/// Gets a list of Cheermotes that users can use to cheer Bits in any Bits-enabled channel’s chat room. 
/// Cheermotes are animated emotes that viewers can assign Bits to.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-cheermotes">get cheermotes</see> for more information.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app or user access token. Does not require any specific <see cref="Scope"/>.</param>
/// <param name="broadcasterId">
/// The user ID of the broadcaster whose custom Cheermotes you want to get. 
/// Specify this if you want to include the broadcaster’s Cheermotes in the response (not all broadcasters upload Cheermotes). 
/// If not specified, the response contains only global Cheermotes.
/// If the broadcaster uploaded Cheermotes, the <see cref="CheermoteData.Type"/> in the response is set to <see cref="CheermoteType.ChannelCustom"/>.
/// </param>
public class GetCheermotesRequest(string clientId, string accessToken, string? broadcasterId = null)
    : HelixApiRequest<GetCheermotesResponse>(
        "/bits/cheermotes" + (new Dictionary<string, string?>()
        {
            { "broadcaster_id", broadcasterId }
        }.ToHttpQueryString()),
        clientId,
        accessToken
        );
