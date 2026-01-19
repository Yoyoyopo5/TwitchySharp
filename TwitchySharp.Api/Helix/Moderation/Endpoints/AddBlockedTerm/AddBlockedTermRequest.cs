using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Adds a word or phrase to the broadcaster’s list of blocked terms.
/// </summary>
/// <remarks>
/// These are the terms that the broadcaster doesn’t want used in their chat room.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageBlockedTerms"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#add-blocked-term">Add Blocked Term</see> for more information.
/// </remarks>
public record AddBlockedTermRequest : TwitchHelixRequest<AddBlockedTermResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageBlockedTerms"/>.</param>
    /// <param name="broadcasterId">The user id of the broadcaster (channel) to add a blocked term to.</param>
    /// <param name="moderatorId">
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    /// <param name="term">The blocked term to create.</param>
    public AddBlockedTermRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string moderatorId,
        AddBlockedTermRequestData term
        ) : base(
            "/moderation/blocked_terms",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("moderator_id", moderatorId)
            )
    {
        Method = HttpMethod.Post;
        ContentObject = term;
    }
}

/// <summary>
/// Data used to add a blocked term to a channel.
/// </summary>
public record AddBlockedTermRequestData
{
    /// <summary>
    /// The word or phrase to block from being used in the broadcaster’s chat room.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The term must contain a minimum of 2 characters and may contain up to a maximum of 500 characters.
    /// </para>
    /// <para>
    /// Terms may include a wildcard character (*). 
    /// The wildcard character must appear at the beginning or end of a word or set of characters. 
    /// For example, *foo or foo*.
    /// </para>
    /// <para>
    /// If the blocked term already exists, the response contains the existing blocked term.
    /// </para>
    /// </remarks>
    public required string Text { get; init; }
}
