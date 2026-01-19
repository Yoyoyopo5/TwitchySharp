using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Helix.Moderation;
using TwitchySharp.Api.Helix.Channels;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Gets the list of users that are connected to the broadcaster’s chat session.
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
    : TwitchHelixRequest<GetChattersResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token with <see cref="Scope.ModeratorReadChatters"/></param>
    /// <param name="broadcasterId">The user id of the broadcaster whose chatters you want to get.</param>
    /// <param name="moderatorId">
    /// The user id of the broadcaster OR one of the broadcaster's moderators.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    /// <param name="first">
    /// The maximum number of items to return per page in the response. 
    /// The minimum page size is 1 item per page and the maximum is 1,000. 
    /// The default is 100.
    /// </param>
    /// <param name="after">
    /// The cursor used to get the next page of results. 
    /// The <see cref="Pagination"/> object in the response contains the cursor’s value.
    /// </param>
    public GetChattersRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string moderatorId,
        int? first = null,
        string? after = null
        )
        : base(
            "/chat/chatters",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("moderator_id", moderatorId)
                .Add("first", first?.ToString())
                .Add("after", after)
            )
    {
        Method = HttpMethod.Get;
    }
}
