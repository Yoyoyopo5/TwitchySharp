using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// The level of severity for the caught message.
/// </summary>
/// <remarks>
/// Ranges from 1 to 4.
/// </remarks>
/// <param name="Value">The integer value of the severity level.</param>
[Wrapper<int>]
public readonly partial record struct AutomodCaughtMessageSeverity(int Value)
{
    public static bool operator >(AutomodCaughtMessageSeverity a, AutomodCaughtMessageSeverity b)
        => a.Value > b.Value;

    public static bool operator <(AutomodCaughtMessageSeverity a, AutomodCaughtMessageSeverity b)
        => a.Value < b.Value;
}
