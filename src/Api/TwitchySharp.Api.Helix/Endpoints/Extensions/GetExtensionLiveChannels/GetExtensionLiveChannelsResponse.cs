namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Contains a list of currently live broadcasters that are using a specific extension.
/// </summary>
public record GetExtensionLiveChannelsResponse
    : IPageableResponse
{
    /// <summary>
    /// A list of broadcasters that are streaming live and have installed or activated the extension.
    /// </summary>
    public required ExtensionLiveChannel[] Data { get; init; }
    /// <summary>
    /// The cursor used to get the next page of results. Use the <see cref="Pagination.Cursor"/> property to set the request’s after parameter.
    /// </summary>
    public required Pagination Pagination { get; init; }
}
