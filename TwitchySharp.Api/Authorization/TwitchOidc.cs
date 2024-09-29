using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// An <see href="https://openid.net/">OpenID Connect</see> claims collection used by Twitch to relay user information.
/// </summary>
public class TwitchOidc
{
    public TwitchOidc() { }

    /// <summary>
    /// Creates a strongly-typed set of OIDC claims known to be used by Twitch from a standard <see cref="JsonWebToken"/>.
    /// </summary>
    /// <param name="jwt">The JWT returned by Twitch OIDC authorization flows as an ID token.</param>
    /// <returns>
    /// A <see cref="TwitchOidc"/> populated with the claims from the <paramref name="jwt"/> or
    /// <see cref="ArgumentException"/> when a required key was not present or was invalid.
    /// </returns>
    public static OneOf<TwitchOidc, Exception> FromJsonWebToken(JsonWebToken jwt)
    {
        // Could change this to automatically find claims based on each property name
        // would require reflection or source generation, probably not worth the effort here
        // BUT beware of bugs if adding/removing any claims in the future.
        
        if (jwt.GetValue<string>("aud").TryPickT1(out ArgumentException ex, out string aud))
            return ex;
        if (jwt.GetValue<long>("exp").TryPickT1(out ex, out long exp))
            return ex;
        if (jwt.GetValue<long>("iat").TryPickT1(out ex, out long iat))
            return ex;
        if (jwt.GetValue<string>("iss").TryPickT1(out ex, out string iss))
            return ex;
        if (jwt.GetValue<string>("sub").TryPickT1(out ex, out string sub))
            return ex;

        return new TwitchOidc()
        {
            // Required
            Aud = aud,
            Exp = exp,
            Iat = iat,
            Iss = iss,
            Sub = sub,

            // Optional
            Email = jwt.GetValueOrDefault<string>("email"),
            EmailVerified = jwt.GetValueOrDefault<bool>("email_verified"),
            Picture = jwt.GetValueOrDefault<string>("picture"),
            PreferredUsername = jwt.GetValueOrDefault<string>("preferred_username"),
            UpdatedAt = jwt.GetValueOrDefault<string>("updated_at")
        };
    }


    /// <summary>
    /// The client ID of the application that requested the user’s authorization.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string Aud { get; private set; } = string.Empty;
    /// <summary>
    /// The UNIX timestamp of when the token expires.
    /// </summary>
    [JsonInclude, JsonRequired]
    public long Exp { get; private set; }
    /// <summary>
    /// The UNIX timestamp of when the server issued the token.
    /// </summary>
    [JsonInclude, JsonRequired]
    public long Iat { get; private set; }
    /// <summary>
    /// The URI of the issuing authority (twitch.tv in this case).
    /// </summary>
    [JsonInclude, JsonRequired]
    public string Iss { get; private set; } = string.Empty;
    /// <summary>
    /// The Twitch ID of the user that authorized the app.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string Sub { get; private set; } = string.Empty;
    /// <summary>
    /// The email address of the user that authorized the app.
    /// Obtaining this requires <see cref="OidcClaim.Email"/> and <see cref="Scope.ReadUserEmail"/> during authorization.
    /// </summary>
    public string? Email { get; private set; }
    /// <summary>
    /// A Boolean value that indicates whether Twitch has verified the user’s email address. Is <see langword="true"/> if Twitch has verified the user’s email address.
    /// Obtaining this requires <see cref="OidcClaim.EmailVerified"/> and <see cref="Scope.ReadUserEmail"/> during authorization.
    /// </summary>
    public bool? EmailVerified { get; private set; }
    /// <summary>
    /// A URL to the user’s profile image if they included one; otherwise, a default image.
    /// Obtaining this requires <see cref="OidcClaim.Picture"/> during authorization.
    /// </summary>
    public string? Picture { get; private set; }
    /// <summary>
    /// The user’s display name.
    /// Obtaining this requires <see cref="OidcClaim.PreferredUsername"/> during authorization.
    /// </summary>
    public string? PreferredUsername { get; private set; }
    /// <summary>
    /// The date and time (ISO 8601) that the user last updated their profile.
    /// Obtaining this requires <see cref="OidcClaim.UpdatedAt"/> during authorization.
    /// </summary>
    public string? UpdatedAt { get; private set; }
}

internal static class JsonWebTokenExtensions
{
    public static T? GetValueOrDefault<T>(this JsonWebToken jwt, string claim)
    {
        if (jwt.TryGetValue(claim, out T value))
            return value;
        return default;
    }

    public static OneOf<T, ArgumentException> GetValue<T>(this JsonWebToken jwt, string claim)
    {
        if (jwt.TryGetValue(claim, out T value))
            return value;
        return new ArgumentException($"Claim with key \"{claim}\" was not found in the supplied JWT or it was an invalid value type.");
    }
}
