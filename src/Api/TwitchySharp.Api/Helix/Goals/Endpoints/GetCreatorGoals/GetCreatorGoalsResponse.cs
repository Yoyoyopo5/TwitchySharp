namespace TwitchySharp.Api.Helix.Goals;
/// <summary>
/// Contains a list of a creator's active goals.
/// </summary>
public record GetCreatorGoalsResponse
{
    /// <summary>
    /// The list of goals.
    /// This list is empty if the broadcaster hasn't created any goals.
    /// </summary>
    public required CreatorGoal[] Data { get; init; }
}
