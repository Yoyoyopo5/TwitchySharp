namespace TwitchySharp.Api.AuthorizationResolution.Tests.Integration;

public record TestAuthorizedTwitchRequest : TwitchRequest<TestTwitchResponseData>, IAuthorizedTwitchRequest
{
    public override HttpMethod Method => throw new NotImplementedException();
    public override Uri RequestUri => throw new NotImplementedException();
    public required TwitchRequestAuthorizationContext AuthorizationContext { get; init; }
}
