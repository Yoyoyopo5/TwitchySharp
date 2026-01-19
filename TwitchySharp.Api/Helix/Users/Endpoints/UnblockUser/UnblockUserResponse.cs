using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record UnblockUserResponse { }
