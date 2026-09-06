namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Gets an extension's list of shared secrets.
/// </summary>
/// <remarks>
/// Requires a signed JSON Web Token (JWT) created by an EBS.
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>.
/// The signed JWT must include the role, user_id, and exp fields (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>).
/// The role field must be set to external.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-extension-secrets">Get Extension Secrets</see> for more information.
/// </remarks>
public record GetExtensionSecretsRequest
    : TwitchHelixRequest<GetExtensionSecretsResponseContent>,
    IAuthenticatedTwitchRequest<TwitchRequestAuthenticationContext<TwitchIdentity.Extension>>
{
    protected override string Path => "/extensions/jwt/secrets";
    public override HttpMethod Method => HttpMethod.Get;
    private TwitchRequestAuthenticationContext<TwitchIdentity.Extension> DefaultAuthenticationContext => new()
    {
        Identity = new(
            _,
            null,
            ExtensionId
            )
    };
    public TwitchRequestAuthenticationContext<TwitchIdentity.Extension> AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }

    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("extension_id", ExtensionId);

    /// <summary>
    /// The id of the extension whose shared secrets you want to get.
    /// </summary>
    public required ExtensionId ExtensionId { get; init; }
}
