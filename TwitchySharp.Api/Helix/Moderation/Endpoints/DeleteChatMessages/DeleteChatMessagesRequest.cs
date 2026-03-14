using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Removes a single chat message or all chat messages from the broadcaster's chat room.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageChatMessages"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-chat-messages">Delete Chat Messages</see> for more information.
/// </remarks>
public record DeleteChatMessagesRequest
    : TwitchHelixRequest<DeleteChatMessagesResponse>
{
    protected override string Path => "/moderation/chat";
    public override HttpMethod Method => HttpMethod.Delete;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(ModeratorId);
    public override IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ModeratorManageChatMessages);
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId)
            .Add("message_id", MessageId);

    /// <summary>
    /// The user id of the broadcaster (channel) whose chat to delete a chat message from.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId ModeratorId { get; init; }

    /// <summary>
    /// The id of the message to remove.
    /// </summary>
    /// <remarks>
    /// The message must:
    /// <list type="bullet">
    /// <item>have been created within the last 6 hours.</item>
    /// <item>not belong to the broadcaster.</item>
    /// <item>not belong to a different moderator than specified in the moderatorId.</item>
    /// </list>
    /// If this parameter is <see langword="null"/>, the request removes all messages in the chatroom.
    /// </remarks>
    public MessageId? MessageId { get; init; }

    protected override ValueTask<DeleteChatMessagesResponse> ConvertResponseContent(Stream contentStream, CancellationToken ct = default)
        => ValueTask.FromResult(new DeleteChatMessagesResponse());
}
