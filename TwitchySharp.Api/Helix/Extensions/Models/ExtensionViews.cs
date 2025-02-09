namespace TwitchySharp.Api.Helix.Extensions;

/// <summary>
/// Describes all of the views of a specific extension (e.g. how the extension is displayed on mobile devices).
/// </summary>
public record ExtensionViews // Not sure which of the following are always present. Will try to test and remove required modifiers if necessary.
{
    /// <summary>
    /// Describes how the extension is displayed on mobile devices.
    /// </summary>
    public MobileExtensionView? Mobile { get; init; }
    /// <summary>
    /// Describes how the extension is rendered if the extension may be activated as a panel extension.
    /// </summary>
    public PanelExtensionView? Panel { get; init; }
    /// <summary>
    /// Describes how the extension is rendered if the extension may be activated as a video-overlay extension.
    /// </summary>
    public VideoOverlayExtensionView? VideoOverlay { get; init; }
    /// <summary>
    /// Describes how the extension is rendered if the extension may be activated as a video-component extension.
    /// </summary>
    public ComponentExtensionView? Component { get; init; }
    /// <summary>
    /// Describes the view that is shown to broadcasters while they are configuring your extension within the Extension Manager.
    /// </summary>
    public required ConfigExtensionView Config { get; init; }
}
