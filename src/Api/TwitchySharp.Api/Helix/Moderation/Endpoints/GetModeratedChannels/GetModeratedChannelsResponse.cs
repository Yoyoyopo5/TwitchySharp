namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains a list of moderated channels.
/// </summary>
public record GetModeratedChannelsResponse
    : IPageableResponse
{
    /// <summary>
    /// A list of channels that the user has moderator status in.
    /// </summary>
    public required ModeratedChannel[] Data { get; init; }
    /// <inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}
