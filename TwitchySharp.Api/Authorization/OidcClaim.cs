using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Buffers;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Represents additional OIDC claims that can be requested from a <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oidc/">Twitch OIDC authorization flow</see> or the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oidc/#getting-claims-information-from-an-access-token">UserInfo</see> endpoint.
/// </summary>
public record OidcClaim
{
    /// <summary>
    /// The email address of the user that authorized the app. Requires <see cref="Scope.ReadUserEmail"/>.
    /// </summary>
    public static OidcClaim Email { get; } = new("email");
    /// <summary>
    /// A Boolean value that indicates whether Twitch has verified the user’s email address. Is true if Twitch has verified the user’s email address. Requires <see cref="Scope.ReadUserEmail"/>.
    /// </summary>
    public static OidcClaim EmailVerified { get; } = new("email_verified");
    /// <summary>
    /// A URL to the user’s profile image if they included one; otherwise, a default image.
    /// </summary>
    public static OidcClaim Picture { get; } = new("picture");
    /// <summary>
    /// The user’s display name.
    /// </summary>
    public static OidcClaim PreferredUsername { get; } = new("preferred_username");
    /// <summary>
    /// The date and time that the user last updated their profile.
    /// </summary>
    public static OidcClaim UpdatedAt { get; } = new("updated_at");

    private readonly string _claim;
    public override string ToString()
        => _claim;
    private OidcClaim(string claim)
    {
        _claim = claim;
    }
}

/// <summary>
/// Claims container used when getting <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oidc/">access tokens with OIDC</see>.
/// </summary>
public class OidcClaims
{
    /// <summary>
    /// Claims that will be included in the id token of the authorization response.
    /// </summary>
    public HashSet<OidcClaim> IdToken { get; } = [];
    /// <summary>
    /// Claims that will be included when getting a response from the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oidc/#getting-claims-information-from-an-access-token">UserInfo</see> endpoint.
    /// </summary>
    public HashSet<OidcClaim> Userinfo { get; } = [];

    /// <summary>
    /// JSON encode the OIDC claims to be used in the claims query parameter of an authorization url.
    /// </summary>
    /// <returns></returns>
    internal string JsonEncode()
    {
        ArrayBufferWriter<byte> buffer = new();
        using Utf8JsonWriter jsonWriter = new(buffer);

        jsonWriter.WriteStartObject();
        
        jsonWriter.WritePropertyName("id_token");
        jsonWriter.WriteStartObject();
        foreach (OidcClaim claim in IdToken)
        {
            jsonWriter.WriteNull(claim.ToString());
        }
        jsonWriter.WriteEndObject();

        jsonWriter.WritePropertyName("userinfo");
        jsonWriter.WriteStartObject();
        foreach (OidcClaim claim in Userinfo)
        {
            jsonWriter.WriteNull(claim.ToString());
        }
        jsonWriter.WriteEndObject();

        jsonWriter.WriteEndObject();

        jsonWriter.Flush();
        return Encoding.UTF8.GetString(buffer.WrittenMemory.ToArray());
    }
}
