using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch game or category.
/// </summary>
/// <param name="Value">The string value of the game id.</param>
public readonly partial record struct GameId(string Value) : IWrapValue<string>
{
    public static GameId None { get; } = new(string.Empty);
}