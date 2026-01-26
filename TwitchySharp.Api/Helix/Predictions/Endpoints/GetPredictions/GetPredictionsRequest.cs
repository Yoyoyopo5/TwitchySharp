using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Predictions;
/// <summary>
/// Gets a list of Channel Points Predictions that the broadcaster created.
/// </summary>
/// <remarks>
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelReadPredictions"/> or <see cref="Scope.ChannelManagePredictions"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-predictions">Get Predictions</see> for more information.
/// </remarks>
public record GetPredictionsRequest
    : TwitchHelixRequest<GetPredictionsResponse>, IPageableRequest
{
    protected override string Path => "/predictions";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(BroadcasterId);
    public override IEnumerable<Scope> ValidScopes => [ Scope.ChannelReadPredictions, Scope.ChannelManagePredictions ];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("id", PredictionIds?.Select(x => x.Value))
            .Add("first", First?.ToString())
            .Add("after", After?.Value);

    /// <summary>
    /// The user id of the broadcaster (channel) to get predictions for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; set; }

    /// <summary>
    /// Filter the returned list by prediction id.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 25 ids.
    /// The endpoint ignores duplicate ids and those not owned by the broadcaster.
    /// </remarks>
    public IEnumerable<PredictionId>? PredictionIds { get; set; }

    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>.
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 25 items per page.
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; set; }

    /// <inheritdoc/>
    public PaginationCursor? After { get; set; }
}
