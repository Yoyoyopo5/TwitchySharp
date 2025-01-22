using System;
using System.Collections.Generic;
using System.Text;
using TwitchySharp.Api.ApiResponseConverters;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Empty response. Contains no data.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record ModifyChannelInformationResponse { }
