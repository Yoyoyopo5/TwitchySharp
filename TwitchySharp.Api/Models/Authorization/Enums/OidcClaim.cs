using System;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Models.Authorization.Enums;
/// <summary>
/// Represents additional OIDC claims that can be requested from a <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oidc/">Twitch OIDC authorization flow</see> or the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oidc/#getting-claims-information-from-an-access-token">UserInfo</see> endpoint.
/// </summary>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<OidcClaim, string>))]
public record OidcClaim(string Value) : ValueBackedEnum<string>(Value)
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
}
