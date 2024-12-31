namespace TwitchySharp.Api.Helix.Moderation;

/// <summary>
/// The filtering level for AutoMod categories.
/// They range from 0 to 4, with 4 being the highest filtering level.
/// </summary>
public enum AutoModFilteringLevel
{
    /// <summary>
    /// Level 0: No filtering.
    /// </summary>
    None = 0,
    /// <summary>
    /// Level 1: Less filtering.
    /// </summary>
    Less = 1,
    /// <summary>
    /// Level 2: Some filtering.
    /// </summary>
    Some = 2,
    /// <summary>
    /// Level 3: More filtering.
    /// </summary>
    More = 3,
    /// <summary>
    /// Level 4: Maximum filtering.
    /// </summary>
    Maximum = 4
}
