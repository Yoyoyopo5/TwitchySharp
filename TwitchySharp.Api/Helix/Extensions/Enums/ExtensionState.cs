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
    InTest,
    PendingAction,
    Rejected,
    Released
}
