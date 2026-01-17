using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.Moderation.Responses;
using TwitchySharp.Api.Models.Shared;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Moderation.Requests;
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
    /// <param name="broadcasterId">The user id of the broadcaster (channel) to get blocked terms for.</param>
    /// <param name="moderatorId">
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    /// <param name="first">
    /// The maximum number of items to return per page in the response. 
    /// The minimum page size is 1 item per page and the maximum is 100 items per page. 
    /// The default is 20.
    /// </param>
    /// <param name="after">
    /// The cursor used to get the next page of results. 
    /// The <see cref="Pagination"/> property in the response contains the cursor’s value.
    /// </param>
    public GetBlockedTermsRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string moderatorId,
        int? first = null,
        string? after = null
        ) : base(
            "/moderation/blocked_terms",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("moderator_id", moderatorId)
                .Add("first", first?.ToString())
                .Add("after", after)
            )
    {
        Method = HttpMethod.Get;
    }
}
