using System;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api;
/// <summary>
/// An identity used to set Twitch request authorization headers.
/// </summary>
/// <remarks>
/// Use <see cref="TwitchIdentity.Client"/>, <see cref="TwitchIdentity.User"/>, or <see cref="TwitchIdentity.Extension"/>, depending on the endpoint requirement.
/// Pattern matching can be used to determine the actual identity type and set request headers accordingly.
/// </remarks>
public abstract record TwitchIdentity
{
    protected TwitchIdentity(ClientId? clientId)
        => ClientId = clientId;
    public ClientId? ClientId { get; init; }
    /// <summary>
    /// An explicit no identity.
    /// </summary>
    public sealed record None : TwitchIdentity
    {
        private None() : base((ClientId?)null) { }
        public static None Instance { get; } = new();
    }
    /// <summary>
    /// The Twitch developer application to make requests on behalf of.
    /// </summary>
    /// <param name="ClientId">The id of the application registered on the <see href="https://dev.twitch.tv/">Twitch Developer Portal</see> to requests as.</param>
    public sealed record Client(ClientId? ClientId) : TwitchIdentity(ClientId)
    {
        /// <summary>
        /// A default client identity.
        /// </summary>
        /// <remarks>
        /// Does not carry any information, but signals to the authorization resolver to override with a default identity.
        /// </remarks>
        public static TwitchIdentity Default { get; } = new Client((ClientId?)null);
    }
    /// <summary>
    /// A user to make requests on behalf of.
    /// </summary>
    /// <param name="ClientId"><inheritdoc cref="Client(ClientId)" path="/param[@name='ClientId']"/></param>
    /// <param name="UserId">The id of the user to make requests on behalf of.</param>
    public sealed record User(UserId UserId, ClientId? ClientId = null) : TwitchIdentity(ClientId);
    /// <summary>
    /// The extension to make requests on behalf of.
    /// </summary>
    /// <remarks>
    /// Extension endpoints use JWT authentication signed with the extension's secret.
    /// The <paramref name="OwnerId"/> is included in the JWT payload as the <c>user_id</c> field.
    /// </remarks>
    /// <param name="ClientId"><inheritdoc cref="Client(ClientId)" path="/param[@name='ClientId']"/></param>
    /// <param name="OwnerId">The Twitch user id of the extension owner/developer.</param>
    public sealed record Extension(UserId OwnerId, ClientId? ClientId = null) : TwitchIdentity(ClientId);

    /// <summary>
    /// Create a new <see cref="TwitchIdentity"/> with a set <c>ClientId</c>.
    /// </summary>
    /// <param name="clientId">The client id to use.</param>
    /// <returns>
    /// A new <see cref="TwitchIdentity"/> of the same dervied type with <c>ClientId</c> set to <paramref name="clientId"/>.
    /// If <see cref="None"/> or <see cref="Default"/>, a <see cref="TwitchIdentity.Client"/> is returned.
    /// </returns>
    /// <exception cref="NotSupportedException"></exception>
    public TwitchIdentity WithClientId(ClientId clientId)
        => this switch
        {
            None => new Client(clientId),
            Client client => client with { ClientId = clientId },
            User user => user with { ClientId = clientId },
            Extension extension => extension with { ClientId = clientId },
            _ => throw new NotSupportedException("Unsupported identity type.")
        };
}

/// <summary>
/// An identity used to set Twitch request authorization headers.
/// </summary>
/// <remarks>
/// Use <see cref="ClientIdentity"/>, <see cref="UserIdentity"/>, or <see cref="ExtensionIdentity"/>, depending on the endpoint requirement.
/// Pattern matching can be used to determine the actual identity type and set request headers accordingly.
/// </remarks>
[Obsolete("Use TwitchIdentity")]
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
/// <param name="ClientId"><inheritdoc cref="ITwitchIdentity.ClientId" path="/summary"/></param>
[Obsolete("Use TwitchIdentity.Client")]
public readonly record struct ClientIdentity(ClientId ClientId)
{
    /// <param name="clientId"><inheritdoc cref="ITwitchIdentity.ClientId" path="/summary"/></param>
    public static implicit operator ClientIdentity(ClientId clientId)
        => new(clientId);
}

/// <summary>
/// A user to make requests on behalf of.
/// </summary>
/// <param name="ClientId"><inheritdoc cref="ITwitchIdentity.ClientId" path="/summary"/></param>
/// <param name="UserId">The id of the user to make requests on behalf of.</param>
[Obsolete("Use TwitchIdentity.User")]
public readonly record struct UserIdentity(ClientId ClientId, UserId UserId);

/// <summary>
/// The extension to make requests on behalf of.
/// </summary>
/// <remarks>
/// Extension endpoints use JWT authentication signed with the extension's secret.
/// The <paramref name="OwnerId"/> is included in the JWT payload as the <c>user_id</c> field.
/// </remarks>
/// <param name="ClientId"><inheritdoc cref="ITwitchIdentity.ClientId" path="/summary"/></param>
/// <param name="OwnerId">The Twitch user id of the extension owner/developer.</param>
[Obsolete("Use TwitchIdentity.Extension")]
public readonly record struct ExtensionIdentity(ClientId ClientId, UserId OwnerId);