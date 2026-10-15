namespace TwitchySharp.Api.Helix.Bits;

/// <summary>
/// Contains a list of custom Bits power-ups.
/// </summary>
public record GetCustomPowerUpsResponseContent
{
    /// <summary>
    /// The list of the broadcaster's custom Power-ups.
    /// </summary>
    /// <remarks>
    /// The list is in ascending order by id.
    /// If the broadcaster hasn’t created custom Power-ups, the list is empty.
    /// </remarks>
    public required CustomPowerUp[] Data { get; init; }
}
