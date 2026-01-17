using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Models.Helix.GuestStar.Responses;
/// <summary>
/// Empty response.
/// </summary>
/// <remarks>
/// Dev Note: Docs are ambiguous on this response. Please update during integration testing.
/// </remarks>
[ApiConverter(typeof(EmptyResponseConverter))]
public record AssignGuestStarSlotResponse { }
