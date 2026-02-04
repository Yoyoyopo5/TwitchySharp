using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Sends a message to the broadcaster's chat room.
/// </summary>
/// <remarks>
/// <para>
/// Requires a user access token with <see cref="Scope.UserWriteChat"/> or an app access token where the sending user has <see cref="Scope.UserBot"/> and <see cref="Scope.ChannelBot"/> on another user access token granted to this client id.
/// <br/>
/// Defaults to using a <see cref="UserIdentity"/> based on the message sender (requires <see cref="Scope.UserWriteChat"/>).
/// <br/>
/// If you want to use the <see cref="Scope.ChannelBot"/> and <see cref="Scope.UserBot"/> scopes with a <see cref="ClientIdentity"/> (app access token),
/// you should use the <see cref="AsBot(TwitchySharp.Api.ClientIdentity?)"/> method to configure the <see cref="ClientIdentity"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#send-chat-message">Send Chat Message</see> for more information.
/// </remarks>
public record SendChatMessageRequest
    : TwitchHelixRequest<SendChatMessageResponse>
{
    protected override string Path => "/chat/messages";
    public override HttpMethod Method => HttpMethod.Post;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(Message.SenderId);
    public override IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.UserWriteChat, Scope.UserBot, Scope.ChannelBot);
    public override object? ContentObject => Message;
    /// <summary>
    /// Allows for sending the request using an app access token with a user that has authorized the app with <see cref="Scope.UserBot"/> and <see cref="Scope.ChannelBot"/>.
    /// </summary>
    /// <param name="client">
    /// The client to use.
    /// Leave this <see cref="null"/> to use <see cref="TwitchApiIdentity.Default"/>, which is set to a fallback by the <see cref="DefaultRequestAuthorizer"/>.
    /// </param>
    /// <returns>A new <see cref="SendChatMessageRequest"/> with the identity override configured.</returns>
    public SendChatMessageRequest AsBot(ClientIdentity? client = null)
        => this with
        {
            Identity = client ?? TwitchApiIdentity.Default
        };

    /// <summary>
    /// The message to send.
    /// </summary>
    public required SendChatMessageRequestData Message { get; init; }
}

/// <summary>
/// Contains information used to send a chat message to a specific chat room.
/// </summary>
public record SendChatMessageRequestData
{
    /// <summary>
    /// The user id of the broadcaster whose chat room the message will be sent to.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The user id of the user sending the message.
    /// If a user access token was used in the <see cref="SendChatMessageRequest"/>, this must be the same user who created the token.
    /// If an app access token was used, this user must have created a user access token with <see cref="Scope.UserBot"/> and <see cref="Scope.ChannelBot"/>, and it must be the broadcaster or a moderator in the broadcaster's chat room.
    /// </summary>
    public required UserId SenderId { get; init; }

    /// <summary>
    /// The message to send.
    /// The message is limited to a maximum of 500 characters.
    /// Chat messages can also include emoticons. To include emoticons, use the name of the emote.
    /// The names are case sensitive. Don't include colons around the name (e.g., :bleedPurple:).
    /// If Twitch recognizes the name, Twitch converts the name to the emote before writing the chat message to the chat room.
    /// </summary>
    public required string Message { get; init; }

    /// <summary>
    /// The message id of the chat message being replied to.
    /// </summary>
    public MessageId? ReplyParentMessageId { get; init; }

    /// <summary>
    /// Determines if the chat message is sent only to the source channel (defined by broadcaster_id) during a shared chat session.
    /// This has no effect if the message is not sent during a shared chat session.
    /// </summary>
    /// <remarks>
    /// This parameter can only be set when utilizing an app access token.
    /// It cannot be specified when a user access token is used, and will instead result in an HTTP 400 error.
    /// <br/>
    /// The default value is <see langword="true"/>.
    /// </remarks>
    public bool? ForSourceOnly { get; init; }
}
