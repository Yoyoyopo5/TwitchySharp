using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Allow or deny the message that AutoMod flagged for review.
/// </summary>
/// <remarks>
/// For information about AutoMod, see <see href="https://help.twitch.tv/s/article/how-to-use-automod">How to Use AutoMod</see>.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageAutomod"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#manage-held-automod-messages">Manage Held AutoMod Messages</see> for more information.
/// </remarks>
public record ManageHeldAutoModMessagesRequest
    : TwitchHelixRequest<ManageHeldAutoModMessagesResponse>
{
    protected override string Path => "/moderation/automod/message";
    public override HttpMethod Method => HttpMethod.Post;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(MessageAction.UserId),
        ValidScopes = ImmutableHashSet.Create(Scope.ModeratorManageAutomod)
    };
    public override object? ContentObject => MessageAction;

    /// <summary>
    /// Data used to identify the message and select the action.
    /// </summary>
    public required ManageHeldAutoModMessagesRequestData MessageAction { get; init; }

    protected override ValueTask<ManageHeldAutoModMessagesResponse> ConvertResponseContent(Stream contentStream, CancellationToken ct = default)
        => ValueTask.FromResult(new ManageHeldAutoModMessagesResponse());
}

/// <summary>
/// Contains data used to handle a chat message held by a channel's AutoMod.
/// </summary>
public record ManageHeldAutoModMessagesRequestData
{
    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// This must be the same user that created the access token used in the <see cref="ManageHeldAutoModMessagesRequest"/>.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The id of the message to allow or deny.
    /// </summary>
    [JsonPropertyName("msg_id")]
    public required MessageId MessageId { get; init; }
    /// <summary>
    /// The action to take for the message.
    /// Use the static definitions on the <see cref="AutoModAction"/> class to set this.
    /// </summary>
    public required AutoModAction Action { get; init; }
}
