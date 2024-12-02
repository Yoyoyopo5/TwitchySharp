using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Contains a list of newly added extension secrets.
/// </summary>
public record CreateExtensionSecretResponse
{
    /// <summary>
    /// The list of newly created secrets, grouped by version.
    /// </summary>
    public required ExtensionSecretsVersion[] Data { get; init; }
}
