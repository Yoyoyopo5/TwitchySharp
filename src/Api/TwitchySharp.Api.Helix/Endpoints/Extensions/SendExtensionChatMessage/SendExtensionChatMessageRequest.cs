namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Sends a message to the specified broadcaster's chat room.
/// </summary>
/// <remarks>
/// The extension's name is used as the username for the message in the chat room.
/// To send a chat message, your extension must enable Chat Capabilities (under your extension's Capabilities tab).
/// <b>Rate Limits:</b> You may send a maximum of 12 messages per minute per channel.
/// <br/>
/// Requires a signed JSON Web Token (JWT) created by an EBS.
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>.
/// The signed JWT must include the role and user_id fields (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>).
/// The role field must be set to external.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#send-extension-chat-message">Send Extension Chat Message</see> for more information.
/// </remarks>
public record SendExtensionChatMessageRequest
    : TwitchHelixRequest<SendExtensionChatMessageResponseContent>,
    IAuthenticatedTwitchRequest<TwitchRequestAuthenticationContext<TwitchIdentity.Extension>>
{
    protected override string Path => "/extensions/chat";
    public override HttpMethod Method => HttpMethod.Post;
    private TwitchRequestAuthenticationContext<TwitchIdentity.Extension> DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.Extension(
            _,
            BroadcasterId,
            Message.ExtensionId
            )
    };
    public TwitchRequestAuthenticationContext<TwitchIdentity.Extension> AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }

    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId);
    public override object? ContentObject => Message;

    /// <summary>
    /// The user id of the broadcaster with the extension to send a message to.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The message data to send.
    /// </summary>
    public required SendExtensionChatMessageRequestData Message { get; init; }

    public override Func<Stream, CancellationToken, ValueTask<SendExtensionChatMessageResponseContent>>? ConvertResponseContent { get; init; }
        = (_, _) => ValueTask.FromResult(new SendExtensionChatMessageResponseContent());
}

/// <summary>
/// Contains data used to send an extension chat message.
/// </summary>
public record SendExtensionChatMessageRequestData
{
    /// <summary>
    /// The message to send in chat.
    /// The message may contain a maximum of 280 characters.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// The id of the extension that's sending the chat message.
    /// </summary>
    public required ExtensionId ExtensionId { get; init; }
    /// <summary>
    /// The extension's version number.
    /// </summary>
    public required ExtensionVersion ExtensionVersion { get; init; }
}
