using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Gets a list of channels that the specified user has moderator privileges in.
/// </summary>
/// <remarks>
/// <br/>
/// Requires a user access token that includes <see cref="Scope.UserReadModeratedChannels"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-moderated-channels">Get Moderated Channels</see> for more information.
/// </remarks>
public record GetModeratedChannelsRequest : TwitchHelixRequest<GetModeratedChannelsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.UserReadModeratedChannels"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetModeratedChannelsRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetModeratedChannelsRequestParameters parameters
        ) : base(
            "/moderation/channels",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("user_id", parameters.UserId)
                .Add("after", parameters.After?.Value)
                .Add("first", parameters.First?.ToString())
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetModeratedChannelsRequest"/>.
/// </summary>
public record GetModeratedChannelsRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// The user id of the user to get moderated channels for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </remarks>
    public required UserId UserId { get; set; }
    public PaginationCursor? After { get; set; }
    /// <remarks>
    /// Minimum page size is 1 item per page and the maximum is 100. 
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; set; }
}
