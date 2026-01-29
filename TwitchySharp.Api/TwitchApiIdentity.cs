using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api;

/// <summary>
/// An identity used to set Twitch request authorization headers.
/// </summary>
/// <remarks>
/// Use <see cref="ClientIdentity"/>, <see cref="UserIdentity"/>, or <see cref="ExtensionIdentity"/>, depending on the endpoint requirement.
/// Pattern matching can be used to determine the actual identity type and set request headers accordingly.
/// </remarks>
public record TwitchApiIdentity()
{
    /// <summary>
    /// The id of the application registered on the <see href="https://dev.twitch.tv/">Twitch Developer Portal</see> to requests as.
    /// </summary>
    public ClientId? ClientId { get; init; }
    /// <summary>
    /// Represents an explicit "no identity" value.
    /// </summary>
    /// <remarks>
    /// The <see cref="DefaultRequestAuthorizer"/> will not override this with the default <see cref="ClientIdentity"/>.
    /// </remarks>
    public static TwitchApiIdentity None { get; } = new() { ClientId = new ClientId("") };
    /// <summary>
    /// Represents a default identity with a <see langword="null"/> <see cref="ClientId"/>.
    /// </summary>
    /// <remarks>
    /// This is resolved to the configured default <see cref="ClientIdentity"/> by the <see cref="DefaultRequestAuthorizer"/>.
    /// </remarks>
    public static TwitchApiIdentity Default { get; } = new();
}

/// <summary>
/// The Twitch developer application to make requests on behalf of.
/// </summary>
public record ClientIdentity : TwitchApiIdentity
{
    // Alias to enforce non-nullability.
    public new ClientId ClientId { get => base.ClientId ?? default; init => base.ClientId = value; }
    /// <param name="clientId"><inheritdoc cref="TwitchApiIdentity.ClientId" path="/summary"/></param>
    public ClientIdentity(ClientId clientId)
    {
        ClientId = clientId;
    }
    public static implicit operator ClientIdentity(ClientId clientId)
        => new(clientId);
}

/// <summary>
/// A user to make requests on behalf of.
/// </summary>
/// <param name="UserId">The id of the user to make requests on behalf of.</param>
public record UserIdentity(UserId UserId) : TwitchApiIdentity;

/// <summary>
/// The extension to make requests on behalf of.
/// </summary>
/// <remarks>
/// Extension endpoints use JWT authentication signed with the extension's secret.
/// The <see cref="OwnerId"/> is included in the JWT payload as the <c>user_id</c> field.
/// </remarks>
/// <param name="OwnerId">The Twitch user id of the extension owner/developer.</param>
public record ExtensionIdentity(UserId OwnerId) : TwitchApiIdentity;