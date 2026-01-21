using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Charity;
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
    /// <param name="parameters">The request parameters.</param>
    public GetCharityCampaignRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetCharityCampaignRequestParameters parameters
        )
        : base(
            "/charity/campaigns",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetCharityCampaignRequest"/>.
/// </summary>
public record GetCharityCampaignRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster to get charity campaign data for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; set; }
}
