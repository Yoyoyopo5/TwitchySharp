using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using TwitchySharp.Serialization;
using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Authentication;
/// <summary>
/// Used to create a signed JWT for various Extensions API endpoints.
/// </summary>
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
    public required ExtensionOwnerId UserId { get; init; }
    /// <summary>
    /// The JWT role. This should always be set to <c>"external"</c> for EBS generated tokens.
    /// </summary>
    public string Role { get; } = "external";
    /// <summary>
    /// The user id of the broadcaster (channel) that the JWT is issued for.
    /// </summary>
    /// <remarks>
    /// If <see langword="null"/>, represents a <c>global</c> JWT.
    /// Typically, if the requested operation is targeting a specific channel,
    /// it should set this to that channel's broadcaster user id.
    /// </remarks>
    public UserId? ChannelId { get; init; }
    /// <summary>
    /// The set of PubSub permissions associated with the JWT.
    /// </summary>
    /// <remarks>
    /// Set to <c>Send = *</c> by default.
    /// </remarks>
    [JsonPropertyName("pubsub_perms")]
    public ExtensionPubSubPermissions PubSubPermissions { get; init; } = new()
    {
        Send = [ExtensionPubSubPermission.All]
    };

    /// <summary>
    /// Creates an encoded JWT used to make various Extensions API calls.
    /// </summary>
    /// <param name="extensionSecret">An extension secret.</param>
    /// <param name="serialize">
    /// Serialize function to use.
    /// Leave <see langword="null"/> to use the default <see cref="JsonSerializer"/>.
    /// </param>
    /// <returns>A signed JWT.</returns>
    public ExtensionJsonWebToken Sign(ExtensionSecret extensionSecret, Func<ExtensionJwtPayload, string>? serialize = null)
        => new(new JsonWebTokenHandler()
            .CreateToken(
                serialize is not null ? serialize(this) : JsonSerializer.Serialize(this, JsonConfig.ApiOptions),
                new SigningCredentials(new SymmetricSecurityKey(extensionSecret.Bytes), "HS256"))
            );
}

/// <summary>
/// Group of permissions that determine how the token holder can listen and send messages.
/// </summary>
public readonly record struct ExtensionPubSubPermissions
{
    /// <summary>
    /// The topics the token holder is allowed to listen to.
    /// </summary>
    public ExtensionPubSubPermission[]? Listen { get; init; }
    /// <summary>
    /// The topics the token holder is allowed to send to.
    /// </summary>
    public ExtensionPubSubPermission[]? Send { get; init; }
}

/// <summary>
/// An individual extension PubSub permission.
/// </summary>
/// <param name="Value">The string value of the permission</param>
[Wrapper<string>]
public readonly partial record struct ExtensionPubSubPermission(string Value)
{
    /// <summary>
    /// The wildcard permission <c>*</c> includes all permissions.
    /// </summary>
    public static ExtensionPubSubPermission All { get; } = new("*");
}
