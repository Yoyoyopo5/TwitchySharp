namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// Contains data about the newly created Guest Star session.
/// </summary>
public record CreateGuestStarSessionResponseContent
{
    /// <summary>
    /// A list containing a single entry for the newly created Guest Star session.
    /// </summary>
    public required GuestStarSession[] Data { get; init; }
}
