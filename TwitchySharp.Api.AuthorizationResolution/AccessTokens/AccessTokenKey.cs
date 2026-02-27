namespace TwitchySharp.Api.AuthorizationResolution;

public record AccessTokenKey<T>
    where T : TwitchApiIdentity
{
    public T? Identity { get; init; }
}