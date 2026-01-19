namespace TwitchySharp.Api.Helix.Search;
/// <summary>
/// Contains a list of found categories.
/// </summary>
public record SearchCategoriesResponse
{
    /// <summary>
    /// The list of categories.
    /// </summary>
    public required TwitchCategory[] Data { get; init; }
    /// <inheritdoc cref="Api.Pagination"/>
    public required Pagination Pagination { get; init; }
}
