using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Contains a list of extensions that the broadcaster has installed.
/// </summary>
public record GetUserExtensionsResponse
{
    /// <summary>
    /// The list of installed extensions.
    /// </summary>
    public required InstalledExtension[] Data { get; init; }
}
