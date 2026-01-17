using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.Charity.Response;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Charity.Requests;
/// <summary>
/// Gets information about the charity campaign that a broadcaster is running. 
/// </summary>
/// <remarks>
/// For example, the campaign’s fundraising goal and the current amount of donations.
/// <br/>
/// Requires a user access token with <see cref="Scope.ChannelReadCharity"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-charity-campaign">get charity campaign</see> for more information.
/// </remarks>
public record GetCharityCampaignRequest
    : TwitchHelixRequest<GetCharityCampaignResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelReadCharity"/>.</param>
    /// <param name="broadcasterId">
    /// The user id of the broadcaster to get charity campaign data for.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    public GetCharityCampaignRequest(
        string clientId,
        string accessToken,
        string broadcasterId
        )
        : base(
            "/charity/campaigns",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
            )
    {
        Method = HttpMethod.Get;
    }
}
