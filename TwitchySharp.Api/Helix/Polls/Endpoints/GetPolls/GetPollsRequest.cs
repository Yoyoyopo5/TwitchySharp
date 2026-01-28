using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Polls;
/// <summary>
/// Gets a list of polls that the broadcaster created.
/// </summary>
/// <remarks>
/// Polls are available for 90 days after they're created.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelReadPolls"/> or <see cref="Scope.ChannelManagePolls"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-polls">Get Polls</see> for more information.
/// </remarks>
public record GetPollsRequest
    : TwitchHelixRequest<GetPollsResponse>, IPageableRequest
{
    protected override string Path => "/polls";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(BroadcasterId);
    public override IEnumerable<Scope> ValidScopes => [ Scope.ChannelReadPolls, Scope.ChannelManagePolls ];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("id", PollIds?.Select(x => x.Value))
            .Add("first", First?.ToString())
            .Add("after", After?.Value);

    /// <summary>
    /// The user id of the broadcaster (channel) to get polls for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// Filter the list of polls by poll id.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 20 ids.
    /// Specify this parameter only if you want to filter the list that the request returns.
    /// The endpoint ignores duplicate ids and those not owned by this broadcaster.
    /// </remarks>
    public IEnumerable<PollId>? PollIds { get; init; }

    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 20 items per page.
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; init; }

    /// <inheritdoc/>
    public PaginationCursor? After { get; init; }
}
