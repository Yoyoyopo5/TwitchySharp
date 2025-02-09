using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Updates the color used for the user’s name in chat.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-user-chat-color">update user chat color</see> for more information.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.UserManageChatColor"/>.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.UserManageChatColor"/>.</param>
/// <param name="userId">The user id of the user whose color to change. This must be the same user that created the <paramref name="accessToken"/>.</param>
/// <param name="color">The color to use for the user's name in chat.</param>
public class UpdateUserChatColorRequest(string clientId, string accessToken, string userId, ChatColor color)
    : HelixApiRequest<UpdateUserChatColorResponse>(
        "/chat/color" +
        new HttpQueryParameters()
            .Add("user_id", userId)
            .Add("color", HttpUtility.UrlEncode(color)),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Put;
}

/// <summary>
/// Helper class that contains static definitions for Twitch default chat colors.
/// Note that Twitch Turbo and Prime user can also use hex codes to select a chat username color.
/// To use a hex code, construct a new <see cref="ChatColor"/>.
/// </summary>
/// <param name="value">The hex color code to use.</param>
public record ChatColor(string value)
    : ValueBackedEnum<string>(value)
{
    public static ChatColor Blue { get; } = new("blue");
    public static ChatColor BlueViolet { get; } = new("blue_violet");
    public static ChatColor CadetBlue { get; } = new("cadet_blue");
    public static ChatColor Chocolate { get; } = new("chocolate");
    public static ChatColor Coral { get; } = new("coral");
    public static ChatColor DodgerBlue { get; } = new("dodger_blue");
    public static ChatColor Firebrick { get; } = new("firebrick");
    public static ChatColor GoldenRod { get; } = new("golden_rod");
    public static ChatColor Green { get; } = new("green");
    public static ChatColor HotPink { get; } = new("hot_pink");
    public static ChatColor OrangeRed { get; } = new("orange_red");
    public static ChatColor Red { get; } = new("red");
    public static ChatColor SeaGreen { get; } = new("sea_green");
    public static ChatColor SpringGreen { get; } = new("spring_green");
    public static ChatColor YellowGreen { get; } = new("yellow_green");
}
