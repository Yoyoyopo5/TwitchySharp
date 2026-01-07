using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Enums.Events.Channel.SuspiciousUser;

/// <summary>
/// Contains static definitions for possible ban evasion likelihoods for suspicious chat users.
/// </summary>
/// <param name="Value">The string value for the ban evasion evaluation.</param>
public record SuspiciousUserBanEvasionEvaluationLevel(string Value) : ValueBackedEnum<string>(Value)
{
    public static SuspiciousUserBanEvasionEvaluationLevel Unknown { get; } = new("unknown");
    public static SuspiciousUserBanEvasionEvaluationLevel Possible { get; } = new("possible");
    public static SuspiciousUserBanEvasionEvaluationLevel Likely { get; } = new("likely");
}
