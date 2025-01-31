using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Helpers.JsonConverters.DateTime;

namespace TwitchySharp.Api.Authorization.Extensions;
/// <summary>
/// Used to create a signed JWT for various Extensions API endpoints.
/// </summary>
/// <param name="UserId">
/// The user id of the owner of the extension.
/// </param>
public record ExtensionJwtPayload(string UserId)
{
    /// <summary>
    /// When the JWT is set to expire. 
    /// Defaults to 120 minutes from object creation.
    /// </summary>
    [JsonConverter(typeof(UnixSecondsDateTimeOffsetConverter))]
    [JsonPropertyName("exp")]
    public DateTimeOffset ExpiresAt { get; set; } = DateTimeOffset.UtcNow.AddMinutes(120);
    /// <summary>
    /// The user id of the owner of the extension.
    /// </summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; set; } = UserId;
    /// <summary>
    /// The JWT role. This should always be set to <c>"external"</c> for EBS generated tokens.
    /// </summary>
    [JsonPropertyName("role")]
    public string Role { get; } = "external";
    [JsonPropertyName("pubsub_perms")]
    public ExtensionPubSubPermissions PubSubPermissions { get; } = new()
    {
        Send = ["*"]
    };

    /// <summary>
    /// Creates an encoded JWT used to make various Extensions API calls.
    /// </summary>
    /// <param name="extensionSecret">An extension secret.</param>
    /// <returns>A signed JWT.</returns>
    public string Sign(string extensionSecret)
        => new JsonWebTokenHandler()
            .CreateToken(
                JsonSerializer.Serialize(this, JsonConfig.ApiOptions), 
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Convert.FromBase64String(extensionSecret)
                    ),
                    "HS256"
            ));
}

public record ExtensionPubSubPermissions
{
    public string[]? Listen { get; set; }
    public string[]? Send { get; set; }
}
