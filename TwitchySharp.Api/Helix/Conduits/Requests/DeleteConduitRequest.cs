using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Deletes a specified conduit. 
/// Note that it may take some time for Eventsub subscriptions on a deleted conduit to show as disabled when calling Get Eventsub Subscriptions.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-conduit">delete conduit</see> for more information.
/// </summary>
/// <remarks>
/// Requires an app access token.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app access token.</param>
/// <param name="conduitId">The id of the conduit you want to delete.</param>
public class DeleteConduitRequest(string clientId, string accessToken, string conduitId)
    : HelixApiRequest<DeleteConduitResponse>(
        "/eventsub/conduits" +
        new HttpQueryParameters()
            .Add("id", conduitId),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Delete;
}
