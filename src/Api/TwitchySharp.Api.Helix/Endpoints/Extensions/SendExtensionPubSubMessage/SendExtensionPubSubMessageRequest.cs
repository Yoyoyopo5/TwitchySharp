using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Sends a message to one or more viewers.
/// </summary>
/// <remarks>
/// You can send messages to a specific channel or to all channels where your extension is active.
/// This endpoint uses the same mechanism as the <see href="https://dev.twitch.tv/docs/extensions/reference#send">Send</see> JavaScript helper function used to send messages.
/// <br/>
/// Rate Limits: You may send a maximum of 100 messages per minute per combination of extension client ID and broadcaster ID.
/// <br/>
/// Requires a signed JSON Web Token (JWT) created by an EBS.
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>.
/// The signed JWT must include the role, user_id, and exp fields (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>) along with the channel_id and pubsub_perms fields.
/// The role field must be set to external.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#send-extension-pubsub-message">Send Extension PubSub Message</see> for more information.
/// </remarks>
public record SendExtensionPubSubMessageRequest
    : TwitchHelixRequest<SendExtensionPubSubMessageResponseContent>,
    IAuthenticatedTwitchRequest<TwitchRequestAuthenticationContext<TwitchIdentity.Extension>>
{
    protected override string Path => "/extensions/pubsub";
    public override HttpMethod Method => HttpMethod.Post;
    private TwitchRequestAuthenticationContext<TwitchIdentity.Extension> DefaultAuthenticationContext => new()
    {
        Identity = new(ExtensionId, Message is BroadcastPubSubMessageData broadcast ? broadcast.BroadcasterId : null)
    };
    public TwitchRequestAuthenticationContext<TwitchIdentity.Extension> AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }

    public override object? ContentObject => Message;

    /// <summary>
    /// The id of the extension.
    /// </summary>
    public required ExtensionId ExtensionId { get; init; }

    /// <summary>
    /// Data used to create and send the message.
    /// Use derived classes <see cref="BroadcastPubSubMessageData"/> and <see cref="GlobalPubSubMessageData"/>.
    /// </summary>
    public required SendExtensionPubSubMessageRequestData Message { get; init; }

    public override Func<Stream, CancellationToken, ValueTask<SendExtensionPubSubMessageResponseContent>>? ConvertResponseContent { get; init; }
        = (_, _) => ValueTask.FromResult(new SendExtensionPubSubMessageResponseContent());
}

/// <summary>
/// Contains data used to send an extension PubSub message.
/// Use derived classes <see cref="BroadcastPubSubMessageData"/> and <see cref="GlobalPubSubMessageData"/>.
/// </summary>
public record SendExtensionPubSubMessageRequestData
{
    protected ImmutableHashSet<ExtensionPubSubMessageTarget> _target { get; init; } = [];
    /// <summary>
    /// The target of the message. 
    /// The <see cref="ExtensionPubSubMessageTarget.Broadcast"/> and <see cref="ExtensionPubSubMessageTarget.Global"/> values are mutually exclusive.
    /// </summary>
    public IEnumerable<ExtensionPubSubMessageTarget> Target => _target;
    /// <summary>
    /// The user id of the broadcaster to send the message to. 
    /// Don’t include this field if <see cref="IsGlobalBroadcast"/> is set to <see langword="true"/>.
    /// </summary>
    public UserId? BroadcasterId { get; protected init; }
    /// <summary>
    /// Determines whether the message should be sent to all channels where your extension is active. 
    /// Set to <see langword="true"/> if the message should be sent to all channels. The default is <see langword="false"/>.
    /// </summary>
    public bool? IsGlobalBroadcast { get; protected init; }
    /// <summary>
    /// The message to send. The message can be a plain-text string or a string-encoded JSON object. 
    /// The message is limited to a maximum of 5 KB.
    /// </summary>
    public required string Message { get; init; }
}

/// <summary>
/// Used to send a PubSub message globally to all instances of an extension.
/// </summary>
public record GlobalPubSubMessageData
    : SendExtensionPubSubMessageRequestData
{
    /// <summary>
    /// <inheritdoc cref="GlobalPubSubMessageData"/>
    /// </summary>
    public GlobalPubSubMessageData()
        => (_target, IsGlobalBroadcast) = ([ExtensionPubSubMessageTarget.Global], true);
}

/// <summary>
/// Used to send a PubSub message to a specific broadcaster or specific user through Whispers.
/// Be sure to set the <see cref="ExtensionJwtPayload.ChannelId"/> to the user id of the channel you want to broadcast to.
/// </summary>
public record BroadcastPubSubMessageData
    : SendExtensionPubSubMessageRequestData
{
    /// <summary>
    /// <inheritdoc cref="BroadcastPubSubMessageData"/>
    /// </summary>
    public BroadcastPubSubMessageData()
        => IsGlobalBroadcast = false;

    /// <summary>
    /// Set the broadcaster the message should be sent to.
    /// </summary>
    /// <param name="broadcasterId">The user id of the broadcaster to send the PubSub message to.</param>
    public BroadcastPubSubMessageData To(UserId broadcasterId)
        => this with { _target = _target.Add(ExtensionPubSubMessageTarget.Broadcast), BroadcasterId = broadcasterId };

    /// <summary>
    /// Set the user the message should be sent to through Whispers.
    /// </summary>
    /// <param name="userId">The id of the user to send the PubSub message to.</param>
    public BroadcastPubSubMessageData WhisperTo(UserId userId)
        => this with { _target = _target.Add(ExtensionPubSubMessageTarget.Whisper(userId)), BroadcasterId = BroadcasterId ?? userId };
}
