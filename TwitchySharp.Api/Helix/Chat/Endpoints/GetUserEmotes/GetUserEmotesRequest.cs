using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Retrieves emotes available to the user across all channels.
/// </summary>
/// <remarks>
/// Requires a user access token with <see cref="Scope.UserReadEmotes"/> and the access token must belong to the user you are requesting emotes for.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-user-emotes">Get User Emotes</see> for more information.
/// </remarks>
public record GetUserEmotesRequest
    : TwitchHelixRequest<GetUserEmotesResponse>, IPageableRequest
{
    protected override string Path => "/chat/emotes/user";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(UserId);
    public override IEnumerable<Scope> ValidScopes => [ Scope.UserReadEmotes ];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("user_id", UserId)
            .Add("broadcaster_id", BroadcasterId)
            .Add("after", After?.Value);

    /// <summary>
    /// The user id of the user you want to get emotes for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// Requires <see cref="Scope.UserReadEmotes"/>.
    /// </remarks>
    public required UserId UserId { get; init; }

    /// <summary>
    /// The user id of a broadcaster you wish to get follower emotes of.
    /// </summary>
    /// <remarks>
    /// Using this query parameter will guarantee inclusion of the broadcaster's follower emotes in the response body.
    /// <b>Note:</b> If the user specified in <see cref="UserId"/> is subscribed to the broadcaster specified, their follower emotes will appear in the response body regardless if this query parameter is used.
    /// </remarks>
    public UserId? BroadcasterId { get; init; }

    /// <inheritdoc/>
    public PaginationCursor? After { get; init; }

    /// <summary>
    /// Not supported by this endpoint. Present only to satisfy <see cref="IPageableRequest"/>.
    /// </summary>
    public PaginationAmount? First { get; init; }
}
