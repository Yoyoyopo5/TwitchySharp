using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Integration;

public record StubTwitchRequest(string Path) : TwitchRequest<object>
{
    public override HttpMethod Method => HttpMethod.Get;
    public override Uri RequestUri => new("https://api.twitch.tv" + Path);

    public override ValueTask<TwitchResponse<object>> CreateResponse(HttpResponseMessage response, CancellationToken ct)
    {
        using HttpResponseMessage r = response;
        return r.IsSuccessStatusCode == false
            ? ValueTask.FromResult(new TwitchResponse<object>(new Error()))
            : ValueTask.FromResult(new TwitchResponse<object>(new object()));
    }
}
