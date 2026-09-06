namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Contains a list of emotes that a user has access to.
/// </summary>
public record GetUserEmotesResponseContent
    : IPageableResponse
{
    /// <summary>
    /// A list of emotes that the user has access to.
    /// </summary>
    public required UserEmote[] Data { get; init; }
    /// <summary>
    /// A templated URL for getting an emote image.
    /// </summary>
    public required EmoteImageTemplateString Template { get; init; }
    /// <summary>
    /// Contains the information used to page through the list of results. 
    /// The <see cref="Pagination.Cursor"/> is null if there are no more pages left to page through.
    /// </summary>
    public required Pagination Pagination { get; init; }
}
