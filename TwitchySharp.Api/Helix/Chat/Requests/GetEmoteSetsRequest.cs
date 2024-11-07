using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Chat.Requests;
/// <summary>
/// Gets emotes for one or more specified emote sets.
/// An emote set groups emotes that have a similar context. 
/// For example, Twitch places all the subscriber emotes that a broadcaster uploads for their channel in the same emote set.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-emote-sets">get emote sets</see> for more information.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app or user access token.</param>
/// <param name="emoteSetIds">
/// A list of IDs for the emote sets to get. 
/// You may specify a maximum of 25 IDs. 
/// The response contains only the IDs that were found and ignores duplicate IDs.
/// </param>
public class GetEmoteSetsRequest(string clientId, string accessToken, IEnumerable<string> emoteSetIds)
    : HelixApiRequest<GetEmoteSetsResponse>(
        "/chat/emotes/set" +
        new HttpQueryParameters()
            .Add("emote_set_id", emoteSetIds),
        clientId,
        accessToken
        );
