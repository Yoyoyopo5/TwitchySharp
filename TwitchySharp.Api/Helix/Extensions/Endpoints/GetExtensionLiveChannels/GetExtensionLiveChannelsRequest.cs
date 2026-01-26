using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
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
    : TwitchHelixRequest<GetExtensionLiveChannelsResponse>, IPageableRequest
{
    protected override string Path => "/extensions/live";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchApiIdentity DefaultIdentity => TwitchApiIdentity.Default;
    public override IEnumerable<Scope> ValidScopes => [];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("extension_id", ExtensionId)
            .Add("first", First?.ToString())
            .Add("after", After?.ToString());

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

    /// <inheritdoc/>
    public PaginationCursor? After { get; set; }
}
