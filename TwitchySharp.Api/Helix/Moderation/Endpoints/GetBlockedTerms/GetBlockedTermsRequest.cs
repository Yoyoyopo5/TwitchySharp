using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Gets the broadcaster’s list of non-private, blocked words or phrases.
/// </summary>
/// <remarks>
/// These are the terms that the broadcaster or moderator added manually or that were denied by AutoMod.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadBlockedTerms"/> or <see cref="Scope.ModeratorManageBlockedTerms"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-blocked-terms">Get Blocked Terms</see> for more information.
/// </remarks>
public record GetBlockedTermsRequest
    : TwitchHelixRequest<GetBlockedTermsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorReadBlockedTerms"/> or <see cref="Scope.ModeratorManageBlockedTerms"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetBlockedTermsRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetBlockedTermsRequestParameters parameters
        ) : base(
            "/moderation/blocked_terms",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("moderator_id", parameters.ModeratorId)
                .Add("first", parameters.First?.ToString())
                .Add("after", parameters.After?.Value)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetBlockedTermsRequest"/>.
/// </summary>
public record GetBlockedTermsRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get blocked terms for.
    /// </summary>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId ModeratorId { get; set; }
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100 items per page. 
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; set; }
    public PaginationCursor? After { get; set; }
}
