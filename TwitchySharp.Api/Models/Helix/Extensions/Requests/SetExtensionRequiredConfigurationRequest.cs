using System.Net.Http;
using TwitchySharp.Api.Models.Helix.Extensions.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Extensions.Requests;
/// <summary>
/// Updates the extension’s required_configuration string.
/// </summary>
/// <remarks>
/// Use this endpoint if your extension requires the broadcaster to configure the extension before activating it (to require configuration, you must select Custom/My Own Service in Extension <see href="https://dev.twitch.tv/docs/extensions/life-cycle/#capabilities">Capabilities</see>). 
/// For more information, see <see href="https://dev.twitch.tv/docs/extensions/building#required-configurations">Required Configurations</see> and <see href="https://dev.twitch.tv/docs/extensions/building#setting-required-configuration-with-the-configuration-service-optional">Setting Required Configuration</see>.
/// <br/>
/// Requires a signed JSON Web Token (JWT) created by an EBS. 
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>. 
/// The signed JWT must include the role, user_id, and exp fields (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>). 
/// Set the role field to external and the user_id field to the user id of the user that owns the extension.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#set-extension-required-configuration">Set Extension Required Configuration</see> for more information.
/// </remarks>
public record SetExtensionRequiredConfigurationRequest
    : TwitchHelixRequest<SetExtensionRequiredConfigurationResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="jwt">A signed JWT created by an EBS.</param>
    /// <param name="broadcasterId">The user id of the broadcaster that installed the extension on their channel.</param>
    /// <param name="data">The data used to set the required configuration setting.</param>
    public SetExtensionRequiredConfigurationRequest(
        string clientId,
        string jwt,
        string broadcasterId,
        SetExtensionRequiredConfigurationRequestData data
        ) : base(
            "/extensions/required_configuration",
            clientId,
            jwt,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
            )
    {
        Method = HttpMethod.Put;
        ContentObject = data;
    }
}

/// <summary>
/// Contains data used to set an extension's required configuration setting.
/// </summary>
public record SetExtensionRequiredConfigurationRequestData
{
    /// <summary>
    /// The id of the extension to update.
    /// </summary>
    public required string ExtensionId { get; set; }
    /// <summary>
    /// The version of the extension to update.
    /// </summary>
    public required string ExtensionVersion { get; set; }
    /// <summary>
    /// The required_configuration string to use with the extension.
    /// </summary>
    public required string RequiredConfiguration { get; set; }
}
