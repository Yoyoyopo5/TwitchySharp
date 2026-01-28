using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Gets all users allowed to moderate the broadcaster's chat room.
/// </summary>
/// <remarks>
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ModerationRead"/> or <see cref="Scope.ChannelManageModerators"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-moderators">Get Moderators</see> for more information.
/// </remarks>
public record GetModeratorsRequest
    : TwitchHelixRequest<GetModeratorsResponse>, IPageableRequest
{
    protected override string Path => "/moderation/moderators";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(BroadcasterId);
    public override IEnumerable<Scope> ValidScopes => [ Scope.ModerationRead, Scope.ChannelManageModerators ];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("user_id", UserIds?.Select(x => x.Value))
            .Add("first", First?.ToString())
            .Add("after", After?.Value);

    /// <summary>
    /// The user id of the broadcaster (channel) to get moderators for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// A list of user ids used to filter the results.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 100 ids.
    /// The returned list includes only the users from the list who are moderators in the broadcaster's channel.
    /// The list is returned in the same order as you specified the ids.
    /// </remarks>
    public IEnumerable<UserId>? UserIds { get; init; }

    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100 items per page.
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; init; }

    /// <inheritdoc/>
    public PaginationCursor? After { get; init; }
}
