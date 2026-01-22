using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Games;

/// <summary>
/// A game id from <see href="https://www.igdb.com/">IGDB</see>.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[JsonConverter(typeof(WrapperJsonConverter<IgdbId, string>))]
public readonly record struct IgdbId(string Value) : IWrapValue<string>
{
    public static implicit operator string(IgdbId id)
        => id.Value;
    public override string ToString()
        => Value;
}
