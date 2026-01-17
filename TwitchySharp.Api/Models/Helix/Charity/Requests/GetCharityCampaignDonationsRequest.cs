using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.Charity.Response;
using TwitchySharp.Api.Models.Shared;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Charity.Requests;
/// <summary>
/// Gets the list of donations that users have made to the broadcaster’s active charity campaign.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadCharity"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-charity-campaign-donations">Get Charity Campaign Donations</see> for more information.
/// </remarks>
public record GetCharityCampaignDonationsRequest
    : TwitchHelixRequest<GetCharityCampaignDonationsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelReadCharity"/>.</param>
    /// <param name="broadcasterId">
    /// The user id of the broadcaster that you want to get charity donations for. 
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    /// <param name="first">
    /// The maximum number of items to return per page in the response. 
    /// The minimum page size is 1 item per page and the maximum is 100. 
    /// The default is 20.
    /// </param>
    /// <param name="after">
    /// The cursor used to get the next page of results. 
    /// The <see cref="Pagination"/> property in the response contains the cursor’s value.
    /// </param>
    public GetCharityCampaignDonationsRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        int? first = null,
        string? after = null
        )
        : base(
            "/charity/donations",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("first", first?.ToString())
                .Add("after", after)
            )
    {
        Method = HttpMethod.Get;
    }
}
