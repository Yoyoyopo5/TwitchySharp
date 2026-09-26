namespace TwitchySharp.Api.Authentication;

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
        public BearerToken BearerToken => AccessToken;
        public required DateTimeOffset ExpiresAt { get; init; }
    }

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
