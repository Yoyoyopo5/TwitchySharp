using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Allow or deny the message that AutoMod flagged for review. 
/// For information about AutoMod, see <see href="https://help.twitch.tv/s/article/how-to-use-automod">How to Use AutoMod</see>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#manage-held-automod-messages">manage held AutoMod messages</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageAutomod"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageAutomod"/>.</param>
/// <param name="messageAction">
/// Data used to identify the message and select the action.
/// </param>
public class ManageHeldAutoModMessagesRequest(
    string clientId,
    string accessToken,
    ManageHeldAutoModMessagesRequestData messageAction
    )
    : HelixApiRequest<ManageHeldAutoModMessagesResponse, ManageHeldAutoModMessagesRequestData>(
        "/moderation/automod/message",
        clientId,
        accessToken,
        messageAction
        );

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
    [JsonConverter(typeof(ValueBackedEnumJsonConverter<AutoModAction, string>))]
    public required AutoModAction Action { get; set; }
}

/// <summary>
/// Contains static definitions for held AutoMod message actions.
/// Used with <see cref="ManageHeldAutoModMessagesRequest"/>.
/// </summary>
/// <param name="Value">The name of the action to take.</param>
public record AutoModAction(string Value) 
    : ValueBackedEnum<string>(Value)
{
    public static AutoModAction Allow { get; } = new("ALLOW");
    public static AutoModAction Deny { get; } = new("DENY");
}
