using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Models.Helix.Chat.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Chat.Requests;
/// <summary>
/// Gets emotes for one or more specified emote sets.
/// </summary>
/// <remarks>
/// An emote set groups emotes that have a similar context. 
/// For example, Twitch places all the subscriber emotes that a broadcaster uploads for their channel in the same emote set.
/// <br/>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-emote-sets">Get Emote Sets</see> for more information.
/// </remarks>
public record GetEmoteSetsRequest
    : TwitchHelixRequest<GetEmoteSetsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token.</param>
    /// <param name="emoteSetIds">
    /// A list of IDs for the emote sets to get. 
    /// You may specify a maximum of 25 IDs. 
    /// The response contains only the IDs that were found and ignores duplicate IDs.
    /// </param>
    public GetEmoteSetsRequest(
        string clientId,
        string accessToken,
        IEnumerable<string> emoteSetIds
        )
        : base(
            "/chat/emotes/set",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("emote_set_id", emoteSetIds)
            )
    {
        Method = HttpMethod.Get;
    }
}
