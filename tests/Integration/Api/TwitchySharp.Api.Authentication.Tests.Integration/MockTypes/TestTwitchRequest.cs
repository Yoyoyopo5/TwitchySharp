namespace TwitchySharp.Api.AuthorizationResolution.Tests.Integration;

public record TestTwitchRequest : TwitchRequest
{
    public override HttpMethod Method => throw new NotImplementedException();
    public override Uri RequestUri => throw new NotImplementedException();
}
