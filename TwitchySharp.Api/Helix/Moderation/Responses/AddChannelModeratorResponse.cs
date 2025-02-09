using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.ApiResponseConverters;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record AddChannelModeratorResponse { }
