using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Extensions;

/// <summary>
/// Contains static definitions for possible extension states.
/// </summary>
/// <param name="Value">The string value of the extension state.</param>
[Wrapper<string>]
public readonly partial record struct ExtensionState(string Value)
{
    public static ExtensionState Approved { get; } = new("Approved");
    public static ExtensionState AssetsUploaded { get; } = new("AssetsUploaded");
    public static ExtensionState Deleted { get; } = new("Deleted");
    public static ExtensionState Deprecated { get; } = new("Deprecated");
    public static ExtensionState InReview { get; } = new("InReview");
    public static ExtensionState Testing { get; } = new("Testing");
    public static ExtensionState PendingAction { get; } = new("PendingAction");
    public static ExtensionState Rejected { get; } = new("Rejected");
    public static ExtensionState Released { get; } = new("Released");
}
