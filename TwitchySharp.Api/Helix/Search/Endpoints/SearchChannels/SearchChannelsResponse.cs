namespace TwitchySharp.Api.Helix.Search;
/// <summary>
/// Contains a list of found channels.
/// </summary>
public record SearchChannelsResponse
{
    /// <summary>
    /// The list of channels.
    /// </summary>
    public required TwitchChannel[] Data { get; init; }
    /// <inheritdoc cref="Api.Pagination"/>
    public required Pagination Pagination { get; init; }
}
