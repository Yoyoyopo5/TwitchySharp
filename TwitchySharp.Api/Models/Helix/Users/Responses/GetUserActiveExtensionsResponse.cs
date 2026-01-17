using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Api.Models.Helix.Users.Models;

namespace TwitchySharp.Api.Models.Helix.Users.Responses;
/// <inheritdoc cref="UserActiveExtensions"/>
public record GetUserActiveExtensionsResponse
{
    /// <summary>
    /// The active extensions that the broadcaster has installed.
    /// </summary>
    public required UserActiveExtensions Data { get; init; }
}
