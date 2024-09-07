using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Authorization.Responses;
public class OidcJwkResponse
{
    /// <summary>
    /// An array of JWKs used to verify OIDC JWTs obtained as ID tokens during Twitch OIDC authorization flows.
    /// </summary>
    [JsonRequired]
    public JsonWebKey[] Keys { get; } = [];
}
