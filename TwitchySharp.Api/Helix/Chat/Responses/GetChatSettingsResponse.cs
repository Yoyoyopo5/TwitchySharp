using OneOf.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Contains a list of chat settings.
/// </summary>
public record GetChatSettingsResponse
{
    /// <summary>
    /// The list of chat settings. 
    /// The list contains a single object with all the settings.
    /// </summary>
    public required ChatSettings[] Data { get; init; }
}
