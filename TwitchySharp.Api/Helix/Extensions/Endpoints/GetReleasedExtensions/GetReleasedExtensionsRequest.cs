using System.Net.Http;
using TwitchySharp.Helpers;

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
    /// <param name="extensionId">The id of the extension to get.</param>
    /// <param name="extensionVersion">
    /// The version of the extension to get. 
    /// If not specified, it returns the latest version.
    /// </param>
    public GetReleasedExtensionsRequest(
        string clientId,
        string accessToken,
        string extensionId,
        string? extensionVersion = null
        ) : base(
            "/extensions/released",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("extension_id", extensionId)
                .Add("extension_version", extensionVersion)
            )
    {
        Method = HttpMethod.Get;
    }
}
