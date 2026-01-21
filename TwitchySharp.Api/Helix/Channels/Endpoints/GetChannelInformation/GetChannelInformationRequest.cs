using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Gets information about one or more channels.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-channel-information">Get Channel Information</see> for more information.
/// </remarks>
public record GetChannelInformationRequest
    : TwitchHelixRequest<GetChannelInformationResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user or app access token. No specific <see cref="Scope"/> is required.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetChannelInformationRequest(
        ClientId clientId,
        AccessToken accessToken,
        GetChannelInformationRequestParameters parameters
        )
        : base(
            "/channels",
            clientId,
            accessToken,
            new HttpQueryParameters()
              .Add("broadcaster_id", parameters.BroadcasterIds.Select(x => x.ToString()))
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetChannelInformationRequest"/>.
/// </summary>
public record GetChannelInformationRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster(s) whose channel information you want to get. 
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 100 ids. The API ignores duplicate IDs and IDs that are not found.
    /// </remarks>
    public required IEnumerable<UserId> BroadcasterIds { get; set; }
}
