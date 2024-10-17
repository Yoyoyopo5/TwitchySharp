using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Charity;
/// <summary>
/// Gets information about the charity campaign that a broadcaster is running. 
/// For example, the campaign’s fundraising goal and the current amount of donations.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-charity-campaign">get charity campaign</see> for more information.
/// <br/>
/// Requires a user access token with <see cref="Scope.ChannelReadCharity"/>.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelReadCharity"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster to get charity campaign data for.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
public class GetCharityCampaignRequest(string clientId, string accessToken, string broadcasterId)
    : HelixApiRequest<GetCharityCampaignResponse>(
        "/charity/campaigns" + 
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId),
        clientId,
        accessToken
        );
