using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Used to create a signed JWT for various Extensions API endpoints.
/// </summary>
/// <param name="UserId">
/// The user id of the owner of the extension.
/// </param>
public record ExtensionJwtPayload
{
    /// <summary>
    /// When the JWT is set to expire. 
    /// Defaults to 120 minutes from object creation.
    /// </summary>
    [JsonConverter(typeof(UnixSecondsDateTimeOffsetConverter))]
    [JsonPropertyName("exp")]
    public DateTimeOffset ExpiresAt { get; init; } = DateTimeOffset.UtcNow.AddMinutes(120);
    /// <summary>
    /// The user id of the owner of the extension.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The JWT role. This should always be set to <c>"external"</c> for EBS generated tokens.
    /// </summary>
    public string Role { get; } = "external";
    public UserId? ChannelId { get; init; }
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
    public ExtensionJsonWebToken Sign(ExtensionSecret extensionSecret, JsonSerializerOptions? serializerOptions = null)
        => new(new JsonWebTokenHandler()
            .CreateToken(
                JsonSerializer.Serialize(this, serializerOptions ?? JsonConfig.ApiOptions),
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Convert.FromBase64String(extensionSecret.Value)
                    ),
                    "HS256"
            )));
}

public readonly record struct ExtensionPubSubPermissions
{
    public string[]? Listen { get; init; }
    public string[]? Send { get; init; }
}
