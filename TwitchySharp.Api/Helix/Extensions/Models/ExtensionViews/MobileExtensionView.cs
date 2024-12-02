namespace TwitchySharp.Api.Helix.Extensions;

/// <summary>
/// Describes how an extension is displayed on mobile devices.
/// </summary>
public record MobileExtensionView
{
    /// <summary>
    /// The HTML file that is shown to viewers on mobile devices. 
    /// This page is presented to viewers as a panel behind the chat area of the mobile app.
    /// </summary>
    public required string ViewerUrl { get; init; }
}
