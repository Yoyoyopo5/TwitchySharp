using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

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
    /// <param name="parameters">The request parameters.</param>
    public DeleteConduitRequest(
        ClientId clientId,
        AppAccessToken accessToken,
        DeleteConduitRequestParameters parameters
        )
        : base(
            "/eventsub/conduits",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("id", parameters.ConduitId)
            )
    {
        Method = HttpMethod.Delete;
    }
}

/// <summary>
/// Request parameters for a <see cref="DeleteConduitRequest"/>.
/// </summary>
public record DeleteConduitRequestParameters
{
    /// <summary>
    /// The id of the conduit you want to delete.
    /// </summary>
    public required ConduitId ConduitId { get; set; }
}
