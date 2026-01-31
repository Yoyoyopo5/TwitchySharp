using System.Buffers;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace TwitchySharp.Api.Authorization;

/// <summary>
/// Claims container used when getting <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oidc/">access tokens with OIDC</see>.
/// </summary>
public class OidcClaims
{
    /// <summary>
    /// Claims that will be included in the id token of the authorization response.
    /// </summary>
    public HashSet<OidcClaim> IdToken { get; init; } = [];
    /// <summary>
    /// Claims that will be included when getting a response from the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oidc/#getting-claims-information-from-an-access-token">UserInfo</see> endpoint.
    /// </summary>
    public HashSet<OidcClaim> Userinfo { get; init; } = [];

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
            jsonWriter.WriteNull(claim);
        }
        jsonWriter.WriteEndObject();

        jsonWriter.WritePropertyName("userinfo");
        jsonWriter.WriteStartObject();
        foreach (OidcClaim claim in Userinfo)
        {
            jsonWriter.WriteNull(claim);
        }
        jsonWriter.WriteEndObject();

        jsonWriter.WriteEndObject();

        jsonWriter.Flush();
        return Encoding.UTF8.GetString(buffer.WrittenMemory.ToArray());
    }
}
