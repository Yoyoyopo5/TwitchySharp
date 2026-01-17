using System.Net.Http;
using TwitchySharp.Api.Models.Helix.Conduits.Responses;

namespace TwitchySharp.Api.Models.Helix.Conduits.Requests;
/// <summary>
/// Gets the conduits for a client ID.
/// </summary>
/// <remarks>
/// Requires an app access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-conduits">Get Conduits</see> for more information.
/// </remarks>
public record GetConduitsRequest
    : TwitchHelixRequest<GetConduitsResponse>
{
    /// <param name="clientId">The client id of the application. This will be the application to get conduits for.</param>
    /// <param name="accessToken">An app access token.</param>
    public GetConduitsRequest(string clientId, string accessToken)
        : base(
            "/eventsub/conduits",
            clientId,
            accessToken
            )
    {
        Method = HttpMethod.Get;
    }
}
