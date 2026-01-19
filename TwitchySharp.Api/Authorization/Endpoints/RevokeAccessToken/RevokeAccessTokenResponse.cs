using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record RevokeAccessTokenResponse { }
