using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Gets information about an extension.
/// </summary>
/// <remarks>
/// <br/>
/// Requires a signed JSON Web Token (JWT) created by an EBS. 
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>. 
/// The signed JWT must include the role field (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>). 
/// The role field must be set to external.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-extensions">Get Extensions</see> for more information.
/// </remarks>
public record GetExtensionsRequest : TwitchHelixRequest<GetExtensionsResponse>
{
    /// <param name="clientId">The client id of the extension.</param>
    /// <param name="jwt">A signed JWT created by an EBS.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetExtensionsRequest(
        ClientId clientId,
        ExtensionJsonWebToken jwt,
        GetExtensionsRequestParameters parameters
        ) : base(
            "/extensions",
            clientId,
            jwt,
            new HttpQueryParameters()
                .Add("extension_id", parameters.ExtensionId)
                .Add("extension_version", parameters.ExtensionVersion)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request data for a <see cref="GetExtensionsRequest"/>.
/// </summary>
public record GetExtensionsRequestParameters
{
    /// <summary>
    /// The id of the extension to get.
    /// </summary>
    public required ExtensionId ExtensionId { get; set; }
    /// <summary>
    /// The version of the extension to get. 
    /// </summary>
    /// <remarks>
    /// If <see langword="null"/>, it returns the latest, released version. 
    /// If the extension doesn't have a released version, you must specify a version; otherwise, <see cref="GetExtensionsResponse.Data"/> is empty.
    /// </remarks>
    public ExtensionVersion? ExtensionVersion { get; set; }
}
