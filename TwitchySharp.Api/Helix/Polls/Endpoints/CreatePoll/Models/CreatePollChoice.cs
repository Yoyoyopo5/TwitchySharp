namespace TwitchySharp.Api.Helix.Polls;

/// <summary>
/// A choice for a new poll.
/// </summary>
public record CreatePollChoice
{
    /// <summary>
    /// The title of the choice that is visible to viewers.
    /// This may contain up to 25 characters.
    /// </summary>
    public required string Title { get; set; }
}
