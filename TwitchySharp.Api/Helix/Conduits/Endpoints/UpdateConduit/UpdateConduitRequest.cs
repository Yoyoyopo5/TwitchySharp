using System.Net.Http;

namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Updates a conduit’s shard count. 
/// </summary>
/// <remarks>
/// <para>
/// To delete shards, update the count to a lower number, and the shards above the count will be deleted.
/// For example, if the existing shard count is 100, by resetting shard count to 50, shards 50-99 are disabled.
/// </para>
/// Requires an app access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-conduits">Update Conduits</see> for more information.
/// </remarks>
public record UpdateConduitRequest
    : TwitchHelixRequest<UpdateConduitResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app access token.</param>
    /// <param name="conduitToUpdate">The data to update the conduit with.</param>
    public UpdateConduitRequest(
        string clientId,
        string accessToken,
        UpdateConduitRequestData conduitToUpdate
        )
        : base(
            "/eventsub/conduits",
            clientId,
            accessToken
            )
    {
        Method = HttpMethod.Patch;
        ContentObject = conduitToUpdate;
    }
}

/// <summary>
/// Contains data used to update an existing conduit.
/// </summary>
public record UpdateConduitRequestData
{
    /// <summary>
    /// The conduit id of the conduit you want to update.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The new number of shards to assign to this conduit.
    /// </summary>
    public required int ShardCount { get; init; }
}
