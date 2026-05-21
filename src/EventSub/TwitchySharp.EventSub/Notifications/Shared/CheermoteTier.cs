using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// A cheermote tier.
/// </summary>
/// <remarks>
/// The tier of a cheermote increases at 100, 1000, 5000, and 10000 bits used.
/// See <see href="https://help.twitch.tv/s/article/partner-cheermote-guide">Partner Cheermote Guide</see> for more information.
/// </remarks>
/// <param name="Value"></param>
[Wrapper<int>]
public readonly partial record struct CheermoteTier(int Value)
{
    public static bool operator >(CheermoteTier a, CheermoteTier b)
        => a.Value > b.Value;
    public static bool operator <(CheermoteTier a, CheermoteTier b)
        => a.Value < b.Value;
}
