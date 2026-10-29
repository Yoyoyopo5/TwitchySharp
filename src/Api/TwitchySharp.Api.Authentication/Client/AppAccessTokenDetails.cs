namespace TwitchySharp.Api.Authentication;

/// <summary>
/// Contains subtypes of access token details.
/// </summary>
/// <remarks>
/// These <i>details</i> types contain additional information about a specific access token.
/// This can include the identity that the token belongs to and its expiration time.
/// See also <see cref="IAccessTokenDetails{TIdentity}"/>.
/// </remarks>
public static partial class AccessTokenDetails
{
    /// <summary>
    /// Details associated with a specific <see cref="AppAccessToken"/>.
    /// </summary>
    public sealed record App : IAccessTokenDetails<TwitchIdentity.Client>
    {
        /// <summary>
        /// The client identity associated with the app access token.
        /// </summary>
        public required TwitchIdentity.Client Identity { get; init; }
        /// <summary>
        /// The app access token.
        /// </summary>
        public required AppAccessToken AccessToken { get; init; }
        /// <inheritdoc/>
        public BearerToken BearerToken => AccessToken;
        /// <inheritdoc/>
        public required DateTimeOffset ExpiresAt { get; init; }
    }

    /// <summary>
    /// Obtain app access tokens details from a <see cref="ClientCredentialsResponseContent"/>.
    /// </summary>
    /// <param name="credentialsResponse">The response to get details from.</param>
    /// <param name="clientId">The client id used to get the response.</param>
    /// <param name="now">The current time (used to determine expiry).</param>
    /// <returns>A new <see cref="AccessTokenDetails.App"/>.</returns>
    public static AccessTokenDetails.App ToAccessTokenDetails(
        this ClientCredentialsResponseContent credentialsResponse,
        ClientId clientId,
        DateTimeOffset now
        )
        => new()
        {
            Identity = new(clientId),
            AccessToken = credentialsResponse.AccessToken,
            ExpiresAt = now + credentialsResponse.ExpiresIn,
        };
}
