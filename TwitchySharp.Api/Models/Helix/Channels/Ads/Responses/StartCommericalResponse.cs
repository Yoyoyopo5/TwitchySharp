using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Models.Helix.Channels.Ads.Models;

namespace TwitchySharp.Api.Models.Helix.Channels.Ads.Responses;
/// <summary>
/// Contains data about the started ad.
/// </summary>
public record StartCommericalResponse
{
    /// <summary>
    /// An array that contains a single <see cref="StartedCommerical"/> with the status of your start commercial request.
    /// </summary>
    public required StartedCommerical[] Data { get; init; }
}
