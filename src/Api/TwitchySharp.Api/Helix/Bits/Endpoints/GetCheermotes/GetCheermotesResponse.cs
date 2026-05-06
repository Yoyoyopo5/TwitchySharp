namespace TwitchySharp.Api.Helix.Bits;
/// <summary>
/// Contains a list of cheermote data.
/// </summary>
public record GetCheermotesResponse
{
    /// <summary>
    /// The list of Cheermotes. The list is in ascending order by the contained <see cref="Cheermote.Order"/> property.
    /// </summary>
    public required Cheermote[] Data { get; init; }
}