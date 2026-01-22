using System.Net.Http;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Gets the conduits for a specific client id.
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
    public GetConduitsRequest(ClientId clientId, AppAccessToken accessToken)
        : base(
            "/eventsub/conduits",
            clientId,
            accessToken
            )
    {
        Method = HttpMethod.Get;
    }
}
