using System.Text;
using System.Text.Json;
using TwitchySharp.Api.Models.Helix.Bits.Models;
using TwitchySharp.Helpers.JsonConverters;

namespace TwitchySharp.Api.Models.Helix.Bits.Responses;
/// <summary>
/// Contains a list of cheermote data.
/// </summary>
public record GetCheermotesResponse
{
    /// <summary>
    /// The list of Cheermotes. The list is in ascending order by the contained <see cref="Cheermote.Order"/> property.
    /// </summary>
    public required Cheermote[] Data { get; init; }
}