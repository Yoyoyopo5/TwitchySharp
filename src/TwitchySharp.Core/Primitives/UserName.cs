using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// A Twitch user's display name.
/// </summary>
/// <remarks>
/// This is the mixed case version of the <see cref="UserLogin"/> and can be customized by users.
/// It can also be a localized name for users using Twitch in Arabic, Chinese, Korean, or Japanese.
/// See <see href="https://help.twitch.tv/s/article/display-names-on-twitch">Display Names on Twitch</see> for more information.
/// </remarks>
/// <param name="Value">The string value of the display name.</param>
[Wrapper<string>]
public readonly partial record struct UserName(string Value);
