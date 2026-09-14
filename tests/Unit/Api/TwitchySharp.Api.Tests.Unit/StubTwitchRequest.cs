namespace TwitchySharp.Api.Tests.Unit;

public record StubTwitchRequest : TwitchRequest<object>
{
    public override HttpMethod Method { get; } = HttpMethod.Get;
    public override Uri RequestUri { get; } = new("http://localhost");
}

public static class TestTwitchClientExtensions
{
    public static TwitchClient UseStubResponse<T>(this TwitchClient client)
        => client.SetResolver(async (scope, ct) =>
        {
            await scope.ResolveOrDefault<T>(ct);
            return new TwitchResponse<object>()
            {
                Content = new(),
                Request = scope.Request,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        });

    public static Task SendStubRequest(this TwitchClient client, CancellationToken ct)
        => client.SendAsync(new StubTwitchRequest(), ct);
}
