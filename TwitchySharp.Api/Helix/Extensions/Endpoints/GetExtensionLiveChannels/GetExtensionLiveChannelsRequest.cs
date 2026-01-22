using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Gets a list of broadcasters that are streaming live and have installed or activated the extension.
/// </summary>
/// <remarks>
/// It may take a few minutes for the list to include or remove broadcasters that have recently gone live or stopped broadcasting.
/// <br/>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-extension-live-channels">Get Extension Live Channels</see> for more information.
/// </remarks>
public record GetExtensionLiveChannelsRequest
    : TwitchHelixRequest<GetExtensionLiveChannelsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetExtensionLiveChannelsRequest(
        ClientId clientId,
        AccessToken accessToken,
        GetExtensionLiveChannelsRequestParameters parameters
        ) : base(
            "/extensions/live",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("extension_id", parameters.ExtensionId)
                .Add("first", parameters.First?.ToString())
                .Add("after", parameters.After?.Value)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetExtensionLiveChannelsRequest"/>.
/// </summary>
public record GetExtensionLiveChannelsRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// The id of the extension to get.
    /// </summary>
    /// <remarks>
    /// The response will contain the list of broadcasters that are live and that have installed or activated this extension.
    /// </remarks>
    public required ExtensionId ExtensionId { get; set; }
    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100 items per page. 
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; set; }
    public PaginationCursor? After { get; set; }
}
