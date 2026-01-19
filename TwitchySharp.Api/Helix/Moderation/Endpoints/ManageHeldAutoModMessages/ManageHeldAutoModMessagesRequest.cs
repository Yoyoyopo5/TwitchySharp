using System.Net.Http;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Authorization;

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
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageAutomod"/>.</param>
    /// <param name="messageAction">
    /// Data used to identify the message and select the action.
    /// </param>
    public ManageHeldAutoModMessagesRequest(
        string clientId,
        string accessToken,
        ManageHeldAutoModMessagesRequestData messageAction
        ) : base(
            "/moderation/automod/message",
            clientId,
            accessToken
            )
    {
        Method = HttpMethod.Post;
        ContentObject = messageAction;
    }
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
    public required string UserId { get; set; }
    /// <summary>
    /// The id of the message to allow or deny.
    /// </summary>
    [JsonPropertyName("msg_id")]
    public required string MessageId { get; set; }
    /// <summary>
    /// The action to take for the message.
    /// Use the static definitions on the <see cref="AutoModAction"/> class to set this.
    /// </summary>
    public required AutoModAction Action { get; set; }
}