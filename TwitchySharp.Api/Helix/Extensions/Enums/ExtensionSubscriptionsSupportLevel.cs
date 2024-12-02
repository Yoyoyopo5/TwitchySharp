namespace TwitchySharp.Api.Helix.Extensions;

/// <summary>
/// Possible values for an extension's ability to view user subscription levels.
/// </summary>
public enum ExtensionSubscriptionsSupportLevel
{
    /// <summary>
    /// The extension can't view the user’s subscription level.
    /// </summary>
    None,
    /// <summary>
    /// The extension can view the user’s subscription level.
    /// </summary>
    Optional
}
