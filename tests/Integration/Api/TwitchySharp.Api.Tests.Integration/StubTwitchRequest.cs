namespace TwitchySharp.Api.Tests.Integration;

public record StubTwitchRequest(string Path) : TwitchRequest<object>
{
    public override HttpMethod Method => HttpMethod.Get;
    public override Uri RequestUri => new("https://api.twitch.tv" + Path);

    public override Func<Stream, CancellationToken, ValueTask<object>>? ConvertResponseContent { get; init; }
        = (_, _) => ValueTask.FromResult(new object());
}
