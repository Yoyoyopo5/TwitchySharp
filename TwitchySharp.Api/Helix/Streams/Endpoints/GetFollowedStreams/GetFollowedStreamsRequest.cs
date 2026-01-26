using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Streams;
/// <summary>
/// Gets the list of broadcasters that the user follows and that are streaming live.
/// </summary>
/// <remarks>
/// <br/>
/// Requires a user access token that includes <see cref="Scope.UserReadFollows"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-followed-streams">Get Followed Streams</see> for more information.
/// </remarks>
public record GetFollowedStreamsRequest
    : TwitchHelixRequest<GetFollowedStreamsResponse>, IPageableRequest
{
    protected override string Path => "/streams/followed";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(UserId);
    public override IEnumerable<Scope> ValidScopes => [Scope.UserReadFollows];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("user_id", UserId)
            .Add("first", First?.ToString())
            .Add("after", After?.Value);

    /// <summary>
    /// The id of the user to get followed streams for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId UserId { get; set; }
    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100 items per page.
    /// The default is 100.
    /// </remarks>
    public PaginationAmount? First { get; set; }
    /// <inheritdoc/>
    public PaginationCursor? After { get; set; }
}
