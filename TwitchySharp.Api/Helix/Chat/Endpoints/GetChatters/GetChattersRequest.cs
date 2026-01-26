using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Helix.Moderation;
using TwitchySharp.Api.Helix.Channels;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Gets the list of users that are connected to the broadcaster's chat session.
/// </summary>
/// <remarks>
/// To determine whether a user is a moderator or VIP, use the <see cref="GetModeratorsRequest"/> and <see cref="GetVipsRequest"/> endpoints.
/// You can check the roles of up to 100 users.
/// <br/>
/// <b>NOTE:</b> There is a delay between when users join and leave a chat and when the list is updated accordingly.
/// <b>DEV NOTE:</b> The list is usually not very accurate (in real-time) for this reason.
/// Often a user will not be in this list when they are active in chat.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadChatters"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-chatters">Get Chatters</see> for more information.
/// </remarks>
public record GetChattersRequest
    : TwitchHelixRequest<GetChattersResponse>, IPageableRequest
{
    protected override string Path => "/chat/chatters";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(ModeratorId);
    public override IEnumerable<Scope> ValidScopes => [ Scope.ModeratorReadChatters ];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId)
            .Add("first", First?.ToString())
            .Add("after", After?.Value);

    /// <summary>
    /// The user id of the broadcaster whose chatters you want to get.
    /// </summary>
    public required UserId BroadcasterId { get; set; }

    /// <summary>
    /// The user id of the broadcaster OR one of the broadcaster's moderators.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// Requires <see cref="Scope.ModeratorReadChatters"/>.
    /// </remarks>
    public required UserId ModeratorId { get; set; }

    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 1,000.
    /// The default is 100.
    /// </remarks>
    public PaginationAmount? First { get; set; }

    /// <inheritdoc/>
    public PaginationCursor? After { get; set; }
}
