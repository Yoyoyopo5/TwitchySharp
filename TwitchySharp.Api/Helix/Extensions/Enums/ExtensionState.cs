namespace TwitchySharp.Api.Helix.Extensions;

/// <summary>
/// Possible states than an extension can be in.
/// </summary>
public enum ExtensionState
{
    Approved,
    AssetsUploaded,
    Deleted,
    Deprecated,
    InReview,
    Testing, // API docs mark this as "InTest" but API response returns this value
    PendingAction,
    Rejected,
    Released
}
