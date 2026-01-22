using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using TwitchySharp.Helpers.JsonConverters.DateTime;
using TwitchySharp.Shared;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Used to create a signed JWT for various Extensions API endpoints.
/// </summary>
/// <param name="UserId">
/// The user id of the owner of the extension.
/// </param>
public record ExtensionJwtPayload(UserId UserId)
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
    public UserId UserId { get; set; } = new(UserId);
    /// <summary>
    /// The JWT role. This should always be set to <c>"external"</c> for EBS generated tokens.
    /// </summary>
    public string Role { get; } = "external";
    public string? ChannelId { get; set; }
    [JsonPropertyName("pubsub_perms")]
    public ExtensionPubSubPermissions PubSubPermissions { get; } = new()
    {
        Send = ["*"]
    };

    /// <summary>
    /// Creates an encoded JWT used to make various Extensions API calls.
    /// </summary>
    /// <param name="extensionSecret">An extension secret.</param>
    /// <param name="serializerOptions">
    /// Custom serializer options to use. 
    /// Leave <see langword="null"/> to use the default <see cref="JsonConfig.ApiOptions"/>.
    /// </param>
    /// <returns>A signed JWT.</returns>
    public ExtensionJsonWebToken Sign(string extensionSecret, JsonSerializerOptions? serializerOptions = null)
        => new(new JsonWebTokenHandler()
            .CreateToken(
                JsonSerializer.Serialize(this, serializerOptions ?? JsonConfig.ApiOptions),
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Convert.FromBase64String(extensionSecret)
                    ),
                    "HS256"
            )));
}

public record ExtensionPubSubPermissions
{
    public string[]? Listen { get; set; }
    public string[]? Send { get; set; }
}
