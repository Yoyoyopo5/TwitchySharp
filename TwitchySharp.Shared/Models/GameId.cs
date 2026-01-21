using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch game or category.
/// </summary>
/// <param name="Value">The string value of the game id.</param>
[JsonConverter(typeof(WrapperJsonConverter<GameId, string>))]
public readonly record struct GameId(string Value) : IWrapValue<string>
{
    public static implicit operator string(GameId id)
        => id.Value;
    public override string ToString()
        => Value;
}
