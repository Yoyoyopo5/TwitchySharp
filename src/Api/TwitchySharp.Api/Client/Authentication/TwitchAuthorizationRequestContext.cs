namespace TwitchySharp.Api;

public record TwitchAuthorizationRequestContext : TwitchRequestContext
{
    public static TwitchAuthorizationRequestContext From(TwitchRequestContext context)
        => new() { Request = context.Request };
    public TwitchAuthorizationHeaders AuthorizationHeaders { get; init; }
    public override HttpRequestMessage ToHttpRequestMessage() => base.ToHttpRequestMessage().AddTwitchAuthorizationHeaders(AuthorizationHeaders);
}
