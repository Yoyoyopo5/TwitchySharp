using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Contains a list with a single <see cref="Extension"/>.
/// </summary>
public record GetExtensionsResponse
{
    /// <summary>
    /// A list that contains the requested extension as the single entry.
    /// </summary>
    public required Extension[] Data { get; init; }
}
