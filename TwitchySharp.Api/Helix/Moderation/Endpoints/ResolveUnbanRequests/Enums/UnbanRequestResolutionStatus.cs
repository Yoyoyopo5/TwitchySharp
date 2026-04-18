using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains static definitions for possible unban request resolution statuses.
/// </summary>
/// <param name="Value">The string value of the resolution status.</param>
[Wrapper<string>]
public readonly partial record struct UnbanRequestResolutionStatus(string Value)
{
    public static UnbanRequestResolutionStatus Approved { get; } = new("approved");
    public static UnbanRequestResolutionStatus Denied { get; } = new("denied");
}
