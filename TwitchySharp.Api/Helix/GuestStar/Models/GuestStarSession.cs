namespace TwitchySharp.Api.Helix.GuestStar;

/// <summary>
/// Contains data about a specific Guest Star session.
/// </summary>
public record GuestStarSession
{
    /// <summary>
    /// The id of the session.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// A list of guests currently interacting with the Guest Star session.
    /// </summary>
    public required GuestStarSessionGuest[] Guests { get; init; }
}
