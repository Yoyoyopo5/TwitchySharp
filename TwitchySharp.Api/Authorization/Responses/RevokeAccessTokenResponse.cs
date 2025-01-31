using System;
using System.Collections.Generic;
using System.Text;
using TwitchySharp.Api.ApiResponseConverters;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record RevokeAccessTokenResponse { }
