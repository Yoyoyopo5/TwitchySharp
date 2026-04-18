using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Moderation;

/// <summary>
/// Contains static definitions for held AutoMod message actions.
/// Used with <see cref="ManageHeldAutoModMessagesRequest"/>.
/// </summary>
/// <param name="Value">The name of the action to take.</param>
[Wrapper<string>]
public readonly partial record struct AutoModAction(string Value)
{
    public static AutoModAction Allow { get; } = new("ALLOW");
    public static AutoModAction Deny { get; } = new("DENY");
}
