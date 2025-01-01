using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Adds a word or phrase to the broadcaster’s list of blocked terms. 
/// These are the terms that the broadcaster doesn’t want used in their chat room.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#add-blocked-term">add blocked term</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageBlockedTerms"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageBlockedTerms"/>.</param>
/// <param name="broadcasterId">The user id of the broadcaster (channel) to add a blocked term to.</param>
/// <param name="moderatorId">
/// The user id of the broadcaster or a moderator of the broadcaster's channel.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="term">The blocked term to create.</param>
public class AddBlockedTermRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    string moderatorId,
    AddBlockedTermRequestData term
    )
    : HelixApiRequest<AddBlockedTermResponse, AddBlockedTermRequestData>(
        "/moderation/blocked_terms" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId),
        clientId,
        accessToken,
        term
        );

/// <summary>
/// Data used to add a blocked term to a channel.
/// </summary>
public record AddBlockedTermRequestData
{
    /// <summary>
    /// <para>
    /// The word or phrase to block from being used in the broadcaster’s chat room. 
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
    /// </summary>
    public required string Text { get; init; }
}
