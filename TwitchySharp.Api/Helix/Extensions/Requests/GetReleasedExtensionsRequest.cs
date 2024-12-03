using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Gets information about a released extension. 
/// Returns the extension if its state is <see cref="ExtensionState.Released"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-released-extensions">get released extensions</see> for more information.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app or user access token.</param>
/// <param name="extensionId">The id of the extension to get.</param>
/// <param name="extensionVersion">
/// The version of the extension to get. 
/// If not specified, it returns the latest version.
/// </param>
public class GetReleasedExtensionsRequest(string clientId, string accessToken, string extensionId, string? extensionVersion = null)
    : HelixApiRequest<GetReleasedExtensionsResponse>(
        "/extensions/released" +
        new HttpQueryParameters()
            .Add("extension_id", extensionId)
            .Add("extension_version", extensionVersion),
        clientId,
        accessToken
        );
