using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Gets information about one or more channels.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-channel-information">get channel information</see> for more information.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user or app access token. No specific <see cref="Scope"/> is required.</param>
/// <param name="broadcasterId">
/// The user ID of the broadcaster(s) whose channel information you want to get. 
/// You may specify a maximum of 100 IDs. The API ignores duplicate IDs and IDs that are not found.
/// </param>
public class GetChannelInformationRequest(string clientId, string accessToken, params string[] broadcasterIds)
    : HelixApiRequest<GetChannelInformationResponse>("/channels" + new HttpQueryParameters().Add("broadcaster_id", broadcasterIds), clientId, accessToken);
