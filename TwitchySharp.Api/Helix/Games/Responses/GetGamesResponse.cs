using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Games;
/// <summary>
/// Contains a list of Twitch categories.
/// </summary>
public record GetGamesResponse
{
    /// <summary>
    /// The list of categories and games.
    /// </summary>
    public required Game[] Data { get; init; }
}
