namespace TwitchySharp.Api.AuthorizationResolution.Tests.Integration;

public record TestTwitchResponseData
{
    public required TwitchAuthorizationHeaders RequestAuthorizationHeaders { get; init; }
}
