using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Contains a list of chat settings.
/// </summary>
public record UpdateChatSettingsResponse
{
    /// <summary>
    /// A list containing a single value of the chat settings after being updated.
    /// </summary>
    public required ChatSettings[] Data { get; init; }
}
