using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Integration;

public record StubTwitchRequest(string Path) : TwitchRequest<object>
{
    public override HttpMethod Method => HttpMethod.Get;
    public override Uri RequestUri => new("https://api.twitch.tv" + Path);

    public override IResponseContentConverter.ConvertResponse<object>? ConvertResponse { get; init; }
        = (_, response, _) =>
        {
            using HttpResponseMessage r = response;
            return ValueTask.FromResult<Result<object>>(new object());
        };
}
