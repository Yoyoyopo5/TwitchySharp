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
    : TwitchHelixRequest<CreateConduitsResponseContent>,
    IAuthenticatedTwitchRequest<TwitchRequestAuthenticationContext<TwitchIdentity.Client>>
{
    protected override string Path => "/eventsub/conduits";
    public override HttpMethod Method => HttpMethod.Post;
    public TwitchRequestAuthenticationContext<TwitchIdentity.Client> AuthenticationContext { get; init; }
        = TwitchRequestAuthenticationContext.Default;
    public override object? ContentObject => ConduitData;

    /// <summary>
    /// Data used to construct the conduit.
    /// </summary>
    public required CreateConduitRequestData ConduitData { get; init; }
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
