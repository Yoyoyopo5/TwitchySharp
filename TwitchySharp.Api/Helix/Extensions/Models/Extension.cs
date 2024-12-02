using System.Collections.Generic;
using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Extensions;

/// <summary>
/// Contains information about a specific extension.
/// </summary>
public record Extension
{
    /// <summary>
    /// The name of the user or organization that owns the extension.
    /// </summary>
    public required string AuthorName { get; init; }
    /// <summary>
    /// Determines whether the extension has features that use Bits.
    /// </summary>
    public required bool BitsEnabled { get; init; }
    /// <summary>
    /// Determines whether a user can install the extension on their channel.
    /// Typically, this is set to <see langword="false"/> if the extension is currently in testing mode and requires users to be allowlisted (the allowlist is configured on Twitch’s developer site under the Extensions -> Extension -> Version -> Access).
    /// </summary>
    public required bool CanInstall { get; init; }
    /// <summary>
    /// The location of where the extension’s configuration is stored.
    /// </summary>
    [JsonConverter(typeof(SnakeCaseLowerJsonStringEnumConverter<ExtensionConfigurationLocation>))]
    public required ExtensionConfigurationLocation ConfigurationLocation { get; init; }
    /// <summary>
    /// A longer description of the extension. It appears on the details page.
    /// </summary>
    public required string Description { get; init; }
    /// <summary>
    /// A URL to the extension’s Terms of Service.
    /// </summary>
    public required string EulaTosUrl { get; init; }
    /// <summary>
    /// Determines whether the extension can communicate with the installed channel’s chat.
    /// </summary>
    public required bool HasChatSupport { get; init; }
    /// <summary>
    /// A URL to the default icon that’s displayed in the Extensions directory.
    /// </summary>
    public required string IconUrl { get; init; }
    /// <summary>
    /// Contains URLs to different sizes of the default icon. 
    /// The dictionary’s key identifies the icon’s size (for example, 24x24), and the dictionary’s value contains the URL to the icon.
    /// </summary>
    public required Dictionary<string, string> IconUrls { get; init; }
    /// <summary>
    /// The id of the extension.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The extension’s name.
    /// </summary>
    public required string Name { get; init; }
    /// <summary>
    /// A URL to the extension’s privacy policy.
    /// </summary>
    public required string PrivacyPolicyUrl { get; init; }
    /// <summary>
    /// Determines whether the extension wants to explicitly ask viewers to link their Twitch identity.
    /// </summary>
    public required bool RequestIdentityLink { get; init; }
    /// <summary>
    /// A list of URLs to screenshots that are shown in the Extensions marketplace.
    /// </summary>
    public required string[] ScreenshotUrls { get; init; }
    /// <summary>
    /// The extension’s state.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<ExtensionState>))]
    public required ExtensionState State { get; init; }
    /// <summary>
    /// Indicates whether the extension can view the user’s subscription level on the channel that the extension is installed on.
    /// </summary>
    [JsonConverter(typeof(SnakeCaseLowerJsonStringEnumConverter<ExtensionSubscriptionsSupportLevel>))]
    public required ExtensionSubscriptionsSupportLevel SubscriptionsSupportLevel { get; init; }
    /// <summary>
    /// A short description of the extension that streamers see when hovering over the discovery splash screen in the Extensions manager.
    /// </summary>
    public required string Summary { get; init; }
    /// <summary>
    /// The email address that users use to get support for the extension.
    /// </summary>
    public required string SupportEmail { get; init; }
    /// <summary>
    /// The extension’s version number.
    /// </summary>
    public required string Version { get; init; }
    /// <summary>
    /// A brief description displayed on the channel to explain how the extension works.
    /// </summary>
    public required string ViewerSummary { get; init; }
    /// <summary>
    /// Describes all views-related information such as how the extension is displayed on mobile devices.
    /// </summary>
    public required ExtensionViews Views { get; init; }
    /// <summary>
    /// Allowlisted configuration URLs for displaying the extension (the allowlist is configured on Twitch’s developer site under the Extensions -> Extension -> Version -> Capabilities).
    /// </summary>
    public required string[] AllowlistedConfigUrls { get; init; }
    /// <summary>
    /// Allowlisted panel URLs for displaying the extension (the allowlist is configured on Twitch’s developer site under the Extensions -> Extension -> Version -> Capabilities).
    /// </summary>
    public required string[] AllowlistedPanelUrls { get; init; }
}
