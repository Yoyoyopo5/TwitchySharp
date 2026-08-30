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
    /// <param name="OwnerId">The Twitch user id of the extension owner/developer.</param>
    /// <param name="BroadcasterId">
    /// The user id of the broadcaster (channel) with the active extension to make requests for.
    /// This is not required for all endpoints. Included as the <c>channel_id</c> field of the JWT payload.
    /// </param>
    /// <param name="ExtensionId">The client id of the extension. Used as the <c>Client-Id</c> header of Helix API requests.</param>
    public sealed record Extension(UserId OwnerId, UserId? BroadcasterId = null, ExtensionId? ExtensionId = null) : TwitchIdentity(ExtensionId)
    {
        public ExtensionId? ExtensionId
        { 
            get => ClientId.HasValue ? new ExtensionId(ClientId.Value) : default;
            init => ClientId = value;
        }
        
        public Extension(UserId OwnerId, ExtensionId? ExtensionId = null)
            : this(OwnerId, null, ExtensionId) { }
    }

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
