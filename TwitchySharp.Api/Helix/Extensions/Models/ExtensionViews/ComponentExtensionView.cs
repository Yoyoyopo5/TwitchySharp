namespace TwitchySharp.Api.Helix.Extensions;

/// <summary>
/// Describes how an extension is rendered if activated as a video-component extension.
/// </summary>
public record ComponentExtensionView
{
    /// <summary>
    /// The HTML file that is shown to viewers on the channel page when the extension is activated in a Video - Component slot.
    /// </summary>
    public required string ViewerUrl { get; init; }
    /// <summary>
    /// Determines whether the extension can link to non-Twitch domains.
    /// </summary>
    public required bool CanLinkExternalContent { get; init; }
    /// <summary>
    /// The width value of the ratio (width : height) which determines the extension’s width, and how the extension’s iframe will resize in different video player environments.
    /// </summary>
    public required int AspectRatioX { get; init; }
    /// <summary>
    /// The height value of the ratio (width : height) which determines the extension’s height, and how the extension’s iframe will resize in different video player environments.
    /// </summary>
    public required int AspectRatioY { get; init; }
    /// <summary>
    /// Determines whether to apply CSS zoom. 
    /// If <see langword="true"/>, a CSS zoom is applied such that the size of the extension is variable but the inner dimensions are fixed based on <see cref="ScalePixels"/>. 
    /// This allows your extension to render as if it is of fixed width and height. 
    /// If <see langword="false"/>, the inner dimensions of the extension iframe are variable, meaning your extension must implement responsiveness.
    /// </summary>
    public required bool Autoscale { get; init; }
    /// <summary>
    /// The base width, in <b>pixels</b>, of the extension to use when scaling. This value is ignored if <see cref="Autoscale"/> is <see langword="false"/>.
    /// </summary>
    public required int ScalePixels { get; init; }
    /// <summary>
    /// The height as a <b>percent</b> of the maximum height of a video component extension. 
    /// Values are between 1 - 100.
    /// </summary>
    public required int TargetHeight { get; init; }
}
