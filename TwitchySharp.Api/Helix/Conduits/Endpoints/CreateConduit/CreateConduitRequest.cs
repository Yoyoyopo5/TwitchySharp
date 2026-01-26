using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.Models;

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
    protected override string Path => "/eventsub/conduits";
    public override HttpMethod Method => HttpMethod.Post;
    protected override TwitchApiIdentity DefaultIdentity => TwitchApiIdentity.Default;
    public override IEnumerable<Scope> ValidScopes => [];
    public override object? ContentObject => ConduitData;

    /// <summary>
    /// Data used to construct the conduit.
    /// </summary>
    public required CreateConduitRequestData ConduitData { get; set; }
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
