using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models.Helix.Chat.Models;

namespace TwitchySharp.Api.Models.Helix.Chat.Responses;
/// <summary>
/// Contains a list of global emotes.
/// </summary>
public record GetGlobalEmotesResponse
{
    /// <summary>
    /// The list of global emotes.
    /// </summary>
    public required GlobalEmote[] Data { get; init; }
    /// <summary>
    /// A templated URL for getting an emote image.
    /// </summary>
    public required EmoteImageTemplateString Template { get; init; }
}