using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Gets the conduits for a client ID.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-conduits">get conduits</see> for more information.
/// </summary>
/// <param name="clientId">The client id of the application. This will be the application to get conduits for.</param>
/// <param name="accessToken">An app access token.</param>
public class GetConduitsRequest(string clientId, string accessToken)
    : HelixApiRequest<GetConduitsResponse>(
        "/eventsub/conduits",
        clientId,
        accessToken
        );
