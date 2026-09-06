namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Updates shard(s) for a conduit.
/// </summary>
/// <remarks>
/// <para>
/// Requires an app access token.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-conduit-shards">Update Conduit Shards</see> for more information.
/// </remarks>
public record UpdateConduitShardsRequest
    : TwitchHelixRequest<UpdateConduitShardsResponseContent>,
    IAuthenticatedTwitchRequest<TwitchRequestAuthenticationContext<TwitchIdentity.Client>>
{
    protected override string Path => "/eventsub/conduits/shards";
    public override HttpMethod Method => HttpMethod.Patch;
    public TwitchRequestAuthenticationContext<TwitchIdentity.Client> AuthenticationContext { get; init; }
        = TwitchRequestAuthenticationContext.Default;
    public override object? ContentObject => ShardUpdates;

    /// <summary>
    /// Data used to update the shards.
    /// </summary>
    public required UpdateConduitShardsRequestData ShardUpdates { get; init; }
}

/// <summary>
/// Contains data used to update shards on a specific conduit.
/// </summary>
public record UpdateConduitShardsRequestData
{
    /// <summary>
    /// The id of the conduit to update shards on.
    /// </summary>
    public required ConduitId ConduitId { get; init; }
    /// <summary>
    /// The shards to update.
    /// </summary>
    public required ConduitShardUpdate[] Shards { get; init; }
}
