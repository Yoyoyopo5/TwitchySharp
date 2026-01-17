using System;
using System.Collections.Generic;
using System.Text;
using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Models.Authorization.Responses;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record RevokeAccessTokenResponse { }
