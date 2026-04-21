using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Shared.EventSub;

/// <summary>
/// Represents a specific EventSub Subscription condition key.
/// </summary>
/// <remarks>
/// See <see cref="https://dev.twitch.tv/docs/eventsub/eventsub-reference/#conditions">Conditions</see> for more information.
/// </remarks>
/// <param name="Value">The string value of the key.</param>
[Wrapper<string>]
public readonly partial record struct ConditionKey(string Value);