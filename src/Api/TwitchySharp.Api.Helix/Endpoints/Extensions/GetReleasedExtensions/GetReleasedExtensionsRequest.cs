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
    : TwitchHelixRequest<GetReleasedExtensionsResponseContent>,
    IAuthenticatedTwitchRequest<ITwitchRequestAuthenticationContext<TwitchIdentity>>
{
    protected override string Path => "/extensions/released";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("extension_id", ExtensionId)
            .Add("extension_version", ExtensionVersion?.ToString());
    public ITwitchRequestAuthenticationContext<TwitchIdentity> AuthenticationContext { get; init; }
        = TwitchRequestAuthenticationContext.Default;

    /// <summary>
    /// The id of the extension to get.
    /// </summary>
    public required ExtensionId ExtensionId { get; init; }

    /// <summary>
    /// The version of the extension to get.
    /// </summary>
    /// <remarks>
    /// If <see langword="null"/>, it returns the latest version.
    /// </remarks>
    public ExtensionVersion? ExtensionVersion { get; init; }
}
