using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using TwitchySharp.Helpers.JsonConverters.DateTime;

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
        => new TwitchOidc()
        {
            Aud = jwt.Audiences.FirstOrDefault() ?? string.Empty,
            Azp = jwt.Azp,
            Exp = jwt.ValidTo,
            Iat = jwt.IssuedAt,
            Iss = jwt.Issuer,
            Sub = jwt.Subject,
            Nonce = jwt.GetValueOrDefault<string>("nonce"),
            Email = jwt.GetValueOrDefault<string>("email"),
            EmailVerified = jwt.GetValueOrDefault<bool>("email_verified"),
            Picture = jwt.GetValueOrDefault<string>("picture"),
            PreferredUsername = jwt.GetValueOrDefault<string>("preferred_username"),
            UpdatedAt = jwt.GetValueOrDefault<DateTime>("updated_at")
        };


    /// <summary>
    /// The client ID of the application that requested the user’s authorization.
    /// </summary>
    public required string Aud { get; init; }
    /// <summary>
    /// The client ID of the application that received the user’s authorization. 
    /// This contains the same value as <see cref="Aud"/>.
    /// </summary>
    public required string Azp { get; init; }
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
    public required string Iss { get; init; }
    /// <summary>
    /// The Twitch ID of the user that authorized the app.
    /// </summary>
    public required string Sub { get; init; }
    /// <summary>
    /// The nonce that was used in the authorization request, if one was used.
    /// </summary>
    public string? Nonce { get; init; }
    /// <summary>
    /// The email address of the user that authorized the app.
    /// Obtaining this requires <see cref="OidcClaim.Email"/> and <see cref="Scope.ReadUserEmail"/> during authorization.
    /// </summary>
    public string? Email { get; init; }
    /// <summary>
    /// A Boolean value that indicates whether Twitch has verified the user’s email address. Is <see langword="true"/> if Twitch has verified the user’s email address.
    /// Obtaining this requires <see cref="OidcClaim.EmailVerified"/> and <see cref="Scope.ReadUserEmail"/> during authorization.
    /// </summary>
    public bool? EmailVerified { get; init; }
    /// <summary>
    /// A URL to the user’s profile image if they included one; otherwise, a default image.
    /// Obtaining this requires <see cref="OidcClaim.Picture"/> during authorization.
    /// </summary>
    public string? Picture { get; init; }
    /// <summary>
    /// The user’s display name.
    /// Obtaining this requires <see cref="OidcClaim.PreferredUsername"/> during authorization.
    /// </summary>
    public string? PreferredUsername { get; init; }
    /// <summary>
    /// The date and time (ISO 8601) that the user last updated their profile.
    /// Obtaining this requires <see cref="OidcClaim.UpdatedAt"/> during authorization.
    /// </summary>
    public DateTimeOffset? UpdatedAt { get; init; }
}

internal static class JsonWebTokenExtensions
{
    public static T? GetValueOrDefault<T>(this JsonWebToken jwt, string claim)
    {
        if (jwt.TryGetValue(claim, out T value))
            return value;
        return default;
    }
}
