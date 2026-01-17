using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Models.Helix.Users.Responses;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record UnblockUserResponse { }
