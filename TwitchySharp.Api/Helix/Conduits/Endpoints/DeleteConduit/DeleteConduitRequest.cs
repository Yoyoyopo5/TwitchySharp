using System.Net.Http;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Deletes a specified conduit.
/// </summary>
/// <remarks>
/// Note that it may take some time for Eventsub subscriptions on a deleted conduit to show as disabled when calling <see cref="GetEventSubSubscriptionsRequest"/>.
/// <br/>
/// Requires an app access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-conduit">Delete Conduit</see> for more information.
/// </remarks>
public record DeleteConduitRequest
    : TwitchHelixRequest<DeleteConduitResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app access token.</param>
    /// <param name="conduitId">The id of the conduit you want to delete.</param>
    public DeleteConduitRequest(
        string clientId,
        string accessToken,
        string conduitId
        )
        : base(
            "/eventsub/conduits",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("id", conduitId)
            )
    {
        Method = HttpMethod.Delete;
    }
}
