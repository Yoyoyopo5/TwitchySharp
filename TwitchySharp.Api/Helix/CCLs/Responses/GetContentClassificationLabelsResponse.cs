using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.CCLs;
/// <summary>
/// Contains a list of content classification labels.
/// </summary>
public record GetContentClassificationLabelsResponse
{
    /// <summary>
    /// The list of content classification labels.
    /// </summary>
    public required ContentClassificationLabel[] Data { get; init; }
}

/// <summary>
/// Contains information about a specific content classification label (CCL).
/// </summary>
public record ContentClassificationLabel
{
    /// <summary>
    /// The unique id of the CCL.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// A localized description of the CCL.
    /// </summary>
    public required string Description { get; init; }
    /// <summary>
    /// The localized name of the CCL.
    /// </summary>
    public required string Name { get; init; }
}
