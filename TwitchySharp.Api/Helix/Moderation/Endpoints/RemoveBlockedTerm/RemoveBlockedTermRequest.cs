using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

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
    /// <param name="broadcasterId">The user id of the broadcaster (channel) to remove a blocked term from.</param>
    /// <param name="moderatorId">
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    /// <param name="blockedTermId">The id of the blocked term to remove.</param>
    public RemoveBlockedTermRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string moderatorId,
        string blockedTermId
        ) : base(
            "/moderation/blocked_terms",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("moderator_id", moderatorId)
                .Add("id", blockedTermId)
            )
    {
        Method = HttpMethod.Delete;
    }
}
