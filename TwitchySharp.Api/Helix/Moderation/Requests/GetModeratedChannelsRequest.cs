using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Gets a list of channels that the specified user has moderator privileges in.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-moderated-channels">get moderated channels</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserReadModeratedChannels"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.UserReadModeratedChannels"/>.</param>
/// <param name="userId">
/// The user id of the user to get moderated channels for.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="Pagination"/> property in the response contains the cursor’s value.
/// </param>
/// <param name="first">
/// The maximum number of items to return per page in the response.
/// Minimum page size is 1 item per page and the maximum is 100. 
/// The default is 20.
/// </param>
public class GetModeratedChannelsRequest(
    string clientId,
    string accessToken,
    string userId,
    string? after = null,
    int? first = null
    )
    : HelixApiRequest<GetModeratedChannelsResponse>(
        "/moderation/channels" +
        new HttpQueryParameters()
            .Add("user_id", userId)
            .Add("after", after)
            .Add("first", first?.ToString()),
        clientId,
        accessToken
        );
