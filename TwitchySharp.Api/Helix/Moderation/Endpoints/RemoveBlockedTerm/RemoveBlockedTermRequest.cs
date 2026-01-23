using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Removes the word or phrase from the broadcaster’s list of blocked terms.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageBlockedTerms"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#remove-blocked-term">Remove Blocked Term</see> for more information.
/// </remarks>
public record RemoveBlockedTermRequest
    : TwitchHelixRequest<RemoveBlockedTermResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageBlockedTerms"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public RemoveBlockedTermRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        RemoveBlockedTermRequestParameters parameters
        ) : base(
            "/moderation/blocked_terms",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("moderator_id", parameters.ModeratorId)
                .Add("id", parameters.BlockedTermId)
            )
    {
        Method = HttpMethod.Delete;
    }
}

/// <summary>
/// Request parameters for a <see cref="RemoveBlockedTermRequest"/>.
/// </summary>
public record RemoveBlockedTermRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster (channel) to remove a blocked term from.
    /// </summary>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId ModeratorId { get; set; }
    /// <summary>
    /// The id of the blocked term to remove.
    /// </summary>
    public required AutomodBlockedTermId BlockedTermId { get; set; }
}
