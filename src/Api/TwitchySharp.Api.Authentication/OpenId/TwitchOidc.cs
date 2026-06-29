using System.Text.Json.Serialization;
using Microsoft.IdentityModel.JsonWebTokens;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// An <see href="https://openid.net/">OpenID Connect</see> claims collection used by Twitch to relay user information.
/// </summary>
public record TwitchOidc
{
    /// <summary>
    /// Creates a strongly-typed set of OIDC claims known to be used by Twitch from a standard <see cref="JsonWebToken"/>.
    /// </summary>
    /// <param name="jwt">The JWT returned by Twitch OIDC authorization flows as an ID token.</param>
    /// <returns>
    /// A <see cref="TwitchOidc"/> populated with the claims from the <paramref name="jwt"/>.
    /// If a non-nullable claim was not present in the <paramref name="jwt"/>, it is filled with an empty string or default value.
    /// </returns>
    public static TwitchOidc FromJsonWebToken(JsonWebToken jwt)
        => new()
        {
            Aud = new(jwt.Audiences.FirstOrDefault() ?? string.Empty),
            Azp = new(jwt.Azp),
            Exp = jwt.ValidTo,
            Iat = jwt.IssuedAt,
            Iss = new(jwt.Issuer),
            Sub = new(jwt.Subject),
            Nonce = jwt.GetValueOrDefault<string>("nonce"),
            Email = jwt.GetValueOrDefault<string>("email") is string email ? new(email) : null,
            EmailVerified = jwt.GetValueOrNull<bool>("email_verified"),
            Picture = jwt.GetValueOrDefault<string>("picture") is string url ? new(url) : null,
            PreferredUsername = jwt.GetValueOrDefault<string>("preferred_username") is string name ? new(name) : null,
            UpdatedAt = jwt.TryGetValue("updated_at", out int seconds) ? DateTimeOffset.FromUnixTimeSeconds(seconds) : null
        };

    public static explicit operator TwitchOidc(JsonWebToken jwt)
        => FromJsonWebToken(jwt);

    /// <summary>
    /// The client ID of the application that requested the user’s authorization.
    /// </summary>
    public required ClientId Aud { get; init; }
    /// <summary>
    /// The client ID of the application that received the user’s authorization. 
    /// This contains the same value as <see cref="Aud"/>.
    /// </summary>
    public required ClientId Azp { get; init; }
    /// <summary>
    /// The UNIX timestamp of when the token expires.
    /// </summary>
    [JsonConverter(typeof(UnixSecondsDateTimeOffsetConverter))]
    public required DateTimeOffset Exp { get; init; }
    /// <summary>
    /// The UNIX timestamp of when the server issued the token.
    /// </summary>
    [JsonConverter(typeof(UnixSecondsDateTimeOffsetConverter))]
    public required DateTimeOffset Iat { get; init; }
    /// <summary>
    /// The URI of the issuing authority (twitch.tv in this case).
    /// </summary>
    public required Url Iss { get; init; }
    /// <summary>
    /// The Twitch ID of the user that authorized the app.
    /// </summary>
    public required UserId Sub { get; init; }
    /// <summary>
    /// The nonce that was used in the authorization request, if one was used.
    /// </summary>
    public string? Nonce { get; init; }
    /// <summary>
    /// The email address of the user that authorized the app.
    /// Obtaining this requires <see cref="OidcClaim.Email"/> and <see cref="Scope.UserReadEmail"/> during authorization.
    /// </summary>
    public UserEmail? Email { get; init; }
    /// <summary>
    /// A Boolean value that indicates whether Twitch has verified the user’s email address. Is <see langword="true"/> if Twitch has verified the user’s email address.
    /// Obtaining this requires <see cref="OidcClaim.EmailVerified"/> and <see cref="Scope.UserReadEmail"/> during authorization.
    /// </summary>
    public bool? EmailVerified { get; init; }
    /// <summary>
    /// A URL to the user’s profile image if they included one; otherwise, a default image.
    /// Obtaining this requires <see cref="OidcClaim.Picture"/> during authorization.
    /// </summary>
    public ImageUrl? Picture { get; init; }
    /// <summary>
    /// The user’s display name.
    /// Obtaining this requires <see cref="OidcClaim.PreferredUsername"/> during authorization.
    /// </summary>
    public UserName? PreferredUsername { get; init; }
    /// <summary>
    /// The date and time (ISO 8601) that the user last updated their profile.
    /// Obtaining this requires <see cref="OidcClaim.UpdatedAt"/> during authorization.
    /// </summary>
    public DateTimeOffset? UpdatedAt { get; init; }
}

internal static class JsonWebTokenExtensions
{
    public static T? GetValueOrDefault<T>(this JsonWebToken jwt, string claim)
        => jwt.TryGetValue(claim, out T value) ? value : default;

    public static Nullable<T> GetValueOrNull<T>(this JsonWebToken jwt, string claim)
        where T : unmanaged
        => jwt.TryGetValue(claim, out T value) ? value : null;
}
