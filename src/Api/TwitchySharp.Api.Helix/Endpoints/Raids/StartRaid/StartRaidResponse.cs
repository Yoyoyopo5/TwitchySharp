namespace TwitchySharp.Api.Helix.Raids;
/// <inheritdoc cref="StartedRaid"/>.
public record StartRaidResponse
{
    /// <summary>
    /// A list that contains a single object with information about the pending raid.
    /// </summary>
    public required StartedRaid[] Data { get; init; }
}
