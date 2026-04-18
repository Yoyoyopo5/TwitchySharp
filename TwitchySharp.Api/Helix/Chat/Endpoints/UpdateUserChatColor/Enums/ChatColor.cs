using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// Contains static definitions for Twitch default chat colors.
/// Note that Twitch Turbo and Prime user can also use hex codes to select a chat username color.
/// To use a hex code, construct a new <see cref="ChatColor"/>.
/// </summary>
/// <param name="Value">The hex color code to use.</param>
[Wrapper<string>]
public readonly partial record struct ChatColor(string Value)
{
    public ChatColor(RgbColor color)
        : this(color.ToString()) { }
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
