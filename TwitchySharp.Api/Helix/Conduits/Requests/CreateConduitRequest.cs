using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Creates a new conduit.
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-conduits">create conduits</see> for more information.
/// </summary>
/// <remarks>
/// Requires an app access token.
/// </remarks>
/// <param name="clientId">The client id of the application. This is the application to create the conduit for.</param>
/// <param name="accessToken">An app access token.</param>
/// <param name="conduitData">Data used to construct the conduit.</param>
public class CreateConduitRequest(string clientId, string accessToken, CreateConduitRequestData conduitData)
    : HelixApiRequest<CreateConduitsResponse, CreateConduitRequestData>(
        "/eventsub/conduits",
        clientId,
        accessToken,
        conduitData
        );

/// <summary>
/// Contains data used to create a new conduit.
/// </summary>
public record CreateConduitRequestData
{
    /// <summary>
    /// The number of shards to create for this conduit.
    /// </summary>
    public required int ShardCount { get; init; }
}
