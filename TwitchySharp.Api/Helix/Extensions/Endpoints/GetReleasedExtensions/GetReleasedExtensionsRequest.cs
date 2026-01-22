using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Gets information about a released extension.
/// </summary>
/// <remarks>
/// Returns the extension if its state is <see cref="ExtensionState.Released"/>.
/// <br/>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-released-extensions">Get Released Extensions</see> for more information.
/// </remarks>
public record GetReleasedExtensionsRequest
    : TwitchHelixRequest<GetReleasedExtensionsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetReleasedExtensionsRequest(
        ClientId clientId,
        AccessToken accessToken,
        GetReleasedExtensionsRequestParameters parameters
        ) : base(
            "/extensions/released",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("extension_id", parameters.ExtensionId)
                .Add("extension_version", parameters.ExtensionVersion)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetReleasedExtensionsRequest"/>.
/// </summary>
public record GetReleasedExtensionsRequestParameters
{
    /// <summary>
    /// The id of the extension to get.
    /// </summary>
    public required ExtensionId ExtensionId { get; set; }
    /// <summary>
    /// The version of the extension to get. 
    /// </summary>
    /// <remarks>
    /// If <see langword="null"/>, it returns the latest version.
    /// </remarks>
    public ExtensionVersion? ExtensionVersion { get; set; }
}
