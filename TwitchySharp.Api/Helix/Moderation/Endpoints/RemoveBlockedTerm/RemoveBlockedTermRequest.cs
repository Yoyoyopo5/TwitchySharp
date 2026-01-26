using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Removes the word or phrase from the broadcaster's list of blocked terms.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageBlockedTerms"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#remove-blocked-term">Remove Blocked Term</see> for more information.
/// </remarks>
public record RemoveBlockedTermRequest
    : TwitchHelixRequest<RemoveBlockedTermResponse>
{
    protected override string Path => "/moderation/blocked_terms";
    public override HttpMethod Method => HttpMethod.Delete;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(ModeratorId);
    public override IEnumerable<Scope> ValidScopes => [Scope.ModeratorManageBlockedTerms];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId)
            .Add("id", BlockedTermId);

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
