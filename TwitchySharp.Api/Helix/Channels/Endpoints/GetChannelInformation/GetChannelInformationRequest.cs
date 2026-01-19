using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

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
    /// <param name="broadcasterIds">
    /// The user ID of the broadcaster(s) whose channel information you want to get. 
    /// You may specify a maximum of 100 IDs. The API ignores duplicate IDs and IDs that are not found.
    /// </param>
    public GetChannelInformationRequest(
        string clientId,
        string accessToken,
        IEnumerable<string> broadcasterIds
        )
        : base(
            "/channels",
            clientId,
            accessToken,
            new HttpQueryParameters()
              .Add("broadcaster_id", broadcasterIds)
            )
    {
        Method = HttpMethod.Get;
    }
}
