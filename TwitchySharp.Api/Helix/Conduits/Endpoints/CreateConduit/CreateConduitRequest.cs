using System.Net.Http;

namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Creates a new conduit.
/// </summary>
/// <remarks>
/// Requires an app access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-conduits">Create Conduits</see> for more information.
/// </remarks>
public record CreateConduitRequest
    : TwitchHelixRequest<CreateConduitsResponse>
{
    /// <param name="clientId">The client id of the application. This is the application to create the conduit for.</param>
    /// <param name="accessToken">An app access token.</param>
    /// <param name="conduitData">Data used to construct the conduit.</param>
    public CreateConduitRequest(
        string clientId,
        string accessToken,
        CreateConduitRequestData conduitData
        )
        : base(
            "/eventsub/conduits",
            clientId,
            accessToken
            )
    {
        Method = HttpMethod.Post;
        ContentObject = conduitData;
    }
}

/// <summary>
/// Contains data used to create a new conduit.
/// </summary>
public record CreateConduitRequestData
{
    /// <summary>
    /// The number of shards to create for this conduit.
    /// Note that new shards must be initialized via <see cref="UpdateConduitShardsRequest"/> before they will appear in a <see cref="GetConduitShardsRequest"/>.
    /// </summary>
    public required int ShardCount { get; init; }
}
