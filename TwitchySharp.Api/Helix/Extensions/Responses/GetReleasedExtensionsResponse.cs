using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Contains a list of requested released extensions.
/// </summary>
public record GetReleasedExtensionsResponse
{
    /// <summary>
    /// A list that contains the specified extension as its single entry.
    /// </summary>
    public required Extension[] Data { get; init; }
}
