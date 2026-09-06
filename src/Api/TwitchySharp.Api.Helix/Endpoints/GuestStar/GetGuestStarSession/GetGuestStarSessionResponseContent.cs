namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// Contains a list with Guest Star session information.
/// </summary>
public record GetGuestStarSessionResponseContent
{
    /// <summary>
    /// A list with a single entry of the Guest Star session details.
    /// </summary>
    public required GuestStarSession[] Data { get; init; }
}
