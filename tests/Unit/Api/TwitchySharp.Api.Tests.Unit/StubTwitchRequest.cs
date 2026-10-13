namespace TwitchySharp.Api.Tests.Unit;

public record StubTwitchRequest : TwitchRequest<object>
{
    public override HttpMethod Method { get; } = HttpMethod.Get;
    public override Uri RequestUri { get; } = new("http://localhost");
    public override ValueTask<TwitchResponse<object>> CreateResponse(HttpResponseMessage response, CancellationToken ct)
    {
        return ValueTask.FromResult(new TwitchResponse<object>(new object()));
    }
}

public static class TestTwitchClientExtensions
{
    public static TwitchClient UseStubResponse<T>(this TwitchClient client)
        => client.SetResolver(async (scope, ct) =>
        {
            await scope.ResolveOrDefault<T>(ct);
            return new TwitchResponse<object>(new object());
        });

    public static Task SendStubRequest(this TwitchClient client, CancellationToken ct)
        => client.SendAsync(new StubTwitchRequest(), ct);
}
